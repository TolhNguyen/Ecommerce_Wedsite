using DapperQueryBuilder;
using Ecommerce_Wedsite.Models.Entities;
using Ecommerce_Wedsite.Models.Helpers.Response;
using Ecommerce_Wedsite.Models.ViewModel;
using Ecommerce_Wedsite.Service.Helpers.Configuration;
using Microsoft.AspNetCore.DataProtection;
using System.Data.SqlClient;
using Dapper;
using Dapper.Contrib.Extensions;
using Microsoft.Build.Evaluation;

namespace Ecommerce_Wedsite.Service.WebApp
{
    public interface ICustomerCheckoutAdminService // Tạo Interface
    {
        Task<ResponseMessageObject<CustomerCheckout_ViewModel>> UnprocessCheckout(); //Model lớn chứa Model nhỏ. Tạo Phương Thức
        Task<ResponseMessageObject<CustomerCheckout_ViewModel>> ProcessCheckout(); //Model lớn chứa Model nhỏ. Tạo Phương Thức
        Task<ResponseMessageObject<CustomerCheckout_ViewModel>> Deliver(); //Model lớn chứa Model nhỏ. Tạo Phương Thức
        Task<ResponseMessageObject<CustomerCheckout_ViewModel>> Finish(); //Model lớn chứa Model nhỏ. Tạo Phương Thức
        Task<int> ProcessFunction(int id, int status); //Model lớn chứa Model nhỏ. Tạo Phương Thức
        Task<int> ProcessReversedFunction(int id, int status);
        Task<ResponseMessageObject<ProductCheckout_ViewModel>> Details(int CustomerCheckout_Id);
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
        public async Task<ResponseMessageObject<CustomerCheckout_ViewModel>> Finish() // chưa có
        {
            var data = new ResponseMessageObject<CustomerCheckout_ViewModel>();
            data.Data = new CustomerCheckout_ViewModel();
            try
            {
                using (var dbConn = new SqlConnection(_configuration.GetConnectionString("ConnectionString")))
                {
                    await dbConn.OpenAsync(); // mở sync

                    var query = dbConn.QueryBuilder($"select * from CustomerCheckout WHERE CustomerCheckout_Status = 3");

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
        public async Task<int> ProcessFunction(int id, int status) 
        {
            var data = new ResponseMessageObject<CustomerCheckout_ViewModel>();
            data.Data = new CustomerCheckout_ViewModel();
            try
            {
                using (var dbConn = new SqlConnection(_configuration.GetConnectionString("ConnectionString")))
                {
                    await dbConn.OpenAsync(); // mở sync

                    var query = dbConn.QueryBuilder($"select * from CustomerCheckout WHERE CustomerCheckout_Id = {id}");
                    var cs = await query.QueryFirstOrDefaultAsync<CustomerCheckout>(); /// lấy du lieu tu db.
                    status += 1;
                    cs.CustomerCheckout_Status = (Status)status; // vì status đang là enum nên phải ép kiểu.
                    dbConn.Update(cs);
                    await dbConn.CloseAsync();
                }
            }
            catch (Exception e)
            {
                return 0;
            }
            return status;
        }
        public async Task<int> ProcessReversedFunction(int id, int status) 
        {
            try
            {
                using (var dbConn = new SqlConnection(_configuration.GetConnectionString("ConnectionString")))
                {
                    await dbConn.OpenAsync(); // mở sync

                    var query = dbConn.QueryBuilder($"select * from CustomerCheckout WHERE CustomerCheckout_Id = {id}");
                    var cs = await query.QueryFirstOrDefaultAsync<CustomerCheckout>(); /// lấy du lieu tu db.
                    status -= 1;
                    cs.CustomerCheckout_Status = (Status)status; // vì status đang là enum nên phải ép kiểu.
                    dbConn.Update(cs);
                    await dbConn.CloseAsync();
                }
            }
            catch (Exception e)
            {
                return 0;
            }
            return status;
        }
        public async Task<ResponseMessageObject<ProductCheckout_ViewModel>> Details(int CustomerCheckout_Id)
        {
            var data = new ResponseMessageObject<ProductCheckout_ViewModel>();
            data.Data = new ProductCheckout_ViewModel();
            try
            {
                using (var dbConn = new SqlConnection(_configuration.GetConnectionString("ConnectionString")))
                {
                    await dbConn.OpenAsync(); // mở sync

                    var query = dbConn.QueryBuilder($"select * from ProductCheckout WHERE CustomerCheckout_Id = {CustomerCheckout_Id}");

                    data.Data.productcheckout = await query.QueryAsync<ProductCheckout>();

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


