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
    public interface IDiscountAdminService // Tạo Interface
    {
        Task<ResponseMessageObject<Discountt_ViewModel>> Service_Test(); //Model lớn chứa Model nhỏ. Tạo Phương Thức     
    }
    public class DiscountAdminService : IDiscountAdminService // Thừa kế các thuộc tính từ Interface 
    {
        private readonly IConfigManagerService _configuration;
        public DiscountAdminService(IConfigManagerService configuration)
        {
            _configuration = configuration;
        }

        public async Task<ResponseMessageObject<Discountt_ViewModel>> Service_Test() // có thể sử dụng chung viewmodel đc
        {
            var data = new ResponseMessageObject<Discountt_ViewModel>();
            data.Data = new Discountt_ViewModel();
            try
            {
                using (var dbConn = new SqlConnection(_configuration.GetConnectionString("ConnectionString")))
                {
                    await dbConn.OpenAsync(); // mở sync

                    var query = dbConn.QueryBuilder($"select * from Discountt");

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
