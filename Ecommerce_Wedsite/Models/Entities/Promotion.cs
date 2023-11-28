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
    }
    
}
