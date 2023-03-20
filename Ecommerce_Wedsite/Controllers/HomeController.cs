using Ecommerce_Wedsite.Models;
using Ecommerce_Wedsite.Service.WebApp;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Ecommerce_Wedsite.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly INewWebAppService _newWebAppService;

        public HomeController(ILogger<HomeController> logger, INewWebAppService newWebAppService)
        {
            _logger = logger;
            _newWebAppService = newWebAppService;
        }

        [Route("~/")]
        public async Task<IActionResult> Index()
        {
            var data = await _newWebAppService.Service_Test();
            return View("WebApp/Index", data);
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