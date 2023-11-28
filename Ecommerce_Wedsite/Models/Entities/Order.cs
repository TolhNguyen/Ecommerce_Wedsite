using Dapper.Contrib.Extensions;

namespace Ecommerce_Wedsite.Models.Entities
{
    [Table("Order")]
    public class Order
    {
        [Key]
        public int Order_Id { get; set; }
        public string Order_Itemname { get; set; }
        public string Order_Quantity { get; set; }
        public string Order_Price { get; set; }

        public int Checkout_Id { get; set; }
    }
}
