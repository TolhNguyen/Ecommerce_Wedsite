using Dapper.Contrib.Extensions;

namespace Ecommerce_Wedsite.Models.Entities
{
    [Table("HomePage")]
    public class HomePage
    {
        [Key]
        public int HomePage_Id { get; set; }
        public string HomePage_Title { get; set; }
        public Bit3 HomePage_Condition { get; set; }
        public int HomePageParent_Id { get; set; }
        public int HomePage_Levels { get; set; }
        public int HomePage_Type { get; set; }
        public int Logo_Id { get; set; }
        public int PictureGrp_Id { get; set; }
        public int HTML_Id { get; set; }
        public int Video_Id { get; set; }
        public string HomePage_Img { get; set; }
        public string HomePage_Summary { get; set; }
        public int HomePage_Order {  get; set; }
        public string HomePage_Video {  get; set; }
    }
    public enum Bit3
    {
        False, // 0
        True // 1
    }
}
