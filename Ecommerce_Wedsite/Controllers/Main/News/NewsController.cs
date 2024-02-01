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
    public class NewsController : Controller
    {
        private readonly ILogger<NewsController> _logger;
        private readonly IHeaderAndFooterService _headerandfooterService;
        private readonly IDiscounttService _discounttService;
        private readonly IPictureService _pictureService;
        private readonly INewCateService _newscateService;
        private readonly IListNewsService _listnewsService;
        private readonly INewsService _newsService;
        private readonly ISubHeaderService _subheaderService;

        public NewsController(ILogger<NewsController> logger, IHeaderAndFooterService headerandfooterService, IPictureService pictureService, IDiscounttService discounttService, INewCateService newscateService, IListNewsService listnewsService, INewsService newsService, ISubHeaderService subheaderService)
        {
            _logger = logger;
            _headerandfooterService = headerandfooterService;
            _discounttService = discounttService;
            _pictureService = pictureService;
            _newscateService = newscateService;
            _listnewsService = listnewsService;
            _newsService = newsService;
            _subheaderService = subheaderService;
        }

        [Route("~/News")]
        public async Task<IActionResult> News()
        {
            var All = new AllLayout();

            var headerandfooter_ViewModels = await _headerandfooterService.HeaderAndFooter_ServiceTest();
            var picture_ViewModels = await _pictureService.Service_Test();
            var discountt_ViewModels = await _discounttService.PopupDiscount();
            var newscategory_ViewModels = await _newscateService.Service_Test();
            var listnews_ViewModels = await _listnewsService.Service_Test();
            var subheader_ViewModels = await _subheaderService.SubHeader();

            All.headerandfooter_ViewModels = headerandfooter_ViewModels.Data;
            All.picture_ViewModels = picture_ViewModels.Data;
            All.discountt_ViewModels = discountt_ViewModels.Data;
            All.newscategory_ViewModels = newscategory_ViewModels.Data;
            All.listnews_ViewModels = listnews_ViewModels.Data;
            All.subheader_ViewModels = subheader_ViewModels.Data;

            return View("News", All);
        }

        public async Task<IActionResult> NewsDetail(int? ListNews_Id = 0)
        {
            var All = new AllLayout();

            var headerandfooter_ViewModels = await _headerandfooterService.HeaderAndFooter_ServiceTest();
            var picture_ViewModels = await _pictureService.Service_Test();
            var discountt_ViewModels = await _discounttService.PopupDiscount();
            var news_ViewModels = await _newsService.Service_Test(ListNews_Id);

            All.headerandfooter_ViewModels = headerandfooter_ViewModels.Data;
            All.picture_ViewModels = picture_ViewModels.Data;
            All.discountt_ViewModels = discountt_ViewModels.Data;
            All.news_ViewModels = news_ViewModels.Data;

            return View("NewsDetail", All);
        }
    }
}
