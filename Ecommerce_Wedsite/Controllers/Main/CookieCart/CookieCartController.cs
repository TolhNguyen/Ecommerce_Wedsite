using Ecommerce_Wedsite.Controllers.Main;
using Ecommerce_Wedsite.Models;
using Ecommerce_Wedsite.Models.Entities;
using Ecommerce_Wedsite.Models.Helpers.Response;
using Ecommerce_Wedsite.Models.ViewModel;
using Ecommerce_Wedsite.Service.WebApp;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text.Json;

namespace Ecommerce_Wedsite.Controllers
{
    public class CookieCartController : Controller
    {
        private readonly ILogger<CookieCartController> _logger;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IHeaderAndFooterService _headerandfooterService;
        private readonly IProductCookieService _productcookieService;
        private readonly IAddCartCookieService _addcartcookieService;
        private readonly ICreateCartCookieService _createcartcookieService;
        private readonly IDeleteCartCookieService _deletecartcookieService;
        private readonly IChangeProductCartQuantityService _changeproductcartquantityService;

        public CookieCartController(ILogger<CookieCartController> logger, INewWebAppService newWebAppService, IHeaderAndFooterService headerandfooterService, IProductCookieService productcookieService, IAddCartCookieService addcartcookieService, ICreateCartCookieService createcartcookieService, IDeleteCartCookieService deletecartcookieService, IChangeProductCartQuantityService changeproductcartquantityService)
        {
            _logger = logger;
            //_newWebAppService = newWebAppService; // Tạo 1 tham chiếu 
            _headerandfooterService = headerandfooterService;
            _productcookieService = productcookieService;
            _addcartcookieService = addcartcookieService;
            _createcartcookieService = createcartcookieService;
            _deletecartcookieService = deletecartcookieService;
            _changeproductcartquantityService = changeproductcartquantityService;
        }


        public async Task<IActionResult> CreateCookie(int id, int qty) // truyền biến id (id sp), qty (số lượng sp)
        {
            //  lay thong san pham tu id
            //var All = new AllLayout();
            var product_ViewModels = (await _productcookieService.Service_Test(id)).Data.product.FirstOrDefault(); // lấy sp có id từ db bằng service. Service này chạy trước
            if (product_ViewModels == null) // nếu trong db k có product trùng với biến id nào
            {
                return Json(false);
            }

            var cookieCard = HttpContext.Request.Cookies["cart"]; // lấy value từ cart cookie

            // gắn biến card thừa kế model ShopCard nên sẽ có 2 thuộc tính: totalprice sp, và 1 object là cái giỏ hàng chứa thông tin từng sp (từng obj item) có trong đó.
            var card = new ShopCard_ViewModel();
            if (cookieCard != null) // là đã có cookie rồi
            {
                card = JsonSerializer.Deserialize<ShopCard_ViewModel>(cookieCard); // lấy cookie value ra và convert thành object -> luu vao cart
                await _createcartcookieService.CreateCookieFunction(card, product_ViewModels, qty); // function thêm sp vào và update lại card (card có sẵn value rồi). Sai tên function k phải là create mà là ... 
                string productvalue = JsonSerializer.Serialize(card); // đổi từ jsonobj sang string là productvalue để lưu vào cookie
                CookieOptions option = new CookieOptions();
                option.Secure = true; // secure là trữ value vào cookie lại
                option.HttpOnly = false; // httponly này là khóa cookie thành private k cho hiện, xài
                option.SameSite = SameSiteMode.None;
                option.Path = "/";
                option.Expires = DateTime.Now.AddDays(1);
                HttpContext.Response.Cookies.Append("cart", productvalue, option); // lưu những update vào cart
            }
            
            else // k có cookie sẵn thì tạo và chèn dữ liệu mới vô
            {
                card = await _addcartcookieService.them_san_pham_vao_goi_hang(card, product_ViewModels, qty); // k có cookie value nên tạo value luôn
                string productvalue = JsonSerializer.Serialize(card); // đổi từ jsonobj sang string là productvalue. Và chèn card là value mới của cookie vô
                CookieOptions option = new CookieOptions();
                option.Secure = true;
                option.HttpOnly = false; // này là khóa cookie thành private k cho xài
                option.SameSite = SameSiteMode.None;
                option.Path = "/";
                option.Expires = DateTime.Now.AddDays(1);
                HttpContext.Response.Cookies.Append("cart", productvalue, option); // lưu những update đó vào cart
            }
            return Json(true);
        }
        // kiem tra xem da co gio hang hay chua , Lay cookie
        // .1 neu chua co. khong xu li gi ca
        // .2 neu da co. Xu li chuyen du lieu cookie thanh obj

