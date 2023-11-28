using Ecommerce_Wedsite.Models.Entities;

namespace Ecommerce_Wedsite.Models.ViewModel
{
    public class Sort_ViewModel 
    {
        public string proname { get; set; }
        public int category { get; set; }
        public int page { get; set; } 
        public int orderby { get; set; }
    }
}
//Lưu các biến page và orderby từ view
