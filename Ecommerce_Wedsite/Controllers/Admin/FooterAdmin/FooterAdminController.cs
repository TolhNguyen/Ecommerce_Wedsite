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
    public class FooterAdminController : Controller
    {
        private readonly ILogger<FooterAdminController> _logger;
        private readonly IFooterAdminService _footeradminService;
        private readonly IAdminMenuService _adminmenuService;
        private readonly IAdminService _adminService;

        public FooterAdminController(ILogger<FooterAdminController> logger, IFooterAdminService footeradminService, IAdminMenuService adminmenuService, IAdminService adminService)
        {
            _logger = logger;
            _footeradminService = footeradminService;
            _adminmenuService = adminmenuService;
            _adminService = adminService;
        }

        [Route("~/footeradmin")]
        public async Task<IActionResult> FooterAdmin()
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

            var footer_ViewModels = await _footeradminService.Service_Test();
            var adminmenu_ViewModels = await _adminmenuService.AdminMenu_ServiceTest();


            All.adminmenu_ViewModels = adminmenu_ViewModels.Data;
            All.admin_ViewModels = admin_ViewModels.Data;
            All.footer_ViewModels = footer_ViewModels.Data;

            return View("FooterAdmin", All);
        }
    }
}
