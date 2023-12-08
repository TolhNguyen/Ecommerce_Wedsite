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
    public class CustomerCheckoutAdminController : Controller
    {
        private readonly ILogger<CustomerCheckoutAdminController> _logger;
        private readonly IAdminMenuService _adminmenuService;
        private readonly ICustomerCheckoutAdminService _customercheckoutadminService;

        public CustomerCheckoutAdminController(ILogger<CustomerCheckoutAdminController> logger, IAdminMenuService adminmenuService, ICustomerCheckoutAdminService customercheckoutadminService)
        {
            _logger = logger;
            _adminmenuService = adminmenuService;
            _customercheckoutadminService = customercheckoutadminService;
        }

        [Route("~/customercheckoutadmin")]
        public async Task<IActionResult> CustomerCheckoutAdmin() // Delete, Edit nên chung 1 controller hay tách ra
        {
            var All = new AllLayout();

            var adminmenu_ViewModels = await _adminmenuService.AdminMenu_ServiceTest();
            var customercheckout_ViewModels = await _customercheckoutadminService.Service_Test();

            All.adminmenu_ViewModels = adminmenu_ViewModels.Data;
            All.customercheckout_ViewModels = customercheckout_ViewModels.Data; 

            return View("CustomerCheckoutAdmin", All);
        }
    }
}
