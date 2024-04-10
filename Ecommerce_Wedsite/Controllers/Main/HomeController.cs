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
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly INewWebAppService _newWebAppService; //Tạo biến cho Interface muốn sử dụng
        private readonly IHomePageService _homepageService;
        private readonly IPictureService _pictureService;
        private readonly ILogoService _logoService;
        private readonly ICheckoutAdminService _checkoutadminService;
        private readonly IHeaderAndFooterService _headerandfooterService;
        private readonly IDiscounttService _discounttService;
        private readonly ISubHeaderService _subheaderService;
        private readonly IVideoService _videoService;
        private readonly IHomePageProductDisplayService _homepageproductdisplayService;
        // private readonly ISort

        public HomeController(ILogger<HomeController> logger, INewWebAppService newWebAppService, IHomePageService homepageService, IPictureService pictureService, ILogoService logoService, ICheckoutAdminService checkoutadminService, IHeaderAndFooterService headerandfooterService, IDiscounttService discounttService, ISubHeaderService subheaderService, IVideoService videoService, IHomePageProductDisplayService homepageproductdisplayService) //Khai báo Interface
        {
            _logger = logger;
            _newWebAppService = newWebAppService; // Tạo 1 tham chiếu 
            _homepageService = homepageService;
            _pictureService = pictureService;
            _logoService = logoService;
            _checkoutadminService = checkoutadminService;
            _discounttService = discounttService;
            _headerandfooterService = headerandfooterService;
            _subheaderService = subheaderService;
            _videoService = videoService;
            _homepageproductdisplayService = homepageproductdisplayService;
        }
        /// <summary>
        /// Trang Chu
        /// </summary>
        /// <returns></returns>

        [Route("~/")]
        public async Task<IActionResult> Index() // Controller cho service tương ứng
        {
            var All = new AllLayout();// Mode Return 

            var headerandfooter_ViewModels = await _headerandfooterService.HeaderAndFooter_ServiceTest();
            var homepage_ViewModels = await _homepageService.HomePage_ServiceTest();
			var homepageproductdisplay_ViewModels = await _homepageproductdisplayService.GetProductDisplay(homepage_ViewModels.Data.homepage_Vl1); // truyền list LV1 vào
			var picture_ViewModels = await _pictureService.Service_Test();
            var logo_ViewModels = await _logoService.Service_Test();
            var discountt_ViewModels = await _discounttService.PopupDiscount();
            var subheader_ViewModels = await _subheaderService.SubHeader();
            var video_ViewModels = await _videoService.Service_Test();

            All.headerandfooter_ViewModels = headerandfooter_ViewModels.Data;
            All.homepage_ViewModels = homepage_ViewModels.Data;
            All.picture_ViewModels = picture_ViewModels.Data;
            All.logo_ViewModels = logo_ViewModels.Data;
            All.discountt_ViewModels = discountt_ViewModels.Data;
            All.subheader_ViewModels = subheader_ViewModels.Data;
            All.video_ViewModels = video_ViewModels.Data;
            All.homepageproductdisplay_ViewModels = homepageproductdisplay_ViewModels.Data;

            return View("WebApp/Index", All); // Truyền dữ liệu. Hiển thị view đúng địa chỉ. 
        }

        [Route("~/cart")]
        public async Task<IActionResult> Cart()
        {
            var All = new AllLayout();
            var headerandfooter_ViewModels = await _headerandfooterService.HeaderAndFooter_ServiceTest();
            var picture_ViewModels = await _pictureService.Service_Test();
            var discountt_ViewModels = await _discounttService.PopupDiscount();
            var subheader_ViewModels = await _subheaderService.SubHeader();

            All.headerandfooter_ViewModels = headerandfooter_ViewModels.Data;
            All.picture_ViewModels = picture_ViewModels.Data;
            All.discountt_ViewModels = discountt_ViewModels.Data;
            All.subheader_ViewModels = subheader_ViewModels.Data;

            // Cách lấy cart model chỉ có ở viewmodel mang ra view sử dụng: Cần có setup viewmodel trc trong All:

			var cookieCard = HttpContext.Request.Cookies["cart"]; // lấy value từ cart cookie
			var card = new ShopCard_ViewModel(); // tạo 1 biến lưu dữ liệu đó
            if (cookieCard != null) // là đã có cookie rồi
            {
				card = JsonSerializer.Deserialize<ShopCard_ViewModel>(cookieCard); // truyền dữ liệu object vào card
                All.shopcard_ViewModels = card; // lưu nó vào viewmodel để hiện ra.
            }
            else
            {
                return RedirectToAction("Index");
            }

		    return View("WebApp/Cart", All);
        }

        [Route("~/checkoutadmin")]
        public async Task<IActionResult> CheckoutAdmin()
        {
            var All = new AllLayout();

            var checkout_ViewModels = await _checkoutadminService.Service_Test();

            All.checkout_ViewModels = checkout_ViewModels.Data;

            return View("WebApp/CheckoutAdmin", All);
        }

        /* - Tách Cart thành 1 controller khác 
           - Tạo 1 folder View riêng khác 
           - Gọi view đó vào layout (html async)
           - Sửa trang Cart tiếp*/

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
/*
 Lưu ý:
    - cHO showCart hiện lên khi gọi / chạy trang
    - thêm link cho sản phẩm
    - hiện bảng sp mỗi khi add cart9
    - chia nhỏ controller, service ra thành từng thư mục riêng biệt nhiều tầng lớp để dễ quản lý và đẹp
    - có thể cho chạy nhìu service nhỏ chung 1 service lớn để gọn (vd: edit, delete, create trong 1 ...admin)
    - gọi từng service nhỏ trong những service lớn hơn, ... tương tự cho đến controller lớn nhất (càng gọn càng tốt).
    - Controller chứa nhiều service lớn (chính phục vụ cho trang controller đó)
    - service lớn chứa nhiều service nhỏ (là nhiều functions chạy trong 1 service), gom nhiều function services vào thành 1 file thôi
    - có thể đổi tên cho "ServiceTest()" để nhiều service nhỏ khác nhau chạy chung đc trong 1 service lớn.
    - Giai đoạn 1: xong bên trên, cookie controller
    - Giai đoạn 2: thực hiện thêm các function hành động service nhỏ cho người dùng

Cách commit code: 
    - Vào github desktop:
    - Ở changes, bên dưới có description, gõ des cho update mới nhất vào, sau đó commit ở fetch
=> tìm hiểu thêm về Git hub, cách upcode mà k lấy file obj, bin lên (vì sẽ bị conflick)

Cần làm: tối ưu trang web: ưu tiên trang home, trang checkout, trang product:
    1. Thêm hình ảnh sản phẩm ở check out
 */