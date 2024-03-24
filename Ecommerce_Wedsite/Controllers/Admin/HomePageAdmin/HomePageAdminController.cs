using Ecommerce_Wedsite.Models;
using Ecommerce_Wedsite.Models.Entities;
using Ecommerce_Wedsite.Models.Helpers.Response;
using Ecommerce_Wedsite.Models.ViewModel;
using Ecommerce_Wedsite.Service.WebApp;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Text.Json;


namespace Ecommerce_Wedsite.Controllers
{
    public class HomePageAdminController : Controller
    {
        private readonly ILogger<HomePageAdminController> _logger;
        private readonly IAdminMenuService _adminmenuService;
        private readonly IHomePageAdminService _homepageadminService;

        public HomePageAdminController(ILogger<HomePageAdminController> logger, IAdminMenuService adminmenuService, IHomePageAdminService homepageadminService)
        {
            _logger = logger;
            _adminmenuService = adminmenuService;
            _homepageadminService = homepageadminService;
        }

        [Route("~/homepageadmin")]
        public async Task<IActionResult> HomePageAdmin(HomePage homepageitem)
        {
            var All = new AllLayout();

            var adminmenu_ViewModels = await _adminmenuService.AdminMenu_ServiceTest();
            var homepage_ViewModels = await _homepageadminService.HomePageAdmin();
            All.homepage_ViewModels = homepage_ViewModels.Data;
            All.adminmenu_ViewModels = adminmenu_ViewModels.Data;

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

        //[Route("~/headeradmincreatefunction")]
        //public async Task<IActionResult> HeaderAdminCreateFunction(Header headeritem) // Function create
        //{
        //    var All = new AllLayout();

        //    var header_ViewModels = await _headeradminCreateService.Service_Test(headeritem);

        //    var adminmenu_ViewModels = await _adminmenuService.AdminMenu_ServiceTest();
        //    All.adminmenu_ViewModels = adminmenu_ViewModels.Data;
        //    All.header_ViewModels = header_ViewModels.Data;

        //    return View("SuccessPage", All); // sau khi chạy service ở trên r mới trả view ra
        //}

        //[Route("~/headeradminedit")]
        //public async Task<IActionResult> HeaderAdminEdit(int Header_Id) // Tạo thêm trang riêng function riêng
        //{
        //    var All = new AllLayout();

        //    var header_ViewModels = await _headeradminEditService.Service_Test(Header_Id);

        //    var adminmenu_ViewModels = await _adminmenuService.AdminMenu_ServiceTest();
        //    All.adminmenu_ViewModels = adminmenu_ViewModels.Data;
        //    All.header_ViewModels = header_ViewModels.Data;

        //    return View("HeaderAdminEdit", All);
        //}

        //public async Task<IActionResult> HeaderAdminEditFunction(Header headeritem) // controller kiểu ajax thì mới đọc đc 
        //                                                                            // Tạo thêm trang riêng function riêng
        //{
        //    // cần có 1  model layout admin riêng
        //    var header_ViewModels = await _headeradminEditFunctionService.Service_Test(headeritem);
        //    return Json(header_ViewModels); // sau khi chạy service ở trên r mới trả view ra
        //}
    }
}


