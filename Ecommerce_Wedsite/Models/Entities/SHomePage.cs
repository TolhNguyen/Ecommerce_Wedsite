using Dapper.Contrib.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce_Wedsite.Models.Entities
{
    [Table("SHomePage")]
    public class SHomePage
    {
        [Key]
        public int SHomePage_Id { get; set; }
        public string SHomePage_Title { get; set; }
        public string SHomePage_Content { get; set; }
        public string SHomePage_Icon { get; set; }
        public int SHomePage_Order { get; set; }
        public Bit7 SHomePage_Condition { get; set; }
        public int HomePage_Id { get; set; }
    }
    public enum Bit7 // false là 0, true là 1
    {
        False,
        True
    }
}
