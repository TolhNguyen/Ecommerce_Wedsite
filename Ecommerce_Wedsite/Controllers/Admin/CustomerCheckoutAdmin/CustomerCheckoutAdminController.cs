﻿using Ecommerce_Wedsite.Models;
using Ecommerce_Wedsite.Models.Entities;
using Ecommerce_Wedsite.Models.Helpers.Response;
using Ecommerce_Wedsite.Models.ViewModel;
using Ecommerce_Wedsite.Service.WebApp;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Scripting.Utils;
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
        private readonly IPaymentService _paymentService;

        public CustomerCheckoutAdminController(ILogger<CustomerCheckoutAdminController> logger, IAdminMenuService adminmenuService, ICustomerCheckoutAdminService customercheckoutadminService, IPaymentService paymentService)
        {
            _logger = logger;
            _adminmenuService = adminmenuService;
            _customercheckoutadminService = customercheckoutadminService;
            _paymentService = paymentService;
        }

        [Route("~/customercheckoutadmin")]
        public async Task<IActionResult> CustomerCheckoutAdmin() // Chưa có noti và bar (Ccopy từ controller khác sang) 
        {
            var All = new AllLayout();

            var adminmenu_ViewModels = await _adminmenuService.AdminMenu_ServiceTest();
            var customercheckout_ViewModels = await _customercheckoutadminService.Service_Test();
            var payment_ViewModels = await _paymentService.Service_Test();

            All.adminmenu_ViewModels = adminmenu_ViewModels.Data;
            All.customercheckout_ViewModels = customercheckout_ViewModels.Data; 
            All.paymnet_ViewModels = payment_ViewModels.Data;

            return View("CustomerCheckoutAdmin", All);
        }
    }
}
