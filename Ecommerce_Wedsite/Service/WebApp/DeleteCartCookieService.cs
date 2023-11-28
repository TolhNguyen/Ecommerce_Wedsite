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
    public interface IDeleteCartCookieService // Tạo Interface
    {
        ShopCard_ViewModel DeleteCartCookieFunction(ShopCard_ViewModel card, int id); //Model lớn chứa Model nhỏ. Tạo Phương Thức     
    }
    public class DeleteCartCookieService : IDeleteCartCookieService // Thừa kế các thuộc tính từ Interface 
    {
        private readonly IConfigManagerService _configuration;
        public DeleteCartCookieService(IConfigManagerService configuration)
        {
            _configuration = configuration;
        }
        public ShopCard_ViewModel DeleteCartCookieFunction(ShopCard_ViewModel card, int id) // tạo riêng 1 service. (bỏ static r)
        {
            foreach (var item in card.product_card) // product_card là object chứa các sp trong giỏ hàng (card là obj lớn)
            {
                if (id == item.id) // lấy sp có id đó ra từ giỏ hàng (là giỏ product_card)
                {
                    card.product_card.Remove(item);
                    card.total_price = card.total_price - item.price;
                    break;
                }
            }
            return card;
        }
    }
}
