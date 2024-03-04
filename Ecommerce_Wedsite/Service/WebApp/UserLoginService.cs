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
    public interface IUserLoginService // Tạo Interface
    {
        Task<ResponseMessageObject<UserLogin_ViewModel>> Login(string? userName, string? passWord); //Model lớn chứa Model nhỏ. Tạo Phương Thức     
    }
    public class UserLoginService : IUserLoginService // Thừa kế các thuộc tính từ Interface 
    {
        private readonly IConfigManagerService _configuration;
        public UserLoginService(IConfigManagerService configuration)
        {
            _configuration = configuration;
        }

        public async Task<ResponseMessageObject<UserLogin_ViewModel>> Login(string? userName, string? passWord) // Code kiểm tra tài khoản đúng / sai
        {
            var data = new ResponseMessageObject<UserLogin_ViewModel>();
            data.Data = new UserLogin_ViewModel();
            try
            {
                using (var dbConn = new SqlConnection(_configuration.GetConnectionString("ConnectionString"))) // liên kết database
                {
                    await dbConn.OpenAsync(); // mở sync

                    var query = dbConn.QueryBuilder($"select * from UserLogin where UserLogin_Name = '{userName}' and UserLogin_Password = '{passWord}'");

                    // == null vì k có / lấy ra được dữ liệu từ sql => sai 

                    data.Data.userlogin = await query.QueryAsync<UserLogin>();

                    if (data.Data.userlogin == null || !data.Data.userlogin.Any()) return new ResponseMessageObject<UserLogin_ViewModel> { success = false, message = "Thất bại", statusCode = 2 }; // nếu k có tài khoản đó => false

                    await dbConn.CloseAsync(); // đóng sync sau khi sử dụng
                }
            }
            catch (Exception e) // Gặp lỗi, sai
            {
                data.message = e.Message;
                data.success = false;
                return new ResponseMessageObject<UserLogin_ViewModel> { success = false, message = e.Message, statusCode = 2 }; // false

            }
            return new ResponseMessageObject<UserLogin_ViewModel> { success = true, message = "Thành Công", statusCode = 0 }; // nếu có, đúng tk => true 
        }
    }
}

