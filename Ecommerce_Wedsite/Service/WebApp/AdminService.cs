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
    public interface IAdminService // Tạo Interface
    {
        Task<ResponseMessageObject<Admin_ViewModel>> Service_Test(string? userName, string? passWord); //Model lớn chứa Model nhỏ. Tạo Phương Thức     
        Task<ResponseMessageObject<Admin_ViewModel>> AdminInfo(int idcookie);
    }
    public class AdminService : IAdminService // Thừa kế các thuộc tính từ Interface 
    {
        private readonly IConfigManagerService _configuration;
        public AdminService(IConfigManagerService configuration)
        {
            _configuration = configuration;
        }

        public async Task<ResponseMessageObject<Admin_ViewModel>> Service_Test(string? userName, string? passWord) // Code kiểm tra tài khoản đúng / sai
        {
            var data = new ResponseMessageObject<Admin_ViewModel>();
            data.Data = new Admin_ViewModel();
            try
            {
                using (var dbConn = new SqlConnection(_configuration.GetConnectionString("ConnectionString"))) // liên kết database
                {
                    await dbConn.OpenAsync(); // mở sync

                    var query = dbConn.QueryBuilder($"select * from Admin where Admin_Username = '{userName}' and Admin_Password = '{passWord}'");
                    
                    // == null vì k có / lấy ra được dữ liệu từ sql => sai 

                    data.Data.admin = await query.QueryAsync<Admin>();

                    if (data.Data.admin == null || !data.Data.admin.Any()) return new ResponseMessageObject<Admin_ViewModel> { success = false, message = "Thất bại", statusCode = 2 }; // nếu k có tài khoản đó => false

                    await dbConn.CloseAsync(); // đóng sync sau khi sử dụng
                }
            }
            catch (Exception e) // Gặp lỗi, sai
            {
                data.message = e.Message;
                data.success = false;
                return new ResponseMessageObject<Admin_ViewModel> { success = false, message = e.Message, statusCode = 2 }; // false
            }
             return new ResponseMessageObject<Admin_ViewModel> { success = true, message = "Thành Công", statusCode = 0 }; // nếu có, đúng tk => true 
        }
        public async Task<ResponseMessageObject<Admin_ViewModel>> AdminInfo(int idcookie) // Code kiểm tra tài khoản đúng / sai
        {
            var data = new ResponseMessageObject<Admin_ViewModel>();
            data.Data = new Admin_ViewModel();
            try
            {
                using (var dbConn = new SqlConnection(_configuration.GetConnectionString("ConnectionString"))) // liên kết database
                {
                    await dbConn.OpenAsync(); // mở sync

                    var query = dbConn.QueryBuilder($"select * from Admin where Admin_Id = '{idcookie}'");

                    // == null vì k có / lấy ra được dữ liệu từ sql => sai 

                    data.Data.admin = await query.QueryAsync<Admin>();
                    await dbConn.CloseAsync(); // đóng sync sau khi sử dụng
                }
            }
            catch (Exception e) // Gặp lỗi, sai
            {
                data.message = e.Message;
                data.success = false;
            }
            return data;
        }
    }
}
