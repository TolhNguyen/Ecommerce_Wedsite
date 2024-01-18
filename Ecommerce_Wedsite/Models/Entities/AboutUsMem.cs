using Dapper.Contrib.Extensions;

namespace Ecommerce_Wedsite.Models.Entities
{
    [Table("AboutUsMem")]
    public class AboutUsMem
    {
        [Key]
        public int AboutUsMem_Id { get; set; }
        public string AboutUsMem_Name { get; set; }
        public string AboutUsMem_Position { get; set; }
        public string AboutUsMem_Des { get; set; }
        public string AboutUsMem_Contact { get; set; }
        public int AboutUsMem_HTML { get; set; }
        public int AboutUsMem_PictureId { get; set; }
        public int AboutUsMem_Level { get; set; }
    }
}
