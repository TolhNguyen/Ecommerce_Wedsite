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
    public class PythonPageController : Controller
    {
        private readonly ILogger<PythonPageController> _logger;

        public PythonPageController(ILogger<PythonPageController> logger)
        {
            _logger = logger;
        }

        [Route("~/pythonpage")]
        public IActionResult PythonPage()
        {

            return View("PythonPage");
        }
    }
}