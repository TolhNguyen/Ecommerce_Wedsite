using Ecommerce_Wedsite.Models.Entities;

namespace Ecommerce_Wedsite.Models.ViewModel
{
    public class AllLayout //Đây là 1 model xài chung nhieu models khac de tương tác với nhieu View 1 luc
    {
        //Model dùng chung. Để load trước các view cần thiết. Ajax để load các chi tiết sau.

        //public HeaderViewModel header { get; set; } = new HeaderViewModel(); // tạo một biến tượng trưng để liên kết model vào. dùng model nào gọi biến đó ra.
        //public FooterViewModel footer { get; set; } = new FooterViewModel(); 
        // tạo một biến tượng trưng để liên kết model vào. dùng model nào gọi biến đó ra.

        public IEnumerable<Header> header { get; set; } = new List<Header>(); // Phải tạo biến là 1 list chứa dữ liệu của Header (IEnumerable). Gọi từ Entities lên đề sủ dụng
        public IEnumerable<Footer> footer { get; set; } = new List<Footer>();
        public IEnumerable<HomePage> HomePage { get; set; } = new List<HomePage>();
        public IEnumerable<Picture> Picture { get; set; } = new List<Picture>();
        public IEnumerable<Product> Products { get; set; } = new List<Product>(); // Lấy model Entities Products lên
        public IEnumerable<NewWebAppViewModel> newWebAppViewModels { get; set; } = new List<NewWebAppViewModel>(); // mang newwebappmodel vào xài chung 
        public IEnumerable<Users> users { get; set; } = new List<Users>();
        public IEnumerable<Logo> logo { get; set; } = new List<Logo>();
        public IEnumerable<ProductType> producttype { get; set; } = new List<ProductType>();
        public IEnumerable<Admin> admin { get; set; } = new List<Admin>();
        public IEnumerable<Order> order { get; set; } = new List<Order>();
        public IEnumerable<Promotion> promo { get; set; } = new List<Promotion>();
        public IEnumerable<CustomerCheckout> customercheckout { get; set; } = new List<CustomerCheckout>();
        public IEnumerable<Discountt> discountt { get; set; } = new List<Discountt>();


        /*
            -   Tạo từng ViewModel từ Entities (có thể tạo nhiều entities trong 1 viewmodel để service gọi ra nhiều biến result giá trị để sử dụng đc
            -   truyền ViewModels vào Alllayout bằng 1 biến mới (gọi trong alllayout dùng chung):
            -   
         */
        public Header_ViewModel header_ViewModels { get; set; } // tạo model con riêng chỉ để thực hiện function trên View (từ model header). Chỉ lấy 1 dữ liệu (là 1 RMObject) xài mỗi lần
        public Footer_ViewModel footer_ViewModels { get; set; }
        public HeaderAndFooter_ViewModel headerandfooter_ViewModels { get; set; } // Gom model cho header và footer chung
        public HomePage_ViewModel homepage_ViewModels { get; set; }
        public Picture_ViewModel picture_ViewModels { get; set; }
        public Logo_ViewModel logo_ViewModels { get; set; }
        public Product_ViewModel product_ViewModels { get; set; }
        public OrderByProduct_ViewModel orderbyproduct_ViewModels { get; set; }
        public Sort_ViewModel sort_ViewModels { get; set; } = new Sort_ViewModel(); //không đc để rỗng phải "= new ...". Do Sort_ViewModel k có dữ liệu (trong database) sẵn để chứa dữ liệu đc lưu vào.
        public ProductType_ViewModel producttype_ViewModels { get; set; }
        public SubProduct_ViewModel subproduct_ViewModels { get; set;}
        public Checkout_ViewModel checkout_ViewModels { get; set; }
        public Admin_ViewModel admin_ViewModels { get; set; }
        public Order_ViewModel order_ViewModels { get; set; }
        public Promo_ViewModel promo_ViewModels { get; set; }
        public CustomerCheckout_ViewModel customercheckout_ViewModels { get; set; }
        public Discountt_ViewModel discountt_ViewModels { get; set; }
    }
}
