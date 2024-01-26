using Dapper.Contrib.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce_Wedsite.Models.Entities
{
    [Table("NewsCategory")]
    public class NewsCategory
    {
        [Key]
        public int NewsCate_Id { get; set; }
        public string NewsCate_Name { get; set; }
        public Bit6 NewsCate_Condition { get; set; }
        public int NewsCate_HTML { get; set; }
        public int Header_Id { get; set; }
    }
    public enum Bit6 // false là 0, true là 1
    {
        False,
        True
    }
}
