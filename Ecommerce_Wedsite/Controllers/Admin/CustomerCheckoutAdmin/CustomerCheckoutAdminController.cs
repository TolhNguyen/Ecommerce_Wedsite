using Ecommerce_Wedsite.Models;
using Ecommerce_Wedsite.Models.Entities;
using Ecommerce_Wedsite.Models.Helpers.Response;
using Ecommerce_Wedsite.Models.ViewModel;
using Ecommerce_Wedsite.Service.WebApp;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Scripting.Utils;
using System.Diagnostics;
using System.Reflection.Metadata.Ecma335;
using System.Text.Json;


namespace Ecommerce_Wedsite.Controllers
{
    public class CustomerCheckoutAdminController : Controller
    {
        private readonly ILogger<CustomerCheckoutAdminController> _logger;
        private readonly IAdminMenuService _adminmenuService;
        private readonly ICustomerCheckoutAdminService _customercheckoutadminService;
        private readonly IPaymentService _paymentService;
        private readonly IAdminService _adminService;

        public CustomerCheckoutAdminController(ILogger<CustomerCheckoutAdminController> logger, IAdminMenuService adminmenuService, ICustomerCheckoutAdminService customercheckoutadminService, IPaymentService paymentService, IAdminService adminService)
        {
            _logger = logger;
            _adminmenuService = adminmenuService;
            _customercheckoutadminService = customercheckoutadminService;
            _paymentService = paymentService;
            _adminService = adminService;
        }

        [Route("~/customercheckoutadmin")]
        public async Task<IActionResult> CustomerCheckoutAdmin() // nên tách ra làm 3 trang unpro -> pro -> deli
        {
            var All = new AllLayout();

            int id = 0;
            string? idstr = HttpContext.Request.Cookies["adminid"];
            var check = int.TryParse(idstr, out id);
            var admin_ViewModels = await _adminService.AdminInfo(id);

            var adminmenu_ViewModels = await _adminmenuService.AdminMenu_ServiceTest();
            var customercheckout_ViewModels = await _customercheckoutadminService.UnprocessCheckout();
            var payment_ViewModels = await _paymentService.Service_Test();

            var noticookie = HttpContext.Request.Cookies["noticookie"];
            if (noticookie != null) // nếu có tb rồi, chỉ hiện thôi
            {
                var notiVM = new NoticeAdmin_ViewModel();
                notiVM = JsonSerializer.Deserialize<NoticeAdmin_ViewModel>(noticookie);
                All.noticeadmin_ViewModels = notiVM;
            }

            All.admin_ViewModels = admin_ViewModels.Data;
            All.adminmenu_ViewModels = adminmenu_ViewModels.Data;
            All.customercheckout_ViewModels = customercheckout_ViewModels.Data; 
            All.paymnet_ViewModels = payment_ViewModels.Data;

            return View("CustomerCheckoutAdmin", All);
        }
        [Route("~/customercheckoutprocessadmin")]
        public async Task<IActionResult> CustomerCheckoutProcessAdmin() // nên tách ra làm 3 trang unpro -> pro -> deli
        {
            var All = new AllLayout();

            int id = 0;
            string? idstr = HttpContext.Request.Cookies["adminid"];
            var check = int.TryParse(idstr, out id);
            var admin_ViewModels = await _adminService.AdminInfo(id);

            var adminmenu_ViewModels = await _adminmenuService.AdminMenu_ServiceTest();
            var customercheckout_ViewModels = await _customercheckoutadminService.ProcessCheckout();
            var payment_ViewModels = await _paymentService.Service_Test();

            var noticookie = HttpContext.Request.Cookies["noticookie"];
            if (noticookie != null) // nếu có tb rồi, chỉ hiện thôi
            {
                var notiVM = new NoticeAdmin_ViewModel();
                notiVM = JsonSerializer.Deserialize<NoticeAdmin_ViewModel>(noticookie);
                All.noticeadmin_ViewModels = notiVM;
            }

            All.admin_ViewModels = admin_ViewModels.Data;
            All.adminmenu_ViewModels = adminmenu_ViewModels.Data;
            All.customercheckout_ViewModels = customercheckout_ViewModels.Data;
            All.paymnet_ViewModels = payment_ViewModels.Data;

            return View("CustomerCheckoutProcessAdmin", All);
        }
        [Route("~/customercheckoutdeliveradmin")]
        public async Task<IActionResult> CustomerCheckoutDeliverAdmin() // nên tách ra làm 3 trang unpro -> pro -> deli
        {
            var All = new AllLayout();

            int id = 0;
            string? idstr = HttpContext.Request.Cookies["adminid"];
            var check = int.TryParse(idstr, out id);
            var admin_ViewModels = await _adminService.AdminInfo(id);

            var adminmenu_ViewModels = await _adminmenuService.AdminMenu_ServiceTest();
            var customercheckout_ViewModels = await _customercheckoutadminService.Deliver();
            var payment_ViewModels = await _paymentService.Service_Test();

            var noticookie = HttpContext.Request.Cookies["noticookie"];
            if (noticookie != null) // nếu có tb rồi, chỉ hiện thôi
            {
                var notiVM = new NoticeAdmin_ViewModel();
                notiVM = JsonSerializer.Deserialize<NoticeAdmin_ViewModel>(noticookie);
                All.noticeadmin_ViewModels = notiVM;
            }

            All.admin_ViewModels = admin_ViewModels.Data;
            All.adminmenu_ViewModels = adminmenu_ViewModels.Data;
            All.customercheckout_ViewModels = customercheckout_ViewModels.Data;
            All.paymnet_ViewModels = payment_ViewModels.Data;

            return View("CustomerCheckoutDeliverAdmin", All);
        }

