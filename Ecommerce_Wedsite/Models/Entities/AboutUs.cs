using Dapper.Contrib.Extensions;

namespace Ecommerce_Wedsite.Models.Entities
{
    [Table("AboutUs")]
    public class AboutUs
    {
        [Key]
        public int AboutUs_Id { get; set; }
        public string AboutUs_Title { get; set; }
        public string AboutUs_SubTitle { get; set; }
        public int AboutUs_HTML { get; set; }
        public string AboutUs_Content { get; set; }
        public int AboutUs_ParentId { get; set; }
        public int AboutUs_Level { get; set; }
        public int AboutUs_Num { get; set; }
        public string AboutUs_Img { get; set; }
    }
}
