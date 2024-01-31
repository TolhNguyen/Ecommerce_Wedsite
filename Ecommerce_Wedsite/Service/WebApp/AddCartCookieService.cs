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
    public interface IAddCartCookieService // Tạo Interface
    {
        Task<ShopCard_ViewModel> them_san_pham_vao_goi_hang(ShopCard_ViewModel card, Product product_ViewModels, int qty); //Model lớn chứa Model nhỏ. Tạo Phương Thức     
    }
    public class AddCartCookieService : IAddCartCookieService // Thừa kế các thuộc tính từ Interface 
    {
        private readonly IConfigManagerService _configuration;
        private readonly IProductCartImgService _productcartimgService;
        public AddCartCookieService(IConfigManagerService configuration, IProductCartImgService productcartimgService)
        {
            _configuration = configuration;
            _productcartimgService = productcartimgService;
        }
        public async Task<ShopCard_ViewModel> them_san_pham_vao_goi_hang(ShopCard_ViewModel card, Product product_ViewModels, int qty) // tạo riêng 1 service. (bỏ static r)
        {

            var product_item = new Producd_ShopCard(); // tạo biến sp mới là thay đổi của sp cũ (đã có trong giỏ hàng)
            product_item.id = product_ViewModels.Product_Id;
            product_item.name = product_ViewModels.Product_Name;
            product_item.qty = qty;
            product_item.imgid = product_ViewModels.Picture_Id;
            product_item.img = await _productcartimgService.ProductCartImgFunction(product_item.imgid);
            if (product_ViewModels.Product_PromoPrice != 0)
            {
                product_item.price = product_ViewModels.Product_PromoPrice * qty;
            }
            else
            {
                product_item.price = product_ViewModels.Product_Price * qty;
            }

            card.product_card.Add(product_item); // thay biến mới này vào giỏ hàng (card chứa product_card và total price)

            card.total_price = card.total_price + product_item.price; // thay đổi total price

            return card;
        }
    }
}



