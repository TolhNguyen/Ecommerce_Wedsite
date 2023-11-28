using Dapper.Contrib.Extensions;

namespace Ecommerce_Wedsite.Models.Entities
{
    // Model tương tác với SQL nhiều hơn
    [Table("Header")]
    public class Header
    {
        [Key]
        public int Header_Id { get; set; }
        public string Header_Title { get; set; }
        public int HeaderParent_Id { get; set; }
        public int HeaderLevels { get; set; }
        public int HeaderType_Id { get; set; }
        public string HeaderAction { get; set; }
        public Bit HeaderCondition { get; set; }
    }
    public enum Bit // false là 0, true là 1
    {
        False,
        True
    }
}
