using Ecommerce_Wedsite.Models.Entities;

namespace Ecommerce_Wedsite.Models.ViewModel
{
    public class HomePage_ViewModel
    {
        public IEnumerable<HomePage> homepage_Vl1 { get; set; } // tao bien chua HeaderList lv 1 v 2. Gọi đúng biến IEnumerable(hay biến khác) với Service (nếu khai báo chung)
        public IEnumerable<HomePage> homepage_Vl2 { get; set; } // có thể gom nhiều model entities khác nhau chung, để cùng hiện cho 1 view
        public IEnumerable<HomePage> picture_Id { get; set; }
        public IEnumerable<Product> newproduct { get; set; }
        public IEnumerable<Product> promoproduct { get; set; }
        public IEnumerable<Users> users { get; set; }

    }
}
