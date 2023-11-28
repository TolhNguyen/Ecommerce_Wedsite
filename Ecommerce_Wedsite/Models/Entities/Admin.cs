using Dapper.Contrib.Extensions;

namespace Ecommerce_Wedsite.Models.Entities
{
    [Table("Admin")]
    public class Admin
    {
        [Key]
        public int Admin_Id { get; set; }
        public string Admin_Username { get; set; }
        public string Admin_Password { get; set; }
    }

}
