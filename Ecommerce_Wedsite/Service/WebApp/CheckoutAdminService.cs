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
    public interface ICheckoutAdminService // Tạo Interface
    {
        Task<ResponseMessageObject<Checkout_ViewModel>> Service_Test(); //Model lớn chứa Model nhỏ. Tạo Phương Thức     
    }
    public class CheckoutAdminService : ICheckoutAdminService // Thừa kế các thuộc tính từ Interface 
    {
        private readonly IConfigManagerService _configuration;
        public CheckoutAdminService(IConfigManagerService configuration)
        {
            _configuration = configuration;
        }

        public async Task<ResponseMessageObject<Checkout_ViewModel>> Service_Test() // có thể sử dụng chung viewmodel đc
        {
            var data = new ResponseMessageObject<Checkout_ViewModel>();
            data.Data = new Checkout_ViewModel();
            try
            {
                using (var dbConn = new SqlConnection(_configuration.GetConnectionString("ConnectionString"))) 
                {
                    await dbConn.OpenAsync(); // mở sync

                    //var query = dbConn.QueryBuilder($"select * from Checkout");

                    //data.Data.checkout = await query.QueryAsync<Checkout>();

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
