using Dapper.Contrib.Extensions;

namespace Ecommerce_Wedsite.Models.Entities
{
    [Table("ProductCheckout")]
    public class ProductCheckout // Array nhỏ nằm trong ShopCard object lớn
    {
        [Key]
        public int Checkout_Id { get; set; } // id thứ tự : 1 - 2
        public int ProductCheckout_Id { get; set; } // id sp: 5 - 5
        public int CustomerCheckout_Id { get; set; } // id người mua đơn hàng này : 1 - 1 => 
        public string ProductCheckout_Name { get; set; } // tên sp: Kilaz - Sen da
        public int ProductCheckout_Quantity { get; set; } // sl: 4 - 6
        public decimal ProductCheckout_Price { get; set; } // tiền sp đó: 1500 - 2000
        public string ProductCheckout_Img { get; set; } // link anh

    }
}
