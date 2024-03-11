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
        private readonly IUserRegisterService _userregisterService;
        private readonly IUserWebNameService _userwebnameService;
        private readonly IUserEmailService _useremailService;
        public UserController(ILogger<UserController> logger, IUserLoginService userloginService, IUserRegisterService userregisterService, IUserWebNameService userwebnameService, IUserEmailService useremailService)
        {
            _logger = logger;
            _userloginService = userloginService;
            _userregisterService = userregisterService;
            _userwebnameService = userwebnameService;
            _useremailService = useremailService;
        }

        [Route("~/userloginpage")]
        public IActionResult UserLoginPage() // trang admin login
        {
            return View("UserLoginPage");
        }

        [Route("~/userloginpagefunction")]
        public async Task<IActionResult> UserLoginPageFunction(string? userName, string? passWord) // function login ajax. Nên để dưới 
        {

            var checklogin = await _userloginService.Login(userName, passWord); // lấy kết quả kiểm tra ra

            if (checklogin.success == true) // nếu kq đúng: tạo cookie chứa username hiện lên tên người dùng đó
            {
                var webname = "";
                string webnamecookie = await _userwebnameService.WebName(userName, webname);
                CookieOptions option = new CookieOptions();
                option.Secure = true;
                option.HttpOnly = false; // này là khóa cookie thành private k cho xài
                option.SameSite = SameSiteMode.None;
                option.Path = "/";
                option.Expires = DateTime.Now.AddDays(1);
                HttpContext.Response.Cookies.Append("webname", webnamecookie, option);

                return RedirectToAction("Index", "Home");
            }
            else return RedirectToAction("UserLoginPage"); // kq sai
        }

        [Route("~/userregisterpage")]   
        public IActionResult UserRegisterPage() // trang admin login
        {
            return View("UserRegisterPage");
        }

        [Route("~/userregisterpagefunction")]
        public async Task<IActionResult> UserRegisterPageFunction(UserLogin userlogin) // function login ajax. Nên để dưới 
        {
            //var checkemail = await _useremailService.Email(userlogin.UserLogin_Email);
            //if (checkemail.success == true)
            //{ }

            var register = await _userregisterService.Register(userlogin); // lấy kết quả kiểm tra ra

            if (register.success == true) // nếu kq đúng: tạo cookie chứa username hiện lên tên người dùng đó
            {
                return Json(true);
            }
            return Json(false); // kq sai
        }
    }

    /*
        Cần thêm 1 discount/promotion cho member mới đăng nhập.
     */
}

