using Ecommerce_Wedsite.Models;
using Ecommerce_Wedsite.Models.Entities;
using Ecommerce_Wedsite.Models.Helpers.Response;
using Ecommerce_Wedsite.Models.ViewModel;
using Ecommerce_Wedsite.Service.WebApp;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Diagnostics;
using System.Net;
using System.Text.Json;
using static IronPython.Runtime.Profiler;


namespace Ecommerce_Wedsite.Controllers
{
    public class AdminController : Controller
    {
        private readonly ILogger<AdminController> _logger;
        private readonly IAdminService _adminService;
        private readonly IHeaderAndFooterService _headerandfooterService;
        private readonly IAdminMenuService _adminmenuService;
        private readonly IUserWebNameService _userwebnameService;
        private readonly INoticeAdminService _noticeadminService;
        private readonly IProductCheckoutService _productcheckoutService;

        public AdminController(ILogger<AdminController> logger, IAdminService adminService, IHeaderAndFooterService headerandfooterService, IAdminMenuService adminmenuService, IUserWebNameService userwebnameService, INoticeAdminService noticeadminService, IProductCheckoutService productcheckoutService)
        {
            _logger = logger;
            _adminService = adminService;
            _headerandfooterService = headerandfooterService;
            _adminmenuService = adminmenuService;
            _userwebnameService = userwebnameService;
            _noticeadminService = noticeadminService;
            _productcheckoutService = productcheckoutService;
        }

        [Route("~/adminloginpage")]
        public IActionResult AdminLoginPage() // trang admin login
        {
            //var All = new AllLayout();
            //var headerandfooter_ViewModels = await _headerandfooterService.HeaderAndFooter_ServiceTest();
            //All.headerandfooter_ViewModels = headerandfooter_ViewModels.Data;
            return View("AdminLoginPage");
        }

        [Route("~/adminpage")]
        public async Task<IActionResult> AdminPage() // Trang admin. Cần ktra lại code cho sạch gọn lại.
        {
            var All = new AllLayout();
            var adminmenu_ViewModels = await _adminmenuService.AdminMenu_ServiceTest();
            var noticookie = HttpContext.Request.Cookies["noticookie"];

            int id = 0;
            string? idstr = HttpContext.Request.Cookies["adminid"];
            int.TryParse(idstr, out id);

            int dem = 1; //đếm số lần đăng nhập
            string? demstr = HttpContext.Request.Cookies["dem"];
            int.TryParse(demstr, out dem);

            if (noticookie != null) // nếu có tb rồi
            {
                if (dem > 1) // nếu đăng nhập nhiều lần
                {
                    var notiVM = new NoticeAdmin_ViewModel();
                    notiVM = await _noticeadminService.NoticeAdminFunctionFake(notiVM, id, dem);
                    All.noticeadmin_ViewModels = notiVM;
                    //Tạo cookie rỗng mới:
                    string notistr = JsonSerializer.Serialize(notiVM); // chuyển kiểu str
                    CookieOptions option = new CookieOptions();
                    option.Secure = true; // secure là trữ value vào cookie lại
                    option.HttpOnly = false; // httponly này là khóa cookie thành private k cho hiện, xài
                    option.SameSite = SameSiteMode.None;
                    option.Path = "/";
                    option.Expires = DateTime.Now.AddDays(1);
                    HttpContext.Response.Cookies.Append("noticookie", notistr, option);
                }
                else // show tb nếu = 0
                {
                    var notiVM = new NoticeAdmin_ViewModel();
                    notiVM = JsonSerializer.Deserialize<NoticeAdmin_ViewModel>(noticookie);
                    All.noticeadmin_ViewModels = notiVM;
                }
                
            }

            else // nếu chưa có. Thì tạo Fake và luu vào
            {
                var notiVM = new NoticeAdmin_ViewModel();
                notiVM = await _noticeadminService.NoticeAdminFunctionFake(notiVM, id, dem);
                All.noticeadmin_ViewModels = notiVM;
                //Tạo cookie rỗng mới:
                string notistr = JsonSerializer.Serialize(notiVM); // chuyển kiểu str
                CookieOptions option = new CookieOptions();
                option.Secure = true; // secure là trữ value vào cookie lại
                option.HttpOnly = false; // httponly này là khóa cookie thành private k cho hiện, xài
                option.SameSite = SameSiteMode.None;
                option.Path = "/";
                option.Expires = DateTime.Now.AddDays(1);
                HttpContext.Response.Cookies.Append("noticookie", notistr, option);
            }

            var productcheckout_ViewModels = await _productcheckoutService.PorductCheckoutDisplay();

            var admin_ViewModels = await _adminService.AdminInfo(id);
            All.productcheckout_ViewModels = productcheckout_ViewModels.Data;
            All.adminmenu_ViewModels = adminmenu_ViewModels.Data;
            All.admin_ViewModels = admin_ViewModels.Data;
            
            return View("AdminPage", All);
        }


