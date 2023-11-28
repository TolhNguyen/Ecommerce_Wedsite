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
        private readonly IAdminMenuService _adminmenuService;

        public DiscountAdminController(ILogger<DiscountAdminController> logger, IDiscountAdminService discountAdminService, IAdminMenuService adminmenuService)
        {
            _logger = logger;
            _discountAdminService = discountAdminService;
            _adminmenuService = adminmenuService;
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
    }
}