        //var cookieCard = HttpContext.Request.Cookies["cart"]; // lấy value từ cart cookie
        //var card = new ShopCard_ViewModel(); // tạo biến card là biến lưu giá trị của đơn hàng cart ,sẽ thừa kế những thuộc tính của product và thêm qty, total price
        //if (cookieCard != null)
        //{ // nếu có value trong cookie thì tạo  
        //    card = JsonSerializer.Deserialize<ShopCard_ViewModel>(cookieCard); // lấy value đó và convert thành object
        //    if (!card.product_card.Any()) // Kiem tra giỏ có đang trống hay k. Lấy thông tin product lên ktra. Nếu trống:
        //        // tinh toan va them san pham mới
        //        card = _addcartcookieService.them_san_pham_vao_goi_hang(card, product_ViewModels, qty); // gắn biến bằng service thêm sp mới vào. (Mới thêm addcartservice)
        //    else
        //    {
        //        var check = new Producd_ShopCard(); // tạo biến check (ktra xem sp mới khi add vào có id của sp cũ hay chưa)
        //        foreach (var item in card.product_card) // foreach từng sp trong giỏ hàng 
        //        {
        //            if (item.id == product_ViewModels.Product_Id) // ktra có id giống nhau k
        //            {
        //                check = item; // nếu có, gắn biến check là item (có id giống) đó
        //                break;
        //            }
        //        }
        //        if (check.name == null) // gio hang khong co item giong voi san pham them vao. Biến check k có id giống nhau
        //            card = _addcartcookieService.them_san_pham_vao_goi_hang(card, product_ViewModels, qty); // thì thêm mới sp vào giỏ hàng
        //        else // gio hang co item giong san them vao 
        //        {
        //            foreach (var item in card.product_card) // foreach từng sp trong giỏ hàng 
        //            {
        //                if (item.id == product_ViewModels.Product_Id) // nếu có id cũ
        //                {
        //                    item.qty = item.qty + qty; // thì + dồn số lượng mới vào sl cũ 
        //                    item.price = item.price + product_ViewModels.Product_Price * qty; // + tiền dồn 
        //                    card.total_price = card.total_price + item.price; // + dồn tiền total
        //                    break;
        //                }
        //            }
        //        }
        //    }
        //    // update cookie gio hang 
        //    string productvalue = JsonSerializer.Serialize(card); // đổi từ jsonobj sang string là productvalue
        //    CookieOptions option = new CookieOptions();
        //    option.Secure = true;
        //    option.HttpOnly = true;
        //    option.SameSite = SameSiteMode.None;
        //    option.Path = "/";
        //    option.Expires = DateTime.Now.AddDays(1);
        //    HttpContext.Response.Cookies.Append("cart", productvalue, option); // lưu những update vào cart
        //}
        //else
        //{
        //    card = _addcartcookieService.them_san_pham_vao_goi_hang(card, product_ViewModels, qty);
        //    string productvalue = JsonSerializer.Serialize(card); // đổi từ jsonobj sang string là productvalue
        //    CookieOptions option = new CookieOptions();
        //    option.Secure = true;
        //    option.HttpOnly = true;
        //    option.SameSite = SameSiteMode.None;
        //    option.Path = "/";
        //    option.Expires = DateTime.Now.AddDays(1);
        //    HttpContext.Response.Cookies.Append("cart", productvalue, option); // lưu những update đó vào cart
        //}
        // return thong bao:
        //return Json(true);

        //public static ShopCard_ViewModel them_san_pham_vao_goi_hang(ShopCard_ViewModel card, Product product_ViewModels, int qty) // tạo riêng 1 service 
        //{

        //    //var product_item = new Producd_ShopCard();

        //    //product_item.id = product_ViewModels.Product_Id;
        //    //product_item.name = product_ViewModels.Product_Name;
        //    //product_item.qty = qty;
        //    //if (product_ViewModels.Product_PromoPrice != 0)
        //    //{
        //    //    product_item.price = product_ViewModels.Product_PromoPrice * qty;
        //    //}
        //    //else
        //    //{
        //    //    product_item.price = product_ViewModels.Product_Price * qty;
        //    //}

        //    //card.product_card.Add(product_item);

        //    //card.total_price = card.total_price + product_item.price;
        //    //return card;
        //}


