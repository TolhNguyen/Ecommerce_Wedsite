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
    public interface IHeaderAdminDeleteService // Tạo Interface
    {
        Task<ResponseMessageObject<Header_ViewModel>> Service_Test(int id); //Model lớn chứa Model nhỏ. Tạo Phương Thức     
    }
    public class HeaderAdminDeleteService : IHeaderAdminDeleteService // Thừa kế các thuộc tính từ Interface 
    {
        private readonly IConfigManagerService _configuration;
        public HeaderAdminDeleteService(IConfigManagerService configuration)
        {
            _configuration = configuration;
        }

        public async Task<ResponseMessageObject<Header_ViewModel>> Service_Test(int id) // Lấy dữ liệu model từ db lên và hành động vào
        {
            var data = new ResponseMessageObject<Header_ViewModel>();
            data.Data = new Header_ViewModel();
            try
            {
                using (var dbConn = new SqlConnection(_configuration.GetConnectionString("ConnectionString")))
                {

                    await dbConn.OpenAsync(); // mở sync

                    var query = dbConn.QueryBuilder($"select * from Header where Header_Id = {id}");

                    var delete = await query.QueryFirstOrDefaultAsync<Header>();

                    var it = dbConn.Delete(delete); // hành động và lưu model vào db

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


