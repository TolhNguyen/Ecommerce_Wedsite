using DapperQueryBuilder;
using Ecommerce_Wedsite.Models.Entities;
using Ecommerce_Wedsite.Models.Helpers.Response;
using Ecommerce_Wedsite.Models.ViewModel;
using Ecommerce_Wedsite.Service.Helpers.Configuration;
using Microsoft.AspNetCore.DataProtection;
using System.Data.SqlClient;
using Dapper;
using Dapper.Contrib.Extensions;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce_Wedsite.Service.WebApp
{
    public interface ICreateCartCookieService // Tạo Interface
    {
        Task<ShopCard_ViewModel> CreateCookieFunction(ShopCard_ViewModel card, Product product_ViewModels, int qty); //Model lớn chứa Model nhỏ. Tạo Phương Thức     
    }
    public class CreateCartCookieService : ICreateCartCookieService // Thừa kế các thuộc tính từ Interface 
    {
        private readonly IConfigManagerService _configuration;
        private readonly IAddCartCookieService _addcartcookieService;
        
        public CreateCartCookieService(IConfigManagerService configuration, IAddCartCookieService addcartcookieService)
        {
            _configuration = configuration;
            _addcartcookieService = addcartcookieService;
        }

        public async Task<ShopCard_ViewModel> CreateCookieFunction(ShopCard_ViewModel card, Product product_ViewModels, int qty) // Lấy dữ liệu model từ db lên và hành động vào
        {

            if (!card.product_card.Any()) // Kiem tra giỏ có đang trống hay k. Lấy thông tin product lên ktra. Nếu trống:
                // tinh toan va them san pham mới
                card = await _addcartcookieService.them_san_pham_vao_goi_hang(card, product_ViewModels, qty);
            else
            {
                var check = new Producd_ShopCard();
                foreach (var item in card.product_card) // foreach từng sp trong giỏ hàng 
                {
                    if (item.id == product_ViewModels.Product_Id) // ktra có id trùng k
                    {
                        check = item; // nếu có, gắn biến check là item (có id giống) đó
                        break;
                    }
                }
                if (check.name == null) // gio hang khong co item trùng. Biến check k có id giống nhau
                    card = await _addcartcookieService.them_san_pham_vao_goi_hang(card, product_ViewModels, qty); // thì thêm sp mới vào giỏ hàng

                else // gio hang co item trùng, san them vao. Cộng dồn sp
                {
                    foreach (var item in card.product_card) // foreach từng sp trong giỏ hàng 
                    {
                        if (item.id == product_ViewModels.Product_Id) // nếu có id cũ. code add tròng sp vào (k chạy service addcart như trên)
                        {
                            item.qty = item.qty + qty; // thì + dồn số lượng mới vào sl cũ 
                                    
                            item.price = item.price + product_ViewModels.Product_Price * qty; // tiền mới = tiền cũ + tiền sp trùng mới * sl 
                            card.total_price = card.total_price + product_ViewModels.Product_Price * qty; // total mới = total cũ + tiền sp trùng mới * sl 
                            break;
                        }
                    }
                }
            }
            return card;
        }
    }
}
// Có thể gom 2 cái service Create và Add cookie vào làm 1, nếu giống chung 1 model mới đc


