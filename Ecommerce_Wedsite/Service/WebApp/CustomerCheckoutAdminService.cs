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
    public interface ICustomerCheckoutAdminService // Tạo Interface
    {
        Task<ResponseMessageObject<CustomerCheckout_ViewModel>> UnprocessCheckout(); //Model lớn chứa Model nhỏ. Tạo Phương Thức
        Task<ResponseMessageObject<CustomerCheckout_ViewModel>> ProcessCheckout(); //Model lớn chứa Model nhỏ. Tạo Phương Thức
        Task<ResponseMessageObject<CustomerCheckout_ViewModel>> Deliver(); //Model lớn chứa Model nhỏ. Tạo Phương Thức
    }
    public class CustomerCheckoutAdminService : ICustomerCheckoutAdminService // Thừa kế các thuộc tính từ Interface 
    {
        private readonly IConfigManagerService _configuration;
        public CustomerCheckoutAdminService(IConfigManagerService configuration)
        {
            _configuration = configuration;
        }

        public async Task<ResponseMessageObject<CustomerCheckout_ViewModel>> UnprocessCheckout() // có thể sử dụng chung viewmodel đc
        {
            var data = new ResponseMessageObject<CustomerCheckout_ViewModel>();
            data.Data = new CustomerCheckout_ViewModel();
            try
            {
                using (var dbConn = new SqlConnection(_configuration.GetConnectionString("ConnectionString")))
                {
                    await dbConn.OpenAsync(); // mở sync

                    var query = dbConn.QueryBuilder($"select * from CustomerCheckout WHERE CustomerCheckout_Status = 0");

                    data.Data.customercheckout = await query.QueryAsync<CustomerCheckout>();

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
        public async Task<ResponseMessageObject<CustomerCheckout_ViewModel>> ProcessCheckout() // chưa có
        {
            var data = new ResponseMessageObject<CustomerCheckout_ViewModel>();
            data.Data = new CustomerCheckout_ViewModel();
            try
            {
                using (var dbConn = new SqlConnection(_configuration.GetConnectionString("ConnectionString")))
                {
                    await dbConn.OpenAsync(); // mở sync

                    var query = dbConn.QueryBuilder($"select * from CustomerCheckout WHERE CustomerCheckout_Status = 1");

                    data.Data.customercheckout = await query.QueryAsync<CustomerCheckout>();

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
        public async Task<ResponseMessageObject<CustomerCheckout_ViewModel>> Deliver() // chưa có
        {
            var data = new ResponseMessageObject<CustomerCheckout_ViewModel>();
            data.Data = new CustomerCheckout_ViewModel();
            try
            {
                using (var dbConn = new SqlConnection(_configuration.GetConnectionString("ConnectionString")))
                {
                    await dbConn.OpenAsync(); // mở sync

                    var query = dbConn.QueryBuilder($"select * from CustomerCheckout WHERE CustomerCheckout_Status = 2");

                    data.Data.customercheckout = await query.QueryAsync<CustomerCheckout>();

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


