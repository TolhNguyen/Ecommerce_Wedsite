using Ecommerce_Wedsite.Models.Entities;

namespace Ecommerce_Wedsite.Models.ViewModel
{
    public class Product_ViewModel 
    {
        public IEnumerable<Product> product { get; set; } 
        public int producttypeid { get; set; } 
    }
}
