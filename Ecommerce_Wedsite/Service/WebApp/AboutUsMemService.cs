using DapperQueryBuilder;
using Ecommerce_Wedsite.Models.Entities;
using Ecommerce_Wedsite.Models.Helpers.Response;
using Ecommerce_Wedsite.Models.ViewModel;
using Ecommerce_Wedsite.Service.Helpers.Configuration;
using Microsoft.AspNetCore.DataProtection;
using System.Data.SqlClient;

namespace Ecommerce_Wedsite.Service.WebApp
{
    public interface IAboutUsMemService 
    {

        Task<ResponseMessageObject<AboutUsMem_ViewModel>> AboutUsMem_Service(); 
    }
    public class AboutUsMemService : IAboutUsMemService 
    {
        private readonly IConfigManagerService _configuration;
        public AboutUsMemService(IConfigManagerService configuration)
        {
            _configuration = configuration;
        }

        public async Task<ResponseMessageObject<AboutUsMem_ViewModel>> AboutUsMem_Service() // Chưa có controller.
        {
            var data = new ResponseMessageObject<AboutUsMem_ViewModel>();
            data.Data = new AboutUsMem_ViewModel();
            try
            {
                using (var dbConn = new SqlConnection(_configuration.GetConnectionString("ConnectionString")))
                {
                    await dbConn.OpenAsync(); // mở sync

                    var query = dbConn.QueryBuilder($"SELECT * FROM AboutUsMem");

                    data.Data.aboutusmem = await query.QueryAsync<AboutUsMem>();

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
