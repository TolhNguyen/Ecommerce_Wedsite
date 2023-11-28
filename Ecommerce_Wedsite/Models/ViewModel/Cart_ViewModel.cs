using Ecommerce_Wedsite.Models.Entities;

namespace Ecommerce_Wedsite.Models.ViewModel
{
    public class Cart_ViewModel
    {
        public int Item_Id { get; set; }
        public string Item_Name  { get; set; }
        public string Item_FileName { get; set; }
        public int Item_Price { get; set; }
    }
}
