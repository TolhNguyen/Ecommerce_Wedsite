using Dapper.Contrib.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce_Wedsite.Models.Entities
{
    [Table("Users")]
    public class Users
    {
        [Key]
        public int User_Id { get; set; }
        public string User_Name { get; set; }
        public string User_Feedbacks { get; set; }
        public int Logo_Id { get; set; }
    }

}
