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
    public class HomePageAdminController : Controller
    {
        private readonly ILogger<HomePageAdminController> _logger;
        private readonly IAdminMenuService _adminmenuService;
        private readonly IHomePageAdminService _homepageadminService;
        private readonly IHomePageAdminEditService _homepageadmineditService;
        private readonly IHomePageAdminEditFunctionService _homepageadmineditfunctionService;
        private readonly IHomePageAdminIdTableService _homepageadminidtableService;
        private readonly IIdTableFuncService _idtablefuncService;
        private readonly INoticeAdminService _noticeadminservice;
        private readonly IAdminService _adminService;

        public HomePageAdminController(ILogger<HomePageAdminController> logger, IAdminMenuService adminmenuService, IHomePageAdminService homepageadminService, IHomePageAdminEditService homepageadmineditService, IHomePageAdminEditFunctionService homepageadmineditfunctionService, IHomePageAdminIdTableService homepageadminidtableService, IIdTableFuncService idtablefuncService, INoticeAdminService noticeadminservice, IAdminService adminService)
        {
            _logger = logger;
            _adminmenuService = adminmenuService;
            _homepageadminService = homepageadminService;
            _homepageadmineditService = homepageadmineditService;
            _homepageadmineditfunctionService = homepageadmineditfunctionService;
            _homepageadminidtableService = homepageadminidtableService;
            _idtablefuncService = idtablefuncService;
            _noticeadminservice = noticeadminservice;
            _adminService = adminService;
        }

        [Route("~/homepageadmin")]
        public async Task<IActionResult> HomePageAdmin()
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

            var adminmenu_ViewModels = await _adminmenuService.AdminMenu_ServiceTest();
            var homepage_ViewModels = await _homepageadminService.HomePageAdmin();
            All.homepage_ViewModels = homepage_ViewModels.Data;
            All.adminmenu_ViewModels = adminmenu_ViewModels.Data;
            All.admin_ViewModels = admin_ViewModels.Data;

            return View("HomePageAdmin", All);
        }

        //[Route("~/headeradmindelete")]
        //public async Task<IActionResult> HeaderAdminDelete(Header headeritem) // Delete, Edit nên chung 1 controller hay tách ra
        //{
        //    var All = new AllLayout();

        //    var header_ViewModels = await _headeradminDeleteService.Service_Test(headeritem);

        //    var adminmenu_ViewModels = await _adminmenuService.AdminMenu_ServiceTest();
        //    All.adminmenu_ViewModels = adminmenu_ViewModels.Data;
        //    All.header_ViewModels = header_ViewModels.Data;

        //    return View("SuccessPage", All); // sau khi chạy service ở trên r mới trả view ra
        //}

        //[Route("~/headeradmincreate")]
        //public async Task<IActionResult> HeaderAdminCreate() // không cần service chỉ cần trả trang thôi. trang
        //{
        //    var All = new AllLayout();

        //    var header_ViewModels = await _headeradminService.Service_Test();

        //    var adminmenu_ViewModels = await _adminmenuService.AdminMenu_ServiceTest();
        //    All.adminmenu_ViewModels = adminmenu_ViewModels.Data;
        //    All.header_ViewModels = header_ViewModels.Data;

        //    return View("HeaderAdminCreate", All);
        //}

        public async Task<IActionResult> HomePageAdminEditFunction(HomePage homepageitem) // controller kiểu ajax thì mới đọc đc 
                                                                                    // Tạo thêm trang riêng function riêng
        {
            // cần có 1  model layout admin riêng
            var homepage_ViewModels = await _homepageadmineditfunctionService.EditFunction(homepageitem);
            return Json(homepage_ViewModels); // sau khi chạy service ở trên r mới trả view ra
        }

        [Route("~/homepageadminedit")]
        public async Task<IActionResult> HomePageAdminEdit(int HomePage_Id) // chỉ cần id thôi để xác định thôi
        {
            var All = new AllLayout();

            var homepage_ViewModels = await _homepageadmineditService.HomePageAdminEdit(HomePage_Id);    
            var adminmenu_ViewModels = await _adminmenuService.AdminMenu_ServiceTest();
            var idtable_ViewModels = await _homepageadminidtableService.IdTable();
            All.product_ViewModels = idtable_ViewModels.Data;
            All.adminmenu_ViewModels = adminmenu_ViewModels.Data;
            All.homepage_ViewModels = homepage_ViewModels.Data; 

            return View("HomePageAdminEdit", All);
        }

        public async Task<IActionResult> HomePageAdminIdTableFunction(List<string> idsArray, int HomePage_Id) // nên đổi sang ajax
        {
            var result = await _idtablefuncService.IdTableFunction(idsArray, HomePage_Id);
            if(result.success == true)
            {
                return Json(result);
            }
            return Json(result);
        }

    }
}


