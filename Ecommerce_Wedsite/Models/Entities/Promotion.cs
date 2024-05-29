using Dapper.Contrib.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce_Wedsite.Models.Entities
{
    [Table("Promotion")]
    public class Promotion
    {
        [Key]
        public int Promotion_Id { get; set; }
        public string Promotion_Name { get; set; }
        public int Promotion_Quantity { get; set; }
        public decimal Promotion_Percent { get; set; }
        public Bit8 Promotion_Condition { get; set; }
    }
    public enum Bit8 // false là 0, true là 1
    {
        False,
        True
    }

}
