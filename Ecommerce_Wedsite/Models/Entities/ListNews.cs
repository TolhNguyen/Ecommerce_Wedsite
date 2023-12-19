using Dapper.Contrib.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce_Wedsite.Models.Entities
{
    [Table("ListNews")]
    public class ListNews
    {
        [Key]
        public int ListNews_Id { get; set; }
        public int NewsCate_Id { get; set; }
        public int Picture_Id { get; set; }
        public string ListNews_Title { get; set; }
    }
}
