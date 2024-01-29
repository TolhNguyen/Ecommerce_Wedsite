using Dapper.Contrib.Extensions;

namespace Ecommerce_Wedsite.Models.Entities
{
    [Table("Product")]
    public class Product
    {
        [Key]
        public int Product_Id { get; set; }
        public string Product_Name { get; set; }
        public int ProductType_Id { get; set; }
        public int Picture_Id { get; set; }
        public int Logo_Id { get; set; }
        public int Product_Price { get; set; }
        public int Product_PromoPrice { get; set; }
        public string Product_ShortDescription { get; set; }
        public string Product_Manufacturers { get; set; }
        public Bit4 Product_Condition { get; set; }
        public string Product_Info { get; set; }
        public string Product_Size { get; set; }
        public string Product_Href { get; set; }
        public string Product_Info2 { get; set; }
        public int Product_Quantity { get; set; }

    }

    // 0 = False, 1 = True, bit sẽ đổi 1 và 0 thành Unv và av tùy theo ta quy định
    public enum Bit4
    {
        Unavailable,
        Available
    }
}
// Bit giống giới tính trong New web site 2