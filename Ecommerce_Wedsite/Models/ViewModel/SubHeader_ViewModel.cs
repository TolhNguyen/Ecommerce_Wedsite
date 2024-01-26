using Ecommerce_Wedsite.Models.Entities;

namespace Ecommerce_Wedsite.Models.ViewModel
{
    public class SubHeader_ViewModel 
    {
        public IEnumerable<ProductType> subheaderproducttype { get; set; }
        public IEnumerable<Product> subheaderproduct { get; set; }
        public IEnumerable<NewsCategory> subheadernewscate { get; set; }
    }
}
