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
    public interface IProductAdminConditionService // Tạo Interface
    {
        Task<ResponseMessageObject<Product_ViewModel>> CheckCondition(int id, int condition); //Model lớn chứa Model nhỏ. Tạo Phương Thức     
    }
    public class ProductAdminConditionService : IProductAdminConditionService // Thừa kế các thuộc tính từ Interface 
    {
        private readonly IConfigManagerService _configuration;
        public ProductAdminConditionService(IConfigManagerService configuration)
        {
            _configuration = configuration;
        }

        public async Task<ResponseMessageObject<Product_ViewModel>> CheckCondition(int id, int condition) // Lấy dữ liệu model từ db lên và hành động vào
        {
            var data = new ResponseMessageObject<Product_ViewModel>();
            data.Data = new Product_ViewModel();
            try
            {
                using (var dbConn = new SqlConnection(_configuration.GetConnectionString("ConnectionString")))
                {
                    await dbConn.OpenAsync(); // mở sync

                    var query = dbConn.QueryBuilder($"select * from Product where Product_Id = {id}");

                    var productcondition = await query.QueryFirstOrDefaultAsync<Product>();

                    productcondition.Product_Condition = (Bit4)condition; // convert từ int sang Bit4

                    var it = dbConn.Update(productcondition); // hành động và lưu model vào db

                    await dbConn.CloseAsync();
                }
            }
            catch (Exception e)
            {
                data.message = e.Message;
                data.success = false;
            }
            return data;
        }
    }
}



