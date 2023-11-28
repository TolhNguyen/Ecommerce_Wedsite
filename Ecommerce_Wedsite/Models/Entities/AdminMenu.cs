using Dapper.Contrib.Extensions;

namespace Ecommerce_Wedsite.Models.Entities
{
    // Model tương tác với SQL nhiều hơn
    [Table("AdminMenu")]
    public class AdminMenu
    {
        [Key]
        public int AdminMenu_Id { get; set; }
        public string AdminMenu_Title { get; set; }
        public Bit AdminMenu_Condition { get; set; }
        public int AdminMenu_ParentId { get; set; }
        public int AdminMenu_Level { get; set; }
        public string AdminMenu_Action { get; set; }
    }
    public enum Bit5 // false là 0, true là 1
    {
        False, //
        True
    }
}
