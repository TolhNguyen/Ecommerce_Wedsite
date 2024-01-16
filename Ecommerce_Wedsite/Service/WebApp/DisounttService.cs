using DapperQueryBuilder;
using Ecommerce_Wedsite.Models.Entities;
using Ecommerce_Wedsite.Models.Helpers.Response;
using Ecommerce_Wedsite.Models.ViewModel;
using Ecommerce_Wedsite.Service.Helpers.Configuration;
using Microsoft.AspNetCore.DataProtection;
using System.Data.SqlClient;
using Dapper;
using Dapper.Contrib.Extensions;

namespace Ecommerce_Wedsite.Service.WebApp
{
    public interface IDiscounttService // Tạo Interface
    {
        Task<ResponseMessageObject<Discountt_ViewModel>> PopupDiscount(); //Model lớn chứa Model nhỏ. Tạo Phương Thức     
    }
        public class DiscounttService : IDiscounttService // Thừa kế các thuộc tính từ Interface 
    {
            private readonly IConfigManagerService _configuration;
            public DiscounttService(IConfigManagerService configuration)
            {
                _configuration = configuration;
            }

            public async Task<ResponseMessageObject<Discountt_ViewModel>> PopupDiscount() // chức năng: để lấy dữ liệu discount hiện lên popup 
            {
                var data = new ResponseMessageObject<Discountt_ViewModel>();
                data.Data = new Discountt_ViewModel();
                try
                {
                    using (var dbConn = new SqlConnection(_configuration.GetConnectionString("ConnectionString")))
                    {
                        await dbConn.OpenAsync(); // mở sync

                        var query = dbConn.QueryBuilder($"SELECT TOP 1 * FROM Discountt ORDER BY Discount_Id DESC"); // Luôn lấy discount mới nhất lên

                        data.Data.discountt = await query.QueryAsync<Discountt>();

                        await dbConn.CloseAsync(); 
                    }
                }
                catch (Exception e) 
                {
                    data.message = e.Message;
                    data.success = false;
                }
                return data;
            }
        }
}
