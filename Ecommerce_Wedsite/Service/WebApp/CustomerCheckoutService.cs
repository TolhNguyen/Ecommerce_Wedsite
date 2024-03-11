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
    public interface ICustomerCheckoutService // Tạo Interface
    {
        Task<CustomerCheckout> CustomerCheckoutFunction(CustomerCheckout customercheckout, int idwebname); //Model lớn chứa Model nhỏ. Tạo Phương Thức     
    }
    public class CustomerCheckoutService : ICustomerCheckoutService // Thừa kế các thuộc tính từ Interface 
    {
        private readonly IConfigManagerService _configuration;
        public CustomerCheckoutService(IConfigManagerService configuration)
        {
            _configuration = configuration;
            
            // chèn promotionservice vô
        }
        public async Task<CustomerCheckout> CustomerCheckoutFunction(CustomerCheckout customercheckout, int idwebname) // tạo riêng 1 service. (bỏ static r)
        {
            using (var dbConn = new SqlConnection(_configuration.GetConnectionString("ConnectionString")))
            {
                await dbConn.OpenAsync(); // mở sync

                //await _promotionService.PromotionFunction(cscheckouttp);

                customercheckout.UserLogin_Id = idwebname;
                
                dbConn.Insert(customercheckout); // hành động và lưu model vào db

                await dbConn.CloseAsync();
            }
            return customercheckout; // return obj lại
        }
    }
}
// Vẫn sai promotion service: đổi type thành decimal, chỉnh lại cách select và lấy dữ liệu ra, logic code.
// Cách giải: get value mã giảm từ Java và tính lại total price. Để khi insert thì total price đã giảm trước r 