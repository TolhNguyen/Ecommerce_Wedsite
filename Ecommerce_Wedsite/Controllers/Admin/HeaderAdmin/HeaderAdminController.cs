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
    public class HeaderAdminController : Controller
    {
        private readonly ILogger<HeaderAdminController> _logger;
        private readonly IHeaderAdminDeleteService _headeradminDeleteService;
        private readonly IHeaderAdminCreateService _headeradminCreateService;
        private readonly IHeaderAdminEditService _headeradminEditService;
        private readonly IHeaderAdminService _headeradminService;
        private readonly IHeaderAdminEditFunctionService _headeradminEditFunctionService;
        private readonly IAdminMenuService _adminmenuService;
        private readonly IAdminService _adminService;
        private readonly INoticeAdminService _noticeadminService;

        public HeaderAdminController(ILogger<HeaderAdminController> logger, IHeaderAdminService headeradminService, IHeaderAdminEditService headeradminEditService, IHeaderAdminDeleteService headeradminDeleteService, IHeaderAdminCreateService headeradminCreateService, IHeaderAdminEditFunctionService headeradminEditFunctionService, IAdminMenuService adminmenuService, IAdminService adminService, INoticeAdminService noticeadminService)
        {
            _logger = logger;
            _headeradminService = headeradminService;
            _headeradminDeleteService = headeradminDeleteService;
            _headeradminCreateService = headeradminCreateService;
            _headeradminEditService = headeradminEditService;
            _headeradminEditFunctionService = headeradminEditFunctionService;
            _adminmenuService = adminmenuService;
            _adminService = adminService;
            _noticeadminService = noticeadminService;
        }

        [Route("~/headeradmin")]
        public async Task<IActionResult> HeaderAdmin() 
        {
            var All = new AllLayout();

            int id = 0;
            string? idstr = HttpContext.Request.Cookies["adminid"];
            var check = int.TryParse(idstr, out id);
            var admin_ViewModels = await _adminService.AdminInfo(id);
            var noticookie = HttpContext.Request.Cookies["noticookie"];
            if (noticookie != null) // nếu có tb rồi, chỉ hiện thôi
            {
                var notiVM = new NoticeAdmin_ViewModel();
                notiVM = JsonSerializer.Deserialize<NoticeAdmin_ViewModel>(noticookie);
                All.noticeadmin_ViewModels = notiVM;
            }

            var header_ViewModels = await _headeradminService.Service_Test();
            var adminmenu_ViewModels = await _adminmenuService.AdminMenu_ServiceTest();
            All.admin_ViewModels = admin_ViewModels.Data;
            All.adminmenu_ViewModels = adminmenu_ViewModels.Data;
            All.header_ViewModels = header_ViewModels.Data;

            return View("HeaderAdmin", All);
        }

        [Route("~/headeradmindelete")]
        public async Task<IActionResult> HeaderAdminDelete(int id, string pagename) // Chưa dùng noticeadmin được cần sửa.ffff
        {
            var All = new AllLayout();

            var header_ViewModels = await _headeradminDeleteService.Service_Test(id);

            var adminmenu_ViewModels = await _adminmenuService.AdminMenu_ServiceTest();
            All.adminmenu_ViewModels = adminmenu_ViewModels.Data;
            All.header_ViewModels = header_ViewModels.Data;

            return RedirectToAction("HeaderAdmin", "HeaderAdmin"); // sau khi chạy service ở trên r mới trả view ra
        }

        [Route("~/headeradmincreate")]
        public async Task<IActionResult> HeaderAdminCreate() // không cần service chỉ cần trả trang thôi. trang
        {
            var All = new AllLayout();

            int id = 0;
            string? idstr = HttpContext.Request.Cookies["adminid"];
            var check = int.TryParse(idstr, out id);
            var admin_ViewModels = await _adminService.AdminInfo(id);
            var noticookie = HttpContext.Request.Cookies["noticookie"];
            if (noticookie != null) // nếu có tb rồi
            {
                var notiVM = new NoticeAdmin_ViewModel();
                notiVM = System.Text.Json.JsonSerializer.Deserialize<NoticeAdmin_ViewModel>(noticookie);
                All.noticeadmin_ViewModels = notiVM;
            }

            var header_ViewModels = await _headeradminService.Service_Test();
            var adminmenu_ViewModels = await _adminmenuService.AdminMenu_ServiceTest();

            All.adminmenu_ViewModels = adminmenu_ViewModels.Data;
            All.header_ViewModels = header_ViewModels.Data;
            All.admin_ViewModels = admin_ViewModels.Data;
            return View("HeaderAdminCreate", All);
        }

        public async Task<IActionResult> HeaderAdminCreateFunction(Header headeritem, string pagename) // Function create
        {
            var All = new AllLayout();

            var header_ViewModels = await _headeradminCreateService.Service_Test(headeritem);

            var adminmenu_ViewModels = await _adminmenuService.AdminMenu_ServiceTest();
            All.adminmenu_ViewModels = adminmenu_ViewModels.Data;
            All.header_ViewModels = header_ViewModels.Data;

            return RedirectToAction("NoticeAdminFunction", "Admin", new { pagename = pagename }); // sau khi chạy service ở trên r mới trả view ra
        }

        [Route("~/headeradminedit")]
        public async Task<IActionResult> HeaderAdminEdit(int Header_Id) // Tạo thêm trang riêng function riêng
        {
            var All = new AllLayout();

            int id = 0;
            string? idstr = HttpContext.Request.Cookies["adminid"];
            var check = int.TryParse(idstr, out id);
            var admin_ViewModels = await _adminService.AdminInfo(id);
            var noticookie = HttpContext.Request.Cookies["noticookie"];
            if (noticookie != null) // nếu có tb rồi
            {
                var notiVM = new NoticeAdmin_ViewModel();
                notiVM = System.Text.Json.JsonSerializer.Deserialize<NoticeAdmin_ViewModel>(noticookie);
                All.noticeadmin_ViewModels = notiVM;
            }

            var header_ViewModels = await _headeradminEditService.Service_Test(Header_Id);

            var adminmenu_ViewModels = await _adminmenuService.AdminMenu_ServiceTest();
            All.adminmenu_ViewModels = adminmenu_ViewModels.Data;
            All.header_ViewModels = header_ViewModels.Data;
            All.admin_ViewModels = admin_ViewModels.Data;

            return View("HeaderAdminEdit", All);
        }

        public async Task<IActionResult> HeaderAdminEditFunction(Header headeritem, string pagename) // controller kiểu ajax thì mới đọc đc 
            // Tạo thêm trang riêng function riêng
        {
            // cần có 1  model layout admin riêng
            var header_ViewModels = await _headeradminEditFunctionService.Service_Test(headeritem);
            return RedirectToAction("NoticeAdminFunction", "Admin", new { pagename = pagename }); // sau khi chạy service ở trên r mới trả view ra
        }
    }
}
/*
    - Tìm cách gom những action crud này vào 1 controller nhỏ và gọi ra lại trong controller này 
    - Tìm cách gom các function lại với nhau thành ít controller (nhiều service)
    => Giải: do action form controller và ajax chạy cùng nhau (nên ajax k chạy dc, dùng 1 trong 2).Dùng 1 trong 3 cách chạy:
            + 1. dùng form action dẫn tới controller thôi
            + 2. dùng ajax nhận giá trị dẫn tới controller
            + 3. dùng submit form function
 */

