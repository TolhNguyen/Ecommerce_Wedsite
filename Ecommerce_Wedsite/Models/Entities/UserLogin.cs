using Dapper.Contrib.Extensions;

namespace Ecommerce_Wedsite.Models.Entities
{
    [Table("UserLogin")]
    public class UserLogin
    {
        [Key]
        public int UserLogin_Id { get; set; }
        public string UserLogin_Name { get; set; }
        public string UserLogin_Password { get; set; }
    }

}
