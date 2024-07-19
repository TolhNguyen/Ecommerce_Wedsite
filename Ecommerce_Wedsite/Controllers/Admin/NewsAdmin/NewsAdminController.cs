using Ecommerce_Wedsite.Models;
using Ecommerce_Wedsite.Models.Entities;
using Ecommerce_Wedsite.Models.Helpers.Response;
using Ecommerce_Wedsite.Models.ViewModel;
using Ecommerce_Wedsite.Service.WebApp;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Text.Json;
using static IronPython.Modules._ast;


namespace Ecommerce_Wedsite.Controllers
{
    public class NewsAdminController : Controller
    {
        private readonly ILogger<NewsAdminController> _logger;
        private readonly IAdminMenuService _adminmenuService;
        private readonly INewsAdminService _newsadminService;
        private readonly INewsAdminEditService _newsadmineditService;
        private readonly INewsAdminEditFunctionService _newsadmineditfunctionService;
        private readonly INewsAdminCreateService _newsadmincreateService;

        public NewsAdminController(ILogger<NewsAdminController> logger, IAdminMenuService adminmenuService, INewsAdminService newsadminService, INewsAdminEditService newsadmineditService, INewsAdminEditFunctionService newsadmineditfunctionService, INewsAdminCreateService newsadmincreateService)
        {
            _logger = logger;
            _adminmenuService = adminmenuService;
            _newsadminService = newsadminService;
            _newsadmineditService = newsadmineditService;
            _newsadmineditfunctionService = newsadmineditfunctionService;
            _newsadmincreateService = newsadmincreateService;
        }
        [Route("~/newsadmin")]
        public async Task<IActionResult> NewsAdmin()
        {
            var All = new AllLayout();

            var adminmenu_ViewModels = await _adminmenuService.AdminMenu_ServiceTest();
            var news_ViewModels = await _newsadminService.Service_Test(); // view model xài chung dc, nhưng service khác
            All.adminmenu_ViewModels = adminmenu_ViewModels.Data;
            All.news_ViewModels = news_ViewModels.Data;
            return View("NewsAdmin", All);
        }
        [Route("~/newsadminedit")]
        public async Task<IActionResult> NewsAdminEdit(int News_Id) // Chỉ nhận mỗi id thôi đủ. Tạo thêm trang riêng function riêng
        {
            var All = new AllLayout();

            var adminmenu_ViewModels = await _adminmenuService.AdminMenu_ServiceTest();
            var news_ViewModels = await _newsadmineditService.Service_Test(News_Id);
            All.adminmenu_ViewModels = adminmenu_ViewModels.Data;
            All.news_ViewModels = news_ViewModels.Data;
            return View("NewsAdminEdit", All);
        }

        [HttpPost]
        public async Task<IActionResult> NewsAdminEditFunction(News newsitem) // controller kiểu ajax thì mới đọc đc                                                                          // Tạo thêm trang riêng function riêng
        {
            // cần có 1  model layout admin riêng
            var news_ViewModels = await _newsadmineditfunctionService.Service_Test(newsitem);
            if (news_ViewModels.success == true)
            {
                return Json(true);
            }
            return Json(false);
        }

        [Route("~/newsadmincreate")]
        public async Task<IActionResult> NewsAdminCreate()
        {
            var All = new AllLayout();
            var adminmenu_ViewModels = await _adminmenuService.AdminMenu_ServiceTest();
            All.adminmenu_ViewModels = adminmenu_ViewModels.Data;
            return View("NewsAdminCreate", All);
        }

        [HttpPost]
        public async Task<IActionResult> NewsAdminCreateFunction(News newsitem)
        {
            var news_ViewModels = await _newsadmincreateService.Service_Test(newsitem);
            if (news_ViewModels.success == true)
            {
                return Json(true);
            }
            return Json(false);
        }
        /*
            [Route("~/discountadmindelete")]
            public async Task<IActionResult> DiscountAdminDelete(Discountt discountitem) // Delete, Edit nên chung 1 controller hay tách ra
            {
                var All = new AllLayout();

                var discountt_ViewModels = await _discountadmindeleteService.Service_Test(discountitem);

                var adminmenu_ViewModels = await _adminmenuService.AdminMenu_ServiceTest();
                All.adminmenu_ViewModels = adminmenu_ViewModels.Data;

                All.discountt_ViewModels = discountt_ViewModels.Data;

                return View("SuccessPage", All); // sau khi chạy service ở trên r mới trả view ra
            }
        */
    }
}

