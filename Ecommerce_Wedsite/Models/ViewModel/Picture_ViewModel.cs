using Ecommerce_Wedsite.Models.Entities;

namespace Ecommerce_Wedsite.Models.ViewModel
{
    public class Picture_ViewModel // Là model con cua model Header. Chi de thuc hien cac querry
    {
        public IEnumerable<Picture> picture { get; set; } // Lấy hết thuộc tính của Piture.
                                                          // Gọi 1 biến đơn giản vì query k yêu cầu phức tạp (k có phân biệt, nhiều mục cụ thể)

        // Piture_Lv1 2 để làm ảnh minh họa riêng cho các levels:
        public IEnumerable<Picture> subpicture { get; set; }


    }
}
