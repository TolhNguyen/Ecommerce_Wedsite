using Ecommerce_Wedsite.Models.Entities;

namespace Ecommerce_Wedsite.Models.ViewModel
{
    public class Checkout_ViewModel
    {
        public List<ProductCheckout> product_checkout { get; set; } = new List<ProductCheckout>();
        public List<CustomerCheckout> customer_checkout { get; set; } = new List<CustomerCheckout>();

    }
    
    //public class Customer_Checkout // Array nhỏ nằm trong ShopCard object lớn
    //{
    //    public int CustomerCheckout_Id { get; set; } // 1
    //    public string CustomerCheckout_FirstName { get; set; } 
    //    public string CustomerCheckout_LastName { get; set; } 
    //    public int CustomerCheckout_Phone { get; set; } 
    //    public string CustomerCheckout_Email { get; set; } 
    //    public string CustomerCheckout_Address { get; set; } 
    //    public int CustomerCheckout_PostCode { get; set; }
    //    public int CustomerCheckout_Zip { get; set; }
    //    public int CustomerCheckout_TotalPrice { get; set; } // 3500
    //}
}
/*
 Cần làm típ: tạo ajax bên view, và 2 services để add vào db
            tạo thêm 2 table trong sql để lưu dữ liệu vào.
 */
