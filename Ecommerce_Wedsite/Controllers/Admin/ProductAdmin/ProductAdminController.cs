using Ecommerce_Wedsite.Models;
using Ecommerce_Wedsite.Models.Entities;
using Ecommerce_Wedsite.Models.Helpers.Response;
using Ecommerce_Wedsite.Models.ViewModel;
using Ecommerce_Wedsite.Service.WebApp;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text.Json;
using static IronPython.Modules._ast;


namespace Ecommerce_Wedsite.Controllers
{
    public class ProductAdminController : Controller
    {
        private readonly ILogger<ProductAdminController> _logger;
        private readonly IProductAdminService _productadminService;
        private readonly IProductTypeAdminService _producttypeadminService;
        private readonly IProductAdminDeleteService _productadmindeleteService;
        private readonly IProductAdminCreateService _productadmincreateService;
        private readonly IProductAdminEditFunctionService _productadmineditfunctionService;
        private readonly IProductAdminEditService _productadmineditService;
        private readonly IAdminMenuService _adminmenuService;
        private readonly IPictureService _pictureService;
        private readonly IProductAdminConditionService _productadminconditionService;
        private readonly IAdminService _adminService;
        private readonly INoticeAdminService _noticeadminService;

        public ProductAdminController(ILogger<ProductAdminController> logger, IProductAdminService productadminService, IProductTypeAdminService producttypeadminService, IProductAdminDeleteService productadmindeleteService, IProductAdminCreateService productadmincreateService, IProductAdminEditFunctionService productadmineditfunctionService, IProductAdminEditService productadmineditService, IAdminMenuService adminmenuService, IPictureService pictureService, IProductAdminConditionService productadminconditionService, IAdminService adminService, INoticeAdminService noticeadminService)
        {
            _logger = logger;
            _productadminService = productadminService;
            _producttypeadminService = producttypeadminService;
            _productadmindeleteService = productadmindeleteService;
            _productadmincreateService = productadmincreateService;
            _productadmineditfunctionService = productadmineditfunctionService;
            _productadmineditService = productadmineditService;
            _adminmenuService = adminmenuService;
            _pictureService = pictureService;
            _productadminconditionService = productadminconditionService;
            _adminService = adminService;
            _noticeadminService = noticeadminService;
        }

        [Route("~/productadmin")]
        public async Task<IActionResult> ProductAdmin()
        {
            var All = new AllLayout();

            int id = 0;
            string? idstr = HttpContext.Request.Cookies["adminid"];
            var check = int.TryParse(idstr, out id);
            var admin_ViewModels = await _adminService.AdminInfo(id);
            var noticookie = HttpContext.Request.Cookies["noticookie"];
            if (noticookie != null) // nếu có tb rồi, chỉ hiện thôi
            {
                var notiVM = new NoticeAdmin_ViewModel();
                notiVM = JsonSerializer.Deserialize<NoticeAdmin_ViewModel>(noticookie);
                All.noticeadmin_ViewModels = notiVM;
            }

            var picture_ViewModels = await _pictureService.Service_Test();
            var product_ViewModels = await _productadminService.Service_Test();
            var adminmenu_ViewModels = await _adminmenuService.AdminMenu_ServiceTest();

            All.admin_ViewModels = admin_ViewModels.Data;
            All.adminmenu_ViewModels = adminmenu_ViewModels.Data;
            All.product_ViewModels = product_ViewModels.Data;
            All.picture_ViewModels = picture_ViewModels.Data;

            return View("ProductAdmin", All);
        }

        [Route("~/productadmindeletefunction")]
        public async Task<IActionResult> ProductAdminDeleteFunction(int Product_Id) // nên đổi sang ajax
        {
            var All = new AllLayout();

            var product_ViewModels = await _productadmindeleteService.Service_Test(Product_Id);
            
            All.product_ViewModels = product_ViewModels.Data;

            return RedirectToAction("ProductAdmin"); // sau khi chạy service ở trên r mới trả view ra
        }

        [Route("~/productadmincreate")]
        public async Task<IActionResult> ProductAdminCreate()
        {
            var All = new AllLayout();
            int id = 0;
            string? idstr = HttpContext.Request.Cookies["adminid"];
            var check = int.TryParse(idstr, out id);
            var admin_ViewModels = await _adminService.AdminInfo(id);
            var noticookie = HttpContext.Request.Cookies["noticookie"];
            if (noticookie != null) // nếu có tb rồi, chỉ hiện thôi
            {
                var notiVM = new NoticeAdmin_ViewModel();
                notiVM = JsonSerializer.Deserialize<NoticeAdmin_ViewModel>(noticookie);
                All.noticeadmin_ViewModels = notiVM;
            }

            var producttype_ViewModels = await _producttypeadminService.Service_Test();
            var adminmenu_ViewModels = await _adminmenuService.AdminMenu_ServiceTest();
            All.producttype_ViewModels = producttype_ViewModels.Data;
            All.admin_ViewModels = admin_ViewModels.Data;
            All.adminmenu_ViewModels = adminmenu_ViewModels.Data;
            return View("ProductAdminCreate", All);
        }

        public async Task<IActionResult> ProductAdminCreateFunction(Product productitem) 
        {
            var product_ViewModels = await _productadmincreateService.Service_Test(productitem);
            return Json(product_ViewModels); 
        }

        [Route("~/productadminedit")]
        public async Task<IActionResult> ProductAdminEdit(int Product_Id) // từ item.Product_Id value trong thẻ tag. async để await kết quả về r mới return view
        {
            var All = new AllLayout();

            var product_ViewModels = await _productadmineditService.Service_Test(Product_Id);
			var adminmenu_ViewModels = await _adminmenuService.AdminMenu_ServiceTest();
			All.adminmenu_ViewModels = adminmenu_ViewModels.Data;
			All.product_ViewModels = product_ViewModels.Data;

            return View("ProductAdminEdit", All);
        }

        [HttpPost]
        public async Task<IActionResult> ProductAdminEditFunction(Product productitem) // nên đổi sang ajax
        {
            var product_ViewModels = await _productadmineditfunctionService.Service_Test(productitem);
            if(product_ViewModels.success == true) {
                return Json(true);
            }
            return Json(false);
        }

        public async Task<IActionResult> ProductAdminCondition(int id, int condition)
        {

            await _productadminconditionService.CheckCondition(id, condition);

            return RedirectToAction("ProductAdmin");
        }

        [Route("~/producttypeadmin")]
        public async Task<IActionResult> ProductTypeAdmin()
        {
            var All = new AllLayout();

            var producttype_ViewModels = await _producttypeadminService.Service_Test();
            var adminmenu_ViewModels = await _adminmenuService.AdminMenu_ServiceTest();
            All.adminmenu_ViewModels = adminmenu_ViewModels.Data;
            All.producttype_ViewModels = producttype_ViewModels.Data;

            return View("ProductTypeAdmin", All);  
        }

        

        //[Route("/producttypecrud")]
        //public async Task<IActionResult> ProductTypeAdminCRUD()
        //{
        //    var All = new AllLayout();

        //    var producttype_ViewModels = await _producttypeadminService.Service_Test();

        //    All.producttype_ViewModels = producttype_ViewModels.Data;

        //    return View("ProductTypeAdmin", All);
        //}
    }
}
