using Ecommerce_Wedsite.Models.Entities;

namespace Ecommerce_Wedsite.Models.ViewModel
{
    //public class HomePageProductDisplay_ViewModel // Là model con cua model Header. Chi de thuc hien cac querry
    //{
    //    public IEnumerable<Product> productdisplay { get; set; }
    //    public int HomePage_Id { get; set; }

    //}
    public class HomePageProductDisplay_ViewModel // List lớn: chứa list nhỏ products và id HomePage
    {
        public List<ProducdDisplay> productdisplayGrp { get; set; } = new List<ProducdDisplay>();// new thành 1 list

	}
    public class ProducdDisplay
    {
        public IEnumerable<Product> productdisplay { get; set; } // List nhỏ product
        public int HomePage_Id { get; set; } // và Id Home Page
    }

    
}
