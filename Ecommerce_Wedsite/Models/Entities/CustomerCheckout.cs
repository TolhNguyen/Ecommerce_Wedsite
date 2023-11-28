﻿using Dapper.Contrib.Extensions;

namespace Ecommerce_Wedsite.Models.Entities
{
    [Table("CustomerCheckout")]
    public class CustomerCheckout // k cần bỏ vô Allayout View Model (khi nào cần hiện dữ liệu mới bỏ vô đó)
    {
        [Key]
        public int CustomerCheckout_Id { get; set; }
        public string CustomerCheckout_FirstName { get; set; }
        public string CustomerCheckout_LastName { get; set; }
        public int CustomerCheckout_Phone { get; set; }
        public string CustomerCheckout_Email { get; set; }
        public string CustomerCheckout_Address { get; set; }
        public int CustomerCheckout_PostCode { get; set; }
        public int CustomerCheckout_Zip { get; set; }
        public string CustomerCheckout_Mess { get; set; }
        public int CustomerCheckout_TotalPrice { get; set; }
        public int Promotion_Id { get; set; }
    }
    //public enum Promo
    //{
    //    None,
    //    Special, // id: 1
    //    VIP // id: 2
    //}
}
