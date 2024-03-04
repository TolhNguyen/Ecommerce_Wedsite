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
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserLoginService _userloginService;
        public UserController(ILogger<UserController> logger, IUserLoginService userloginService)
        {
            _logger = logger;
            _userloginService = userloginService;
        }

        [Route("~/userloginpage")]
        public IActionResult UserLoginPage() // trang admin login
        {
            return View("UserLoginPage");
        }

        [Route("~/userloginpagefunction")]
        public async Task<IActionResult> UserLoginPageFunction(string? userName, string? passWord) // function login ajax. Nên để dưới 
        {

            var admin_ViewModels = await _userloginService.Login(userName, passWord); // lấy kết quả kiểm tra ra

            if (admin_ViewModels.success == true) // nếu kq đúng: tạo cookie chứa username hiện lên tên người dùng đó
            {
                return RedirectToAction("Index", "Home");
            }
            else return RedirectToAction("UserLoginPage"); // kq sai
        }
    }
}

