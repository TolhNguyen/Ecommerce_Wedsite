using Ecommerce_Wedsite.Controllers.Main;
using Ecommerce_Wedsite.Models;
using Ecommerce_Wedsite.Models.Entities;
using Ecommerce_Wedsite.Models.Helpers.Response;
using Ecommerce_Wedsite.Models.ViewModel;
using Ecommerce_Wedsite.Service.WebApp;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text.Json;

namespace Ecommerce_Wedsite.Controllers
{
    public class AboutUsController : Controller
    {
        private readonly ILogger<AboutUsController> _logger;
        private readonly IHeaderAndFooterService _headerandfooterService;
        private readonly IDiscounttService _discounttService;
        private readonly IPictureService _pictureService;

        public AboutUsController(ILogger<AboutUsController> logger, IHeaderAndFooterService headerandfooterService, IPictureService pictureService, IDiscounttService discounttService)
        {
            _logger = logger;
            _headerandfooterService = headerandfooterService;
            _discounttService = discounttService;
            _pictureService = pictureService;
        }

        [Route("~/AboutUs")]
        public async Task<IActionResult> AboutUs() // chọn template boostrap khác
        {
            var All = new AllLayout();

            var headerandfooter_ViewModels = await _headerandfooterService.HeaderAndFooter_ServiceTest();
            var picture_ViewModels = await _pictureService.Service_Test();
            var discountt_ViewModels = await _discounttService.PopupDiscount();

            All.headerandfooter_ViewModels = headerandfooter_ViewModels.Data;
            All.picture_ViewModels = picture_ViewModels.Data;
            All.discountt_ViewModels = discountt_ViewModels.Data;

            return View("AboutUs", All);
        }
    }
}