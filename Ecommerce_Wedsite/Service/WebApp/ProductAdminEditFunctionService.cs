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
    public interface IProductAdminEditFunctionService // Tạo Interface
    {
        Task<ResponseMessageObject<string>> Service_Test(Product productitem); //Model lớn chứa Model nhỏ. Tạo Phương Thức     
    }
    public class ProductAdminEditFunctionService : IProductAdminEditFunctionService // Thừa kế các thuộc tính từ Interface 
    {
        private readonly IConfigManagerService _configuration;
        public ProductAdminEditFunctionService(IConfigManagerService configuration)
        {
            _configuration = configuration;
        }

        public async Task<ResponseMessageObject<string>> Service_Test(Product productitem) // Lấy dữ liệu model từ db lên và hành động vào.
        {
            try
            {
                using (var dbConn = new SqlConnection(_configuration.GetConnectionString("ConnectionString")))
                {
                    await dbConn.OpenAsync(); // mở sync

                    var query = dbConn.QueryBuilder($"select top 1 * from Product where Product_Id = {productitem.Product_Id}"); // select theo top id
                    var product = await query.QueryFirstOrDefaultAsync<Product>(); /// lấy du lieu tu db. 
                    
                    //tạo biến lưu dữ liệu mới vào. Dùng QueryFODA để lấy những column duy nhất ra.

                    //  map du lieu db  cli
                    //  mapper 
                    // map tay 

                    //header.Header_Title = headeritem.Header_Title;
                    
                    product.Product_Id = productitem.Product_Id;
                    product.Product_Manufacturers = productitem.Product_Manufacturers;
                    product.Product_Name = productitem.Product_Name;
                    product.Product_Price = productitem.Product_Price;
                    product.Product_PromoPrice = productitem.Product_PromoPrice;
                    product.Product_Info = productitem.Product_Info; 
                    product.Logo_Id = productitem.Logo_Id;
                    product.Picture_Id = productitem.Picture_Id;
                    product.Product_Href = productitem.Product_Href;
                    product.ProductType_Id = productitem.ProductType_Id;
                    product.Product_Condition = productitem.Product_Condition;
                    product.Product_Quantity = productitem.Product_Quantity;
                    product.Product_Info2 = productitem.Product_Info2;
                    product.Product_ShortDescription = productitem.Product_ShortDescription;
                    product.Product_Size = productitem.Product_Size;
                    product.Product_Img = productitem.Product_Img;

                    var it = dbConn.Update(product);

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


