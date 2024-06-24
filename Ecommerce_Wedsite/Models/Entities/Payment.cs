using Dapper.Contrib.Extensions;

namespace Ecommerce_Wedsite.Models.Entities
{
    [Table("Payment")]
    public class Payment
    {
        [Key]
        public int Payment_Id { get; set; }
        public string Payment_Name { get; set; }
        public string Payment_Img { get; set; }
        public string Payment_Color { get; set; }
        public string Payment_Color2 { get; set; }
        public Bit9 Paymnet_Condition { get; set; }
    }
    public enum Bit9 // false là 0, true là 1
    {
        False,
        True
    }
}
