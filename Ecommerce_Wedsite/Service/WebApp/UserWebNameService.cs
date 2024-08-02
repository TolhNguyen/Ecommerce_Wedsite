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
    public interface IUserWebNameService // Tạo Interface
    {
        Task<string> WebName(string? userName, string webname); //Model lớn chứa Model nhỏ. Tạo Phương Thức
        Task<int> WebNameAdminId(string? userName, int id);
        Task<string> WebNameAdmin(string name, int id);
    }
    public class UserWebNameService : IUserWebNameService // Thừa kế các thuộc tính từ Interface 
    {
        private readonly IConfigManagerService _configuration;
        public UserWebNameService(IConfigManagerService configuration)
        {
            _configuration = configuration;
        }

        public async Task<string> WebName(string? userName, string webname) // Code kiểm tra tài khoản đúng / sai
        {
            try
            {
                using (var dbConn = new SqlConnection(_configuration.GetConnectionString("ConnectionString"))) // liên kết database
                {
                    await dbConn.OpenAsync(); // mở sync

                    var query = dbConn.QueryBuilder($"select * from UserLogin where UserLogin_Name = '{userName}'");

                    // == null vì k có / lấy ra được dữ liệu từ sql => sai 

                    var wb = await query.QueryFirstOrDefaultAsync<UserLogin>();

                    webname = wb.UserLogin_WebName;

                    await dbConn.CloseAsync(); // đóng sync sau khi sử dụng
                }
            }
            catch (Exception e) // Gặp lỗi, sai
            {
                return "";
            }
            return webname;
        }
        public async Task<int> WebNameAdminId(string? userName, int id) // Code kiểm tra tài khoản đúng / sai
        {
            try
            {
                using (var dbConn = new SqlConnection(_configuration.GetConnectionString("ConnectionString"))) // liên kết database
                {
                    await dbConn.OpenAsync(); // mở sync

                    var query = dbConn.QueryBuilder($"select * from Admin where Admin_Username = '{userName}'");

                    // == null vì k có / lấy ra được dữ liệu từ sql => sai 
                    var admin = await query.QueryFirstOrDefaultAsync<Admin>();
                    id = admin.Admin_Id;
                    await dbConn.CloseAsync(); // đóng sync sau khi sử dụng
                }
            }
            catch (Exception e) // Gặp lỗi, sai
            {
                return 0; // chỉ trả về là 1 biến tên thôi
            }
            return id;
        }
        public async Task<string> WebNameAdmin(string name, int id) // Code kiểm tra tài khoản đúng / sai
        {
            try
            {
                using (var dbConn = new SqlConnection(_configuration.GetConnectionString("ConnectionString"))) // liên kết database
                {
                    await dbConn.OpenAsync(); // mở sync

                    var query = dbConn.QueryBuilder($"select * from Admin where Admin_Id = '{id}'");

                    // == null vì k có / lấy ra được dữ liệu từ sql => sai 
                    var admin = await query.QueryFirstOrDefaultAsync<Admin>();
                    name = admin.Admin_Name;
                    await dbConn.CloseAsync(); // đóng sync sau khi sử dụng
                }
            }
            catch (Exception e) // Gặp lỗi, sai
            {
                return ""; // chỉ trả về là 1 biến tên thôi
            }
            return name;
        }
    }
}


