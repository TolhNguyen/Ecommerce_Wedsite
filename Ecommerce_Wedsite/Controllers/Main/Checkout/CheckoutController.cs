using Ecommerce_Wedsite.Models;
using Ecommerce_Wedsite.Models.Entities;
using Ecommerce_Wedsite.Models.Helpers.Response;
using Ecommerce_Wedsite.Models.ViewModel;
using Ecommerce_Wedsite.Service.WebApp;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text.Json;

namespace Ecommerce_Wedsite.Controllers.Main
{
    public class CheckoutController : Controller
    {
        private readonly ILogger<CheckoutController> _logger;
        private readonly IPictureService _pictureService;
        private readonly IHeaderAndFooterService _headerandfooterService;
        private readonly ICustomerCheckoutService _customercheckoutService;
        private readonly IProductCheckoutService _productcheckoutService;
        private readonly IPromotionService _promotionService;
        private readonly IDiscounttService _discounttService;
        private readonly IDecreasePromoQuantityService _decreasepromoquantityService;
        private readonly ICityService _cityService;
        private readonly ISubHeaderService _subheaderService;
        private readonly ICheckingQuantityService _checkingquantityService;

        public CheckoutController(ILogger<CheckoutController> logger, IHeaderAndFooterService headerandfooterService, IPictureService pictureService, ICustomerCheckoutService customercheckoutService, IProductCheckoutService productcheckoutService, IPromotionService promotionService, IDiscounttService discounttService, IDecreasePromoQuantityService decreasepromoquantityService, ICityService cityService, ISubHeaderService subheaderService, ICheckingQuantityService checkingquantityService)
        {
            _logger = logger;
            _headerandfooterService = headerandfooterService;
            _pictureService = pictureService;
            _customercheckoutService = customercheckoutService;
            _productcheckoutService = productcheckoutService;
            _promotionService = promotionService;
            _discounttService = discounttService;
            _decreasepromoquantityService = decreasepromoquantityService;
            _cityService = cityService;
            _subheaderService = subheaderService;
            _checkingquantityService = checkingquantityService;
        }

        [Route("~/checkout")]
        public async Task<IActionResult> Checkout() // chạy ra trang checkout trước function phải là 1 controller khác
        {
            var All = new AllLayout();

            var headerandfooter_ViewModels = await _headerandfooterService.HeaderAndFooter_ServiceTest();
            var promotion_ViewModels = await _promotionService.Service_Test();
            var picture_ViewModels = await _pictureService.Service_Test();
            var discountt_ViewModels = await _discounttService.PopupDiscount();
            var city_ViewModels = await _cityService.Service_Test();
            var subheader_ViewModels = await _subheaderService.SubHeader();

            All.headerandfooter_ViewModels = headerandfooter_ViewModels.Data;
            All.picture_ViewModels = picture_ViewModels.Data;
            All.promo_ViewModels = promotion_ViewModels.Data;
            All.discountt_ViewModels = discountt_ViewModels.Data;
            All.city_ViewModels = city_ViewModels.Data;
            All.subheader_ViewModels = subheader_ViewModels.Data;

            return View("Checkout", All);
        }


        [Route("~/checkoutfunction")]
        public async Task<IActionResult> CheckoutFunction(CustomerCheckout customercheckout) // l
        {
            await _customercheckoutService.CustomerCheckoutFunction(customercheckout); // xong
            int cscheckoutid = customercheckout.CustomerCheckout_Id; // lấy cus id ra so sánh
            int promoid = customercheckout.Promotion_Id; // lấy id để - số lượng
            var cookieCard = HttpContext.Request.Cookies["cart"]; //lấy giỏ hàng từ cookie
            var card = new ShopCard_ViewModel();
            if (cookieCard != null) // là đã có cookie rồi
            {
                card = JsonSerializer.Deserialize<ShopCard_ViewModel>(cookieCard);
                card.total_price = customercheckout.CustomerCheckout_TotalPrice; // đổi lại total trong cart
                await _productcheckoutService.ProductCheckoutFunction(card, cscheckoutid); // kiểm tra giỏ hàng có được lưu chưa
                await _decreasepromoquantityService.DecreasePromoQuantity(promoid);
                string productvalue = JsonSerializer.Serialize(card); // add lại card đã thay đổi
                CookieOptions option = new CookieOptions();
                option.Secure = true;
                option.HttpOnly = false; // này là khóa cookie thành private k cho xài
                option.SameSite = SameSiteMode.None;
                option.Path = "/";
                option.Expires = DateTime.Now.AddDays(1);
                HttpContext.Response.Cookies.Append("cart", productvalue, option);
                return RedirectToAction("CheckoutFinal"); // chuyển sang controller khác
            }
            else return Json(false);
        }

