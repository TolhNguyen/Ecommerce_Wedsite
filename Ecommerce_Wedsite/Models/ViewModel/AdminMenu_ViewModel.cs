using Ecommerce_Wedsite.Models.Entities;

namespace Ecommerce_Wedsite.Models.ViewModel
{
    public class AdminMenu_ViewModel // Là model con cua model Header. Chi de thuc hien cac querry
    {
        //Cả hai đều có hết thuộc tính từ Header nhưng chỉ phân biệt khác nhau ở level (cụ thể là sự khác nhau ở câu query trong Header Service. Để lấy các Title khác nhau)
        public IEnumerable<AdminMenu> adminmenu_LV1 { get; set; } // tao bien chua HeaderList lv 1 v 2. Gọi đúng biến IEnumerable(hay biến khác) với Service (nếu khai báo chung)
        public IEnumerable<AdminMenu> adminmenu_LV2 { get; set; }
        public IEnumerable<AdminMenu> adminmenu { get; set; }
    }
}
