using Ecommerce_Wedsite.Models.Entities;
using System.Security.Policy;

namespace Ecommerce_Wedsite.Models.ViewModel
{
    public class AdminStatistic_ViewModel
    {
        public IEnumerable<ProductCheckoutStatistic> productcheckoutstatistic { get; set; }
        public IEnumerable<ProductCountStatistic> productcountstatistic { get; set; }
        public IEnumerable<CustomerCountStatistic> customercountstatistic { get; set; }
        public IEnumerable<TurnoverStatistic> turnoverstatistic { get; set; }
        public IEnumerable<PromotionUsedStatistic> promousedstatistic { get; set; }
    }
    public class ProductCheckoutStatistic // Tạo một model riêng cho statistic từ query sql lên
    {
        public string ProductCheckout_Name { get; set; }
        public int TongSoLuong { get; set; }
    }

    public class ProductCountStatistic // Tạo một model riêng cho statistic từ query sql lên
    {
        public int TongSoLuongSP { get; set; }
    }

    public class CustomerCountStatistic // Tạo một model riêng cho statistic từ query sql lên
    {
        public int TongSoLuongCS { get; set; }
    }

    public class TurnoverStatistic // Tạo một model riêng cho statistic từ query sql lên
    {
        public int TongDoanhThu { get; set; }
    }
    public class PromotionUsedStatistic // Tạo một model riêng cho statistic từ query sql lên
    {
        public int TongPromoSuDung { get; set; }
    }
}
