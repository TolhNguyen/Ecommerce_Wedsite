using Dapper.Contrib.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce_Wedsite.Models.Entities
{
    [Table("City")]
    public class City
    {
        [Key]
        public int City_Id { get; set; }
        public string City_Name { get; set; }
        public int City_ZipCode {  get; set; }
    }
}
