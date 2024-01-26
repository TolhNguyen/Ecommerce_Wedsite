using Dapper.Contrib.Extensions;

namespace Ecommerce_Wedsite.Models.Entities
{
    [Table("ProductType")]
    public class ProductType
    {
        [Key]
        public int ProductType_Id { get; set; }
        public string ProductType_Name { get; set; }
        public int Header_Id { get; set; }
    }
}
