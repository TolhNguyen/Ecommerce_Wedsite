using DapperQueryBuilder;
using Ecommerce_Wedsite.Models.Entities;
using Ecommerce_Wedsite.Models.Helpers.Response;
using Ecommerce_Wedsite.Models.ViewModel;
using Ecommerce_Wedsite.Service.Helpers.Configuration;
using Microsoft.AspNetCore.DataProtection;
using System.Data.SqlClient;

namespace Ecommerce_Wedsite.Service.WebApp
{
    public interface IProductTypeAdminService // Tạo Interface
    {

        Task<ResponseMessageObject<ProductType_ViewModel>> Service_Test(); //Model lớn chứa Model nhỏ. Tạo Phương Thức     
    }
    public class ProductTypeAdminService : IProductTypeAdminService // Thừa kế các thuộc tính từ Interface 
    {
        private readonly IConfigManagerService _configuration;
        public ProductTypeAdminService(IConfigManagerService configuration)
        {
            _configuration = configuration;
        }

        // Nhận đường dẫn từ View là tên các sản phẩm để vào sản phẩm tương ứng producthref
        public async Task<ResponseMessageObject<ProductType_ViewModel>> Service_Test()
        {
            var data = new ResponseMessageObject<ProductType_ViewModel>();
            data.Data = new ProductType_ViewModel();
            try
            {
                using (var dbConn = new SqlConnection(_configuration.GetConnectionString("ConnectionString")))
                {
                    await dbConn.OpenAsync(); // mở sync

                    var query = dbConn.QueryBuilder($"SELECT * FROM ProductType");

                    data.Data.producttype = await query.QueryAsync<ProductType>();

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


