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
    public class AdminController : Controller
    {
        private readonly ILogger<AdminController> _logger;
        private readonly IAdminService _adminService;
        private readonly IHeaderAndFooterService _headerandfooterService;
        private readonly IAdminMenuService _adminmenuService;


        public AdminController(ILogger<AdminController> logger, IAdminService adminService, IHeaderAndFooterService headerandfooterService, IAdminMenuService adminmenuService)
        {
            _logger = logger;
            _adminService = adminService;
            _headerandfooterService = headerandfooterService;
            _adminmenuService = adminmenuService;
        }

        [Route("~/adminloginpage")]
        public IActionResult AdminLoginPage() // trang admin login
        {
            //var All = new AllLayout();
            //var headerandfooter_ViewModels = await _headerandfooterService.HeaderAndFooter_ServiceTest();
            //All.headerandfooter_ViewModels = headerandfooter_ViewModels.Data;
            return View("AdminLoginPage");
        }

        [Route("~/adminpage")]
        public async Task<IActionResult> AdminPage() // Trang admin 
        {
            var All = new AllLayout();
            var adminmenu_ViewModels = await _adminmenuService.AdminMenu_ServiceTest();
            All.adminmenu_ViewModels = adminmenu_ViewModels.Data;
            return View("AdminPage", All);
        }


    //C1: Dùng ajax để login với js:
    //public async Task<ResponseMessageObject<Admin_ViewModel>> AdminLoginPageFunction(string? userName, string? passWord) // function login ajax. Nên để dưới 
    //{
    //    var All = new AllLayout();

    //    var admin_ViewModels = await _adminService.Service_Test(userName, passWord);

    //    All.admin_ViewModels = admin_ViewModels.Data;

    //    return admin_ViewModels;
    //}


    //C2:
    [Route("~/adminloginpagefunction")]
        public async Task<IActionResult> AdminLoginPageFunction(string? userName, string? passWord) // function login ajax. Nên để dưới 
        {

            var admin_ViewModels = await _adminService.Service_Test(userName, passWord); // lấy kết quả kiểm tra ra

            if (admin_ViewModels.success == true) // nếu kq đúng:
            {
                return RedirectToAction("AdminPage");
            }
            else return RedirectToAction("AdminLoginPage"); // kq sai
        }
    }
}
