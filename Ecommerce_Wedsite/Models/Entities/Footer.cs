using Dapper.Contrib.Extensions;

namespace Ecommerce_Wedsite.Models.Entities
{
    [Table("Footer")]
    public class Footer
    {
        [Key]
        public int Footer_Id { get; set; }
        public string Footer_Title { get; set; }
        public int FooterParent_Id { get; set; }
        public int FooterLevels { get; set; }
        public int FooterType_Id { get; set; }
        public Bit2 FooterCondition { get; set; }
    }
    public enum Bit2
    {
        True,
        Flase
    }
}