        [Route("~/checkoutfinal")]
        public async Task<IActionResult> CheckoutFinal() 
        {
            var All = new AllLayout();

            var headerandfooter_ViewModels = await _headerandfooterService.HeaderAndFooter_ServiceTest();
            var picture_ViewModels = await _pictureService.Service_Test();
            var discountt_ViewModels = await _discounttService.PopupDiscount();
            var subheader_ViewModels = await _subheaderService.SubHeader();

            All.discountt_ViewModels = discountt_ViewModels.Data;
            All.headerandfooter_ViewModels = headerandfooter_ViewModels.Data;
            All.picture_ViewModels = picture_ViewModels.Data;
            All.subheader_ViewModels = subheader_ViewModels.Data;

            var cookieCard = HttpContext.Request.Cookies["cart"];
            if (cookieCard != null) // set time cookie cho giỏ hàng mất luôn
            {
                CookieOptions option = new CookieOptions();
                option.Secure = true;
                option.HttpOnly = false;
                option.SameSite = SameSiteMode.None;
                option.Path = "/";
                option.Expires = DateTime.Now.AddMinutes(1); // set cookie cart sau 1p là mất
                HttpContext.Response.Cookies.Append("cart", cookieCard, option); // lưu những thay đổi vào cart cookie
                
                // k cần return chỉ cần nếu chạy if thì set time out mất cookie.
            }

            return View("CheckoutFinal", All); // hiện page kết quả và nội dung các dữ liệu checkout và order
        }

        //Chức năng check sl sp
        public async Task<IActionResult> CheckingCheckout(string cartcookie) // dữ liệu từ ajax. Lỗi trả về trang checkout
        {
            var cartcookieobj = new ShopCard_ViewModel();
			cartcookieobj = JsonSerializer.Deserialize<ShopCard_ViewModel>(cartcookie); // 2 biến này giống cookiecard và card
            if (cartcookieobj != null)
            {
				var result = await _checkingquantityService.CheckingQuantityFunction(cartcookieobj);
                if(result.success == true)
                {
					return Json(true);
				}
                else return Json(result.message); // Phải trả về kiểu như này mới được

			}
			return Json(false); // hiện page kết quả và nội dung các dữ liệu checkout và order
        }
    }
}
/*
Hướng đi: 
    Get từng value input vào ajax -> ajax gửi data value qua controller 
    -> Tạo trước 1 object lớn chứa 2 object nhỏ (model object chi tiết giỏ hàng - chứa data hóa đơn và model object người mua- chứa data người mua và id hóa đơn)
    -> Tạo 2 services (1 hóa đon, 1 người mua) add cùng lúc 2 loại objects đó vào db (có table sql sẵn) trong 1 controller
    -> return về ajax để chuyển trang. (chức năng sẽ là ở...)

Đường đi:
    - Tạo fnc ajax bên view Checkout nhận value của CustomerCheckout và ProductCheckout
    - Chuyển sang controller nhận
    - Chuyển sang service thực hiện insert vào db
    - Controller trả về Json (k view)
    - ajax nhận lại kết quả và trả view tiếp theo nếu thành công(true) view tiếp theo, k thì view cũ
    ->(xài ajax của ...)

    - CustomerCheckoutFunction insert đc data rồi (thiếu CustomerCheckout_Message vô, sửa sql nữa)
    - Chưa có ajax nhận json trả về (đang trả về 1 trang có là json(true)) - k cần
    
    => xong r

Tương lai 1:
    - tối ưu CustomerCheckout_Address(thành ô chọn dữ liệu)
    - Tạo CustomerCheckout_Mess (lưu vào db nữa)
    - Làm lại trang admin cho checkout tables
    - Tạo chức năng nạp promotion vào checkout (đang làm ...)
    - Tạo chức năng tìm kiếm sản phẩm theo tên trang web
    - Tạo các trang chính còn lại
    -
*/
