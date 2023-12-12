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
    public class DiscountAdminController : Controller
    {
        private readonly ILogger<DiscountAdminController> _logger;
        private readonly IDiscountAdminService _discountAdminService;
        private readonly IDiscounttAdminEditService _discounttadmineditService;
        private readonly IAdminMenuService _adminmenuService;
        private readonly IDiscountAdminEditFunctionService _discountadmineditfunctionService;
        private readonly IDiscounttAdminDeleteService _discountadmindeleteService;

        public DiscountAdminController(ILogger<DiscountAdminController> logger, IDiscountAdminService discountAdminService, IAdminMenuService adminmenuService, IDiscounttAdminEditService discounttadmineditService, IDiscountAdminEditFunctionService discountadmineditfunctionService, IDiscounttAdminDeleteService discountadmindeleteService)
        {
            _logger = logger;
            _discountAdminService = discountAdminService;
            _adminmenuService = adminmenuService;
            _discounttadmineditService = discounttadmineditService;
            _discountadmineditfunctionService = discountadmineditfunctionService;
            _discountadmindeleteService = discountadmindeleteService;
        }
        [Route("~/discountadmin")]
        public async Task<IActionResult> DiscountAdmin()
        {
            var All = new AllLayout();

            var discount_ViewModels = await _discountAdminService.Service_Test();

            var adminmenu_ViewModels = await _adminmenuService.AdminMenu_ServiceTest();
            All.adminmenu_ViewModels = adminmenu_ViewModels.Data;

            All.discountt_ViewModels = discount_ViewModels.Data;

            return View("DiscountAdmin", All);
        }
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
    }
}
