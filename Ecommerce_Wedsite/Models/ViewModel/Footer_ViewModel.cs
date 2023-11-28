using Microsoft.AspNetCore.Mvc;
using Ecommerce_Wedsite.Models.Entities;


namespace Ecommerce_Wedsite.Models.ViewModel
{
    public class Footer_ViewModel 
    {
        public IEnumerable<Footer> footer_Vl1 { get; set; } // tao bien chua HeaderList lv 1 v 2. Gọi đúng biến IEnumerable(hay biến khác) với Service (nếu khai báo chung)
        public IEnumerable<Footer> footer_Vl2 { get; set; }
        public IEnumerable<Footer> footer { get; set; }
    }
}
