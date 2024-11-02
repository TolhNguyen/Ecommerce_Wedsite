using DapperQueryBuilder;
using Ecommerce_Wedsite.Models.Entities;
using Ecommerce_Wedsite.Models.Helpers.Response;
using Ecommerce_Wedsite.Models.ViewModel;
using Ecommerce_Wedsite.Service.Helpers.Configuration;
using Microsoft.AspNetCore.DataProtection;
using System.Data.SqlClient;
using Dapper;
using Dapper.Contrib.Extensions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Ecommerce_Wedsite.Service.WebApp
{
    public interface IProductCheckoutService // Tạo Interface
    {
        Task<ShopCard_ViewModel> ProductCheckoutFunction(ShopCard_ViewModel card, int cscheckoutid); //Model lớn chứa Model nhỏ. Tạo Phương Thức     
    }
    public class ProductCheckoutService : IProductCheckoutService // Thừa kế các thuộc tính từ Interface 
    {
        private readonly IConfigManagerService _configuration;
        private readonly IProductCheckoutQuantityService _productcheckoutquantityService; // cách thêm service khác vào sử dụng
        public ProductCheckoutService(IConfigManagerService configuration, IProductCheckoutQuantityService productcheckoutquantityService)
        {
            _configuration = configuration;
            _productcheckoutquantityService = productcheckoutquantityService;
        }
        public async Task<ShopCard_ViewModel> ProductCheckoutFunction(ShopCard_ViewModel card, int cscheckoutid) // tạo riêng 1 service. (bỏ static r)
        {
            using (var dbConn = new SqlConnection(_configuration.GetConnectionString("ConnectionString")))
            {
                await dbConn.OpenAsync(); // mở sync

                var productcheckout = new ProductCheckout();

                productcheckout.CustomerCheckout_Id = cscheckoutid;

                foreach (var item in card.product_card) // foreach từng sp trong giỏ hàng 
                {
                    // thêm try catch:
                    var idcheck = item.id;
                    var qtycheck = item.qty;

                    await _productcheckoutquantityService.ProductCheckoutQuantityFunction(idcheck, qtycheck); // check và giảm số lượng từng sp qua for

                    productcheckout.ProductCheckout_Id = idcheck;
                    productcheckout.ProductCheckout_Quantity = qtycheck;
                    
                    productcheckout.ProductCheckout_Name = item.name;
                    productcheckout.ProductCheckout_Price = item.price;
                    productcheckout.ProductCheckout_Img = item.img;
                    
                    
                    dbConn.Insert(productcheckout); // gắn id cus vào và lưu model mới db
                }
                await dbConn.CloseAsync();
            }
            return card;
        }
    }
}
