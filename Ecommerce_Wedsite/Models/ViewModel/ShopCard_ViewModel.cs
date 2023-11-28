namespace Ecommerce_Wedsite.Models.ViewModel
{
    public class ShopCard_ViewModel
    {
        public List<Producd_ShopCard> product_card { get; set; } = new List<Producd_ShopCard>(); // thành 1 list
        public decimal total_price { get; set; }
        public string promo_name { get; set; }

    }
    public class Producd_ShopCard // Array nhỏ nằm trong ShopCard object lớn
    {
        public int id { get; set; }
        public string name { get; set; }
        public int qty { get; set; }
        public decimal price { get; set; }
    }
}
// k cần vào Allayout. Dây là model để lưu, đọc dữ liệu từ ajax gửi về cho controller
