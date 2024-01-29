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
    public interface IProductCheckoutQuantityService // Tạo Interface
    {
        Task<int> ProductCheckoutQuantityFunction(int idcheck, int qtycheck); //Model lớn chứa Model nhỏ. Tạo Phương Thức     
    }
    public class ProductCheckoutQuantityService : IProductCheckoutQuantityService // Thừa kế các thuộc tính từ Interface 
    {
        private readonly IConfigManagerService _configuration;
        public ProductCheckoutQuantityService(IConfigManagerService configuration)
        {
            _configuration = configuration;
        }
        public async Task<int> ProductCheckoutQuantityFunction(int idcheck, int qtycheck) // tạo riêng 1 service. (bỏ static r)
        {
            try
            {
                using (var dbConn = new SqlConnection(_configuration.GetConnectionString("ConnectionString")))
                {
                    await dbConn.OpenAsync(); // mở sync

                    var query = dbConn.QueryBuilder($"select top 1 * from Product where Product_Id = '{idcheck}'");

                    var productcheck = await query.QueryFirstOrDefaultAsync<Product>();

                    productcheck.Product_Quantity -= qtycheck; // tính số lượng sp mới
                    dbConn.Update(productcheck);
                    
                    await dbConn.CloseAsync();
                }
            }
            catch (Exception e)
            {
                return qtycheck = 0;
            }
            return qtycheck;
        }
    }
}
