using Dapper.Contrib.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce_Wedsite.Models.Entities
{
    [Table("News")]
    public class News
    {
        [Key]
        public int News_Id { get; set; }
        public int ListNews_Id { get; set; }
        public int Picture_Id { get; set; }
        public string News_Title { get; set; }
        public string News_SubTitle { get; set; }
        public string Author { get; set; }
        public string News_Content { get; set; }
        public DateTime News_DatePost {  get; set; }
    }
}
