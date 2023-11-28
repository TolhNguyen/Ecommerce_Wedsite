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
    public class FooterAdminController : Controller
    {
        private readonly ILogger<FooterAdminController> _logger;
        private readonly IFooterAdminService _footeradminService;

        public FooterAdminController(ILogger<FooterAdminController> logger, IFooterAdminService footeradminService)
        {
            _logger = logger;
            _footeradminService = footeradminService;
        }

        [Route("~/footeradmin")]
        public async Task<IActionResult> FooterAdmin()
        {
            var All = new AllLayout();

            var footer_ViewModels = await _footeradminService.Service_Test();

            All.footer_ViewModels = footer_ViewModels.Data;

            return View("FooterAdmin", All);
        }
    }
}