        // 
        public async Task<IActionResult> DeleteCartCookie(int id) // truyền biến id (id sp), qty (số lượng sp). Dùng để xóa 1 item trong cart
        {
            var cookieCard = HttpContext.Request.Cookies["cart"];
            var card = new ShopCard_ViewModel();
            if (cookieCard != null) // Phải có cookie sẵn
            {
                card = JsonSerializer.Deserialize<ShopCard_ViewModel>(cookieCard); // đổi từ string sang json
                if (card != null) // card k đc null 
                {
                    card = _deletecartcookieService.DeleteCartCookieFunction(card, id); // xử lý sp trong giỏ hàng
                }
                string productvalue = JsonSerializer.Serialize(card); // đổi lại từ jsonobj sang string là productvalue
                CookieOptions option = new CookieOptions();
                option.Secure = true;
                option.HttpOnly = false;
                option.SameSite = SameSiteMode.None;
                option.Path = "/";
                option.Expires = DateTime.Now.AddDays(1);
                HttpContext.Response.Cookies.Append("cart", productvalue, option); // lưu những thay đổi vào cart

                return Json(true); // nếu function chạy thành công thì true
            }
            return Json(false); // nếu function k chạy được thì false
            // json gửi cho ajax đọc kết quả đó
        }

		public async Task<IActionResult> ChangeProductCartQuantity(int id, int qty) // truyền biến id (id sp), qty (số lượng sp). Dùng để xóa 1 item trong cart
		{
			var cookieCard = HttpContext.Request.Cookies["cart"];
			var card = new ShopCard_ViewModel();
			if (cookieCard != null) // Phải có cookie sẵn
			{
				card = JsonSerializer.Deserialize<ShopCard_ViewModel>(cookieCard); // đổi từ string sang json
				if (card != null) // card k đc null 
				{
					card = _changeproductcartquantityService.ChangeProductCartQuantityFunction(card, id, qty); // xử lý sp trong giỏ hàng
				}
				string productvalue = JsonSerializer.Serialize(card); // đổi lại từ jsonobj sang string là productvalue. 
				CookieOptions option = new CookieOptions();
				option.Secure = true;
				option.HttpOnly = false;
				option.SameSite = SameSiteMode.None;
				option.Path = "/";
				option.Expires = DateTime.Now.AddDays(1);
				HttpContext.Response.Cookies.Append("cart", productvalue, option); // lưu những thay đổi mới vào cart

				return Json(true); // nếu function chạy thành công thì true
			}
			return Json(false); // nếu function k chạy được thì false
								// json gửi cho ajax đọc kết quả đó
		}

		//public IActionResult ResetCookie()
		//{
		//    var cookie = HttpContext.Request.Cookies["bg-modal"];
		//    if (cookie != null) // là đang có cookie thì 
		//    {
		//        CookieOptions option = new CookieOptions();
		//        option.Secure = true;
		//        option.HttpOnly = false;
		//        option.SameSite = SameSiteMode.None;
		//        option.Path = "/";
		//        option.Expires = DateTime.Now.AddDays(1);
		//        HttpContext.Response.Cookies.Append("bg-model", "yes", option);
		//    }
		//    return RedirectToAction("Index");
		//}

	}
}
// hướng đi: lấy cart về -> chuyển thành object -> insert object đó vào db
// vấn đề: controller k đọc hiểu đc code js ở cookie value.
// giải quyết: khi nhấn mua sp -> function ajax chạy tạo cart -> dẫn đến controller cookie mới (chuyển js setCart, getCart,... từ shopcartjs qua đây) -> tạo cookie đó.
// Cần thêm: 1 controller cookie, 1 ajax ở productdetail. Sửa: productdetail (js tạo cookie), scpt2 (js show cookie). Code của anh Tín gửi 
// => Xong roi

// mai làm típ. chèn action vào html: <input type="submit" id="btnWrite" formaction="@Url.Action("WriteCookie")" value="Write Cookie" />
// Đường truyền sẽ là: Bấm nút add -> chạy function ajax -> chạy action controller(tạo giá trị trong cookie), k cần trả gì hết -> 
// Về học code C# và ghi lại note logic, viết thêm 1 service và làm gọn code cookie lại (1)
// (2) getCart và hiện bằng js
// (3) Đường đi Insert object cookie value vào db(ở trang checkout): get cookie value đó về -> convert thành obj -> thành 1 model -> dùng service để insert nguyên object đó vào table db.
/* (4) Cách xóa 1 data object trong cart đó: chọn item muốn xóa (nút xóa) bằng js -> làm 1 function ajax  -> controller nhận dữ liệu 
 *                                          -> làm 1 service xóa item đó -> cập nhật lại giỏ hàng cart value bằng C#
 *                                          
 *                                          
 *                                          
 */
