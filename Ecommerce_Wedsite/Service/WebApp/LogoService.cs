using DapperQueryBuilder;
using Ecommerce_Wedsite.Models.Entities;
using Ecommerce_Wedsite.Models.Helpers.Response;
using Ecommerce_Wedsite.Models.ViewModel;
using Ecommerce_Wedsite.Service.Helpers.Configuration;
using Microsoft.AspNetCore.DataProtection;
using System.Data.SqlClient;

namespace Ecommerce_Wedsite.Service.WebApp
{
    public interface ILogoService
    {
        Task<ResponseMessageObject<Logo_ViewModel>> Service_Test();
    }
    public class LogoService : ILogoService // Thừa kế các thuộc tính từ Interface 
    {
        private readonly IConfigManagerService _configuration;
        public LogoService(IConfigManagerService configuration)
        {
            _configuration = configuration;
        }

        public async Task<ResponseMessageObject<Logo_ViewModel>> Service_Test() // Code cho Phương Thức Service
        {
            var data = new ResponseMessageObject<Logo_ViewModel>();
            data.Data = new Logo_ViewModel();
            try
            {
                using (var dbConn = new SqlConnection(_configuration.GetConnectionString("ConnectionString"))) // liên kết database
                {
                    await dbConn.OpenAsync();

                    var query = dbConn.QueryBuilder($"select * from Logo");

                    data.Data.logo = await query.QueryAsync<Logo>();

                    await dbConn.CloseAsync(); // đóng sync sau khi sử dụng
                }
            }
            catch (Exception e) // Gặp lỗi
            {
                data.message = e.Message;
                data.success = false;
            }
            return data;
        }
    }
}
