using DapperQueryBuilder;
using Ecommerce_Wedsite.Models.Entities;
using Ecommerce_Wedsite.Models.Helpers.Response;
using Ecommerce_Wedsite.Models.ViewModel;
using Ecommerce_Wedsite.Service.Helpers.Configuration;
using Microsoft.AspNetCore.DataProtection;
using System.Data.SqlClient;
using Dapper;
using Dapper.Contrib.Extensions;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce_Wedsite.Service.WebApp
{
    public interface IProductAdminCreateService // Tạo Interface
    {
        Task<ResponseMessageObject<string>> Service_Test(Product productitem, IFormFile productimg); //Model lớn chứa Model nhỏ. Tạo Phương Thức     
    }
    public class ProductAdminCreateService : IProductAdminCreateService // Thừa kế các thuộc tính từ Interface 
    {
        private readonly IConfigManagerService _configuration;
        public ProductAdminCreateService(IConfigManagerService configuration)
        {
            _configuration = configuration;
        }

        public async Task<ResponseMessageObject<string>> Service_Test(Product productitem, [FromForm] IFormFile productimg) // Lấy dữ liệu model từ db lên và hành động vào
        {
            try
            {
                using (var dbConn = new SqlConnection(_configuration.GetConnectionString("ConnectionString")))
                {
                    await dbConn.OpenAsync(); // mở sync

                    if (productimg != null && productimg.Length > 0)
                    {
                        var imgPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/Picture", productimg.FileName);

                        using (var stream = new FileStream(imgPath, FileMode.Create))
                        {
                            await productimg.CopyToAsync(stream); // đã chạy được là lưu được file ảnh cả cho database và trên file Picture
                        }
                    }
                    var it = dbConn.Insert(productitem); // hành động và lưu model vào db

                    await dbConn.CloseAsync();
                }
            }
            catch (Exception e)
            {
                return new ResponseMessageObject<string> { success = false, message = e.Message };
            }
            return new ResponseMessageObject<string> { success = true, message = "Thành công" };
        }
    }
}



