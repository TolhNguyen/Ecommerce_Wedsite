using DapperQueryBuilder;
using Ecommerce_Wedsite.Models.Entities;
using Ecommerce_Wedsite.Models.Helpers.Response;
using Ecommerce_Wedsite.Models.ViewModel;
using Ecommerce_Wedsite.Service.Helpers.Configuration;
using Microsoft.AspNetCore.DataProtection;
using System.Data.SqlClient;

namespace Ecommerce_Wedsite.Service.WebApp
{
    public interface IProductCookieService // Tạo Interface
    {

        Task<ResponseMessageObject<Product_ViewModel>> Service_Test(int id); //Model lớn chứa Model nhỏ. Tạo Phương Thức     
    }
    public class ProductCookieService : IProductCookieService // Thừa kế các thuộc tính từ Interface 
    {
        private readonly IConfigManagerService _configuration;
        public ProductCookieService(IConfigManagerService configuration)
        {
            _configuration = configuration;
        }

        // Nhận đường dẫn từ View là tên các sản phẩm để vào sản phẩm tương ứng producthref. 
        public async Task<ResponseMessageObject<Product_ViewModel>> Service_Test(int id)
        {
            var data = new ResponseMessageObject<Product_ViewModel>();
            data.Data = new Product_ViewModel();
            try
            {
                using (var dbConn = new SqlConnection(_configuration.GetConnectionString("ConnectionString")))
                {
                    await dbConn.OpenAsync(); // mở sync

                    var query = dbConn.QueryBuilder($"select * from Product where Product_Id = {id}");

                    data.Data.product = await query.QueryAsync<Product>();

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


