using Dapper.Contrib.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce_Wedsite.Models.Entities
{
    [Table("Picture")]
    public class Picture
    {
        [Key]
        public int Picture_Id { get; set; }
        public string Picture_FileName { get; set; }
        public string Picture_Desciption { get; set; }
        public int PictureGrp_Id { get; set; }
    }
    
}
