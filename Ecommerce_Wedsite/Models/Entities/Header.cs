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
        public int HeaderType_Id { get; set; }
        public string HeaderAction { get; set; }
        public Bit HeaderCondition { get; set; }
        public int Header_HTML { get; set; }

        /*
         ? Có thể cần thêm public HeaderParnt_condition k: tùy
         ? Có cần thêm table Subheader riêng k: subheader, id parent, level, ...
         */
    }
    public enum Bit // false là 0, true là 1
    {
        False,
        True
    }
}
