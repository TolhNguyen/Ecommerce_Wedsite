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
    public class NewsAdminController : Controller
    {
        private readonly ILogger<NewsAdminController> _logger;
        private readonly IAdminMenuService _adminmenuService;

        public NewsAdminController(ILogger<NewsAdminController> logger, IAdminMenuService adminmenuService)
        {
            _logger = logger;
            _adminmenuService = adminmenuService;
        }
        [Route("~/newsadmin")]
        public async Task<IActionResult> NewsAdmin()
        {
            var All = new AllLayout();

            var adminmenu_ViewModels = await _adminmenuService.AdminMenu_ServiceTest();
            All.adminmenu_ViewModels = adminmenu_ViewModels.Data;

            return View("NewsAdmin", All);
        }
        /*
            [Route("~/discountadminedit")]
            public async Task<IActionResult> DiscountAdminEdit(int Discount_Id) // Tạo thêm trang riêng function riêng
            {
                var All = new AllLayout();

                var discountt_ViewModels = await _discounttadmineditService.Service_Test(Discount_Id);
                var adminmenu_ViewModels = await _adminmenuService.AdminMenu_ServiceTest();
                All.adminmenu_ViewModels = adminmenu_ViewModels.Data;
                All.discountt_ViewModels = discountt_ViewModels.Data;

                return View("DiscountAdminEdit", All);
            }   

            public async Task<IActionResult> DiscountAdminEditFunction(Discountt discountitem) // controller kiểu ajax thì mới đọc đc                                                                          // Tạo thêm trang riêng function riêng
            {
                // cần có 1  model layout admin riêng
                var discount_ViewModels = await _discountadmineditfunctionService.Service_Test(discountitem);
                return Json(discount_ViewModels); // sau khi chạy service ở trên r mới trả thông báo bằng ajax. rồi về lại trang chủ.
            }

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

