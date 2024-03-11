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
    public interface IGetIdWebNameService // Tạo Interface
    {
        Task<int> GetIdWebName(string UserLogin_WebName, int idwebname); //Model lớn chứa Model nhỏ. Tạo Phương Thức     
    }
    public class GetIdWebNameService : IGetIdWebNameService // Thừa kế các thuộc tính từ Interface 
    {
        private readonly IConfigManagerService _configuration;
        public GetIdWebNameService(IConfigManagerService configuration)
        {
            _configuration = configuration;
        }

        public async Task<int> GetIdWebName(string UserLogin_WebName, int idwebname) // Code kiểm tra tài khoản đúng / sai
        {
            try
            {
                using (var dbConn = new SqlConnection(_configuration.GetConnectionString("ConnectionString"))) // liên kết database
                {
                    await dbConn.OpenAsync(); // mở sync

                    var query = dbConn.QueryBuilder($"select * from UserLogin where UserLogin_WebName = '{UserLogin_WebName}'");

                    // == null vì k có / lấy ra được dữ liệu từ sql => sai 

                    var wb = await query.QueryFirstOrDefaultAsync<UserLogin>();

                    idwebname = wb.UserLogin_Id;

                    await dbConn.CloseAsync(); // đóng sync sau khi sử dụng
                }
            }
            catch (Exception e) // Gặp lỗi, sai
            {
                return 0;
            }
            return idwebname;
        }
    }
}
