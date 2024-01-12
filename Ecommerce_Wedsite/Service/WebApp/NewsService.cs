using DapperQueryBuilder;
using Ecommerce_Wedsite.Models.Entities;
using Ecommerce_Wedsite.Models.Helpers.Response;
using Ecommerce_Wedsite.Models.ViewModel;
using Ecommerce_Wedsite.Service.Helpers.Configuration;
using Microsoft.AspNetCore.DataProtection;
using System.Data.SqlClient;

namespace Ecommerce_Wedsite.Service.WebApp
{
    public interface INewsService // Tạo Interface
    {
        Task<ResponseMessageObject<News_ViewModel>> Service_Test(int? ListNews_Id = 0); //Model lớn chứa Model nhỏ. Tạo Phương Thức     
    }
    public class NewsService : INewsService // Thừa kế các thuộc tính từ Interface 
    {
        private readonly IConfigManagerService _configuration;
        public NewsService(IConfigManagerService configuration)
        {
            _configuration = configuration;
        }

        public async Task<ResponseMessageObject<News_ViewModel>> Service_Test(int? ListNews_Id = 0)
        {
            var data = new ResponseMessageObject<News_ViewModel>();
            data.Data = new News_ViewModel();
            try
            {
                using (var dbConn = new SqlConnection(_configuration.GetConnectionString("ConnectionString")))
                {
                    await dbConn.OpenAsync(); // mở sync

                    var query = dbConn.QueryBuilder($"SELECT * FROM News where ListNews_Id = {ListNews_Id}");

                    data.Data.news = await query.QueryAsync<News>();

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


