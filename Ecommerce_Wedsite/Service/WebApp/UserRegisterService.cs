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
    public interface IUserRegisterService // Tạo Interface
    {
        Task<ResponseMessageObject<UserLogin_ViewModel>> Register(UserLogin userlogin); //Model lớn chứa Model nhỏ. Tạo Phương Thức     
    }
    public class UserRegisterService : IUserRegisterService // Thừa kế các thuộc tính từ Interface 
    {
        private readonly IConfigManagerService _configuration;
        public UserRegisterService(IConfigManagerService configuration)
        {
            _configuration = configuration;
        }

        public async Task<ResponseMessageObject<UserLogin_ViewModel>> Register(UserLogin userlogin) // Code kiểm tra tài khoản đúng / sai
        {
            var data = new ResponseMessageObject<UserLogin_ViewModel>();
            data.Data = new UserLogin_ViewModel();
            try
            {
                using (var dbConn = new SqlConnection(_configuration.GetConnectionString("ConnectionString"))) // liên kết database
                {
                    await dbConn.OpenAsync(); // mở sync

                    var query = dbConn.QueryBuilder($"select * from UserLogin where UserLogin_Name = '{userlogin.UserLogin_Name}'");

                    var userlg = await query.QueryFirstOrDefaultAsync<UserLogin>();

                    if (userlg != null)
                    {
                        return new ResponseMessageObject<UserLogin_ViewModel> { success = false}; // nếu có tài khoản đó => bị trùng => false
                    }
                    else // Cần sửa thành truyền 1 model ajax data và insert thẳng model luôn chứ k tách ra như này.
                    {
                        dbConn.Insert(userlogin);
                    }

                    await dbConn.CloseAsync(); // đóng sync sau khi sử dụng
                }
            }
            catch // Gặp lỗi, sai
            {
                return new ResponseMessageObject<UserLogin_ViewModel> { success = false}; // false

            }
            return new ResponseMessageObject<UserLogin_ViewModel> { success = true}; // nếu có, đúng tk => true 
        }
    }
}


