using Ecommerce_Wedsite.Models.Entities;

namespace Ecommerce_Wedsite.Models.ViewModel
{
    public class ProductCheckout_ViewModel
    {
        public IEnumerable<ProductCheckoutStatistic> productcheckoutstatistic { get; set; }

    }
    public class ProductCheckoutStatistic // Tạo một model riêng cho statistic từ query sql lên
    {
        public string ProductCheckout_Name { get; set; }
        public int TongSoLuong { get; set; }
    }
}
