using Dapper.Contrib.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce_Wedsite.Models.Entities
{
    [Table("Logo")]
    public class Logo
    {
        [Key]
        public int Logo_Id { get; set; }
        public string Logo_FileName { get; set; }
        public string Logo_Description { get; set; }
    }

}
