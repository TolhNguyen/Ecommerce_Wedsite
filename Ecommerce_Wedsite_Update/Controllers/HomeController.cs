using Ecommerce_Wedsite.Models;
using Ecommerce_Wedsite.Service.WebApp;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Ecommerce_Wedsite.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly INewWebAppService _newWebAppService; //Tạo biến cho Interface muốn sử dụng

        public HomeController(ILogger<HomeController> logger, INewWebAppService newWebAppService) //Khai báo Interface
        {
            _logger = logger;
            _newWebAppService = newWebAppService; // Tạo 1 tham chiếu 
        }

        [Route("~/")]
        public async Task<IActionResult> Index() // Controller cho service tương ứng
        {
            var data = await _newWebAppService.Service_Test(); // chạy dữ liệu service của trong Interface
            return View("WebApp/Index", data); // Trả view đúng địa chỉ
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}