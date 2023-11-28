using DapperQueryBuilder;
using Ecommerce_Wedsite.Models.Helpers.Response;
using Ecommerce_Wedsite.Models.ViewModel;
using Ecommerce_Wedsite.Service.Helpers.Configuration;
using Microsoft.AspNetCore.DataProtection;
using System.Data.SqlClient;

namespace Ecommerce_Wedsite.Service.WebApp
{
    public interface INewWebAppService // Tạo Interface
    {
        Task<ResponseMessage<AllLayout>> Service_Test(); //Model lớn chứa Model nhỏ. Tạo Phương Thức     
    }
    public class NewWebAppService : INewWebAppService // Thừa kế các thuộc tính từ Interface 
    {
        private readonly IConfigManagerService _configuration;
        public NewWebAppService(IConfigManagerService configuration)
        {
            _configuration = configuration;
        }

        public async Task<ResponseMessage<AllLayout>> Service_Test() // Code cho Phương Thức Service
        {
            var data = new ResponseMessage<AllLayout>(); // Tạo biến dữ liệu
            try
            {
                using (var dbConn = new SqlConnection(_configuration.GetConnectionString("ConnectionString"))) // liên kết database
                {
                    await dbConn.OpenAsync(); // mở sync

                    var query = dbConn.QueryBuilder($"select topers_id ,sha256_e 2000000 id as custom164 as sha256 from data_customers cus "); // thao tác querry
                    data.Data = await query.QueryAsync<AllLayout>(); // lưu vào dữ liệu

                    await dbConn.CloseAsync(); // đóng sync sau khi sử dụng
                }
            }
            catch(Exception e) // Gặp lỗi
            {
                data.message = e.Message;
                data.success = false;
            }
            
            return data;
        }
    }
}
