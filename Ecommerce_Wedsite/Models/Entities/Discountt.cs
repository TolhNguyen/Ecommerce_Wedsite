using Dapper.Contrib.Extensions;

namespace Ecommerce_Wedsite.Models.Entities
{
    [Table("Discountt")]
    public class Discountt
    {
        [Key]
        public int Discount_Id { get; set; }
        public decimal Discount_Rate { get; set; }
        public int Discount_No { get; set; }
        public int Discount_RanId { get; set; }
        public string Discount_Name { get; set; }
        public Guid Discount_NewId { get; set; } // này là random ID chứ k phải là id của tin tức news
        public string Discount_Code { get; set; }
    }
}