        [Route("~/processfunction")]
        public async Task<IActionResult> ProcessFunction(int id, int status) // ép kiểu status từ view trc khi sang controller .unpro -> pro -> deli
        {
            var stt = await _customercheckoutadminService.ProcessFunction(id, status); // trả về status int
            if (stt == 1) // là process
            {
                return RedirectToAction("CustomerCheckoutProcessAdmin");
            }
            if (stt == 2) // là deliver
            {
                return RedirectToAction("CustomerCheckoutDeliverAdmin");
            }
            return RedirectToAction("CustomerCheckoutFinishAdmin"); // là finish
        }

        [Route("~/processreversedfunction")]
        public async Task<IActionResult> ProcessReversedFunction(int id, int status) // nên tách ra làm 3 trang unpro -> pro -> deli
        {
            var stt = await _customercheckoutadminService.ProcessReversedFunction(id, status); // trả về status int
            if(stt == 1) // là process
            {
                return RedirectToAction("CustomerCheckoutProcessAdmin");
            }
            if (stt == 2) // là deliver
            {
                return RedirectToAction("CustomerCheckoutDeliverAdmin");
            }
            return RedirectToAction("CustomerCheckoutFinishAdmin"); // là finish
        }

        [Route("~/customercheckoutfinishadmin")]
        public async Task<IActionResult> CustomerCheckoutFinishAdmin() // nên tách ra làm 3 trang unpro -> pro -> deli
        {
            var All = new AllLayout();

            int id = 0;
            string? idstr = HttpContext.Request.Cookies["adminid"];
            var check = int.TryParse(idstr, out id);
            var admin_ViewModels = await _adminService.AdminInfo(id);

            var adminmenu_ViewModels = await _adminmenuService.AdminMenu_ServiceTest();
            var customercheckout_ViewModels = await _customercheckoutadminService.Finish();
            var payment_ViewModels = await _paymentService.Service_Test();

            var noticookie = HttpContext.Request.Cookies["noticookie"];
            if (noticookie != null) // nếu có tb rồi, chỉ hiện thôi
            {
                var notiVM = new NoticeAdmin_ViewModel();
                notiVM = JsonSerializer.Deserialize<NoticeAdmin_ViewModel>(noticookie);
                All.noticeadmin_ViewModels = notiVM;
            }

            All.admin_ViewModels = admin_ViewModels.Data;
            All.adminmenu_ViewModels = adminmenu_ViewModels.Data;
            All.customercheckout_ViewModels = customercheckout_ViewModels.Data;
            All.paymnet_ViewModels = payment_ViewModels.Data;

            return View("CustomerCheckoutFinishAdmin", All); // lỗi 
        }
    }
}