    //C1: Dùng ajax để login với js:
    //public async Task<ResponseMessageObject<Admin_ViewModel>> AdminLoginPageFunction(string? userName, string? passWord) // function login ajax. Nên để dưới 
    //{
    //    var All = new AllLayout();

    //    var admin_ViewModels = await _adminService.Service_Test(userName, passWord);

    //    All.admin_ViewModels = admin_ViewModels.Data;

    //    return admin_ViewModels;
    //}

    //C2:
        [Route("~/adminloginpagefunction")]
        public async Task<IActionResult> AdminLoginPageFunction(string? userName, string? passWord) // function login ajax. Nên để dưới 
        {
            var admin_ViewModels = await _adminService.Service_Test(userName, passWord); // lấy kết quả kiểm tra ra

            if (admin_ViewModels.success == true) // nếu kq đúng:
            {
                int id = 0;
                int idcookie = await _userwebnameService.WebNameAdminId(userName, id);

                // nên đổi sang lưu cookie
                //var noticefake = await _noticeadminService.NoticeAdminFunctionFake();

                CookieOptions option = new CookieOptions();
                option.Secure = true;
                option.HttpOnly = false;
                option.SameSite = SameSiteMode.None;
                option.Path = "/";
                option.Expires = DateTime.Now.AddDays(1);
                HttpContext.Response.Cookies.Append("adminid", idcookie.ToString(), option);

                int dem = 1;
                if (HttpContext.Request.Cookies["dem"] != null) // nếu có giá trị
                {
                    int.TryParse(HttpContext.Request.Cookies["dem"], out dem); // ép kiểu sang số dếm
                    dem++;
                    HttpContext.Response.Cookies.Append("dem", dem.ToString(), option);
                }
                else // nếu k có giá trị thì là 1
                {
                    HttpContext.Response.Cookies.Append("dem", dem.ToString(), option);
                }
                return RedirectToAction("AdminPage"); // cài id admin và name lên cookie
            }
            else return RedirectToAction("AdminLoginPage"); // kq sai
        }

        [Route("~/noticeadminfunction")]
        public async Task<IActionResult> NoticeAdminFunction(string pagename) // nội dung và id hành động đó.
        {
            // Cần test và cb content.
            var id = 0;
            var content = pagename + " đã thay đổi!";
            string? idstr = HttpContext.Request.Cookies["adminid"];
            var check = int.TryParse(idstr, out id);
            var noticookie = HttpContext.Request.Cookies["noticookie"];
            var notiVM = new NoticeAdmin_ViewModel();
            if (check == true && noticookie != null) // đã có cookie r:
            {
                notiVM = JsonSerializer.Deserialize<NoticeAdmin_ViewModel>(noticookie); // lấy dữ liệu cookie ra
                await _noticeadminService.NoticeAdminFunction(notiVM, content, id); // cập nhật thông báo
                string notistr = JsonSerializer.Serialize(notiVM); // chuyển lại kiểu str
                CookieOptions option = new CookieOptions();
                option.Secure = true; // secure là trữ value vào cookie lại
                option.HttpOnly = false; // httponly này là khóa cookie thành private k cho hiện, xài
                option.SameSite = SameSiteMode.None;
                option.Path = "/";
                option.Expires = DateTime.Now.AddDays(1);
                HttpContext.Response.Cookies.Append("noticookie", notistr, option);
                //var result = new {noti = notistr, success = true }; //id admin va tb. Gom chung vào 1 new và tên biến giống nhau
                return Json(true);
            }
            //else // nếu k có giá trị nào trước thì
            //{

            //}
            return Json(false);
        }
    }
}
