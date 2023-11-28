using DapperQueryBuilder;
using Ecommerce_Wedsite.Models.Entities;
using Ecommerce_Wedsite.Models.Helpers.Response;
using Ecommerce_Wedsite.Models.ViewModel;
using Ecommerce_Wedsite.Service.Helpers.Configuration;
using Microsoft.AspNetCore.DataProtection;
using System.Data.SqlClient;

namespace Ecommerce_Wedsite.Service.WebApp
{
    public interface ISubProductService // Tạo Interface
    {

        Task<ResponseMessageObject<SubProduct_ViewModel>> Service_Test(); //Model lớn chứa Model nhỏ. Tạo Phương Thức     
    }
    public class SubProductService : ISubProductService // Thừa kế các thuộc tính từ Interface 
    {
        private readonly IConfigManagerService _configuration;
        public SubProductService(IConfigManagerService configuration)
        {
            _configuration = configuration;
        }

        public async Task<ResponseMessageObject<SubProduct_ViewModel>> Service_Test()
        {
            var data = new ResponseMessageObject<SubProduct_ViewModel>();
            data.Data = new SubProduct_ViewModel();
            try
            {
                using (var dbConn = new SqlConnection(_configuration.GetConnectionString("ConnectionString")))
                {
                    await dbConn.OpenAsync(); // mở sync

                    var query = dbConn.QueryBuilder($"SELECT TOP 3 * FROM Product order by NEWID() asc");

                    data.Data.subproduct = await query.QueryAsync<Product>();

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
