using DapperQueryBuilder;
using Ecommerce_Wedsite.Models.Entities;
using Ecommerce_Wedsite.Models.Helpers.Response;
using Ecommerce_Wedsite.Models.ViewModel;
using Ecommerce_Wedsite.Service.Helpers.Configuration;
using Microsoft.AspNetCore.DataProtection;
using System.Data.SqlClient;

namespace Ecommerce_Wedsite.Service.WebApp
{
    public interface IAboutUsService // Tạo Interface
    {

        Task<ResponseMessageObject<AboutUs_ViewModel>> AboutUs_Service(); //Model lớn chứa Model nhỏ. Tạo Phương Thức     
    }
    public class AboutUsService : IAboutUsService // Thừa kế các thuộc tính từ Interface 
    {
        private readonly IConfigManagerService _configuration;
        public AboutUsService(IConfigManagerService configuration)
        {
            _configuration = configuration;
        }

        public async Task<ResponseMessageObject<AboutUs_ViewModel>> AboutUs_Service()
        {
            var data = new ResponseMessageObject<AboutUs_ViewModel>();
            data.Data = new AboutUs_ViewModel();
            try
            {
                using (var dbConn = new SqlConnection(_configuration.GetConnectionString("ConnectionString")))
                {
                    await dbConn.OpenAsync(); // mở sync

                    var query = dbConn.QueryBuilder($"SELECT * FROM AboutUs");

                    data.Data.aboutus = await query.QueryAsync<AboutUs>();

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
