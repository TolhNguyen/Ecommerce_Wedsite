using Ecommerce_Wedsite.Models;
using Ecommerce_Wedsite.Models.Entities;
using Ecommerce_Wedsite.Models.Helpers.Response;
using Ecommerce_Wedsite.Models.ViewModel;
using Ecommerce_Wedsite.Service.WebApp;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Text.Json;

namespace Ecommerce_Wedsite.Controllers.Main
{
    public class ProductController : Controller
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IProductService _productService;
        private readonly IOrderByProductService _orderbyproductService;
        private readonly IProductTypeService _producttypeService;
        private readonly ISubProductService _subproductService;
        private readonly IPictureService _pictureService;
        private readonly IHeaderAndFooterService _headerandfooterService;
        private readonly IDiscounttService _discounttService;
        private readonly ISubHeaderService _subheaderService;
        // private readonly ISort

        public ProductController(ILogger<ProductController> logger, IPictureService pictureService, IProductService productService, IOrderByProductService orderbyproductService, IProductTypeService producttypeService, ISubProductService subproductService, IHeaderAndFooterService headerandfooterService, IDiscounttService discounttService, ISubHeaderService subheaderService)
        {
            _logger = logger;
            _productService = productService;
            _orderbyproductService = orderbyproductService;
            _producttypeService = producttypeService;
            _subproductService = subproductService;
            _headerandfooterService = headerandfooterService;
            _pictureService = pictureService;
            _discounttService = discounttService;
            _subheaderService = subheaderService;
        }


        [Route("~/product")]
        public async Task<IActionResult> Product(string? proname, int category, int orderby = 1, int page = 1) //Cứ cho orderby và page = 1 là mặc định trước. Không đc rõng
        {
            var All = new AllLayout(); // biến All lấy model từ All layout truyền vào controller ra view

            var headerandfooter_ViewModels = await _headerandfooterService.HeaderAndFooter_ServiceTest();
            var picture_ViewModels = await _pictureService.Service_Test();
            var producttype_ViewModels = await _producttypeService.Service_Test();
            var orderbyproduct_ViewModels = await _orderbyproductService.Service_Test(proname, category, orderby, page);
            var discountt_ViewModels = await _discounttService.PopupDiscount();
            var subheader_ViewModels = await _subheaderService.SubHeader();

            // Tạo view riêng cho những item sp theo desc (A-Z) / sử dụng chung ở trang product. có phải đúng là truyền order và page = 1 ở lick url cho nút a k. Service truyện thêm 2 tham số
            // _PhanTrangService.Service_Test (page , orderby). 2 tham số này lấy từ đường dẫn bắt sự kiện khi click chuột vào. Service này sẽ query dữ liệu từ db lên.

            All.headerandfooter_ViewModels = headerandfooter_ViewModels.Data;
            All.orderbyproduct_ViewModels = orderbyproduct_ViewModels.Data;
            All.picture_ViewModels = picture_ViewModels.Data;
            All.producttype_ViewModels = producttype_ViewModels.Data;
            All.discountt_ViewModels = discountt_ViewModels.Data;
            All.subheader_ViewModels = subheader_ViewModels.Data;

            //Tạo sort là model lấy page và orderby ra:
            All.sort_ViewModels.page = page;
            All.sort_ViewModels.orderby = orderby; // lấy giá trị orderby từ View / contronller -> lưu vào Model sort -> Service lấy ra từ Model để chạy query tương ứng 
            All.sort_ViewModels.proname = proname;
            All.sort_ViewModels.category = category;

            return View("Product", All);
        }

        [Route("~/productdetail/{producthref}")]
        public async Task<IActionResult> ProductDetail(string producthref) // gom service lại
        {

            var All = new AllLayout();

            var headerandfooter_ViewModels = await _headerandfooterService.HeaderAndFooter_ServiceTest();
            var product_ViewModels = await _productService.Service_Test(producthref);
            var producttype_ViewModels = await _producttypeService.Service_Test();
            var subproduct_ViewModels = await _subproductService.Service_Test();
            var picture_ViewModels = await _pictureService.Service_Test();
            var discountt_ViewModels = await _discounttService.PopupDiscount();
            var subheader_ViewModels = await _subheaderService.SubHeader();

            All.headerandfooter_ViewModels = headerandfooter_ViewModels.Data;
            All.product_ViewModels = product_ViewModels.Data;
            All.producttype_ViewModels = producttype_ViewModels.Data;
            All.subproduct_ViewModels = subproduct_ViewModels.Data;
            All.picture_ViewModels = picture_ViewModels.Data;
            All.discountt_ViewModels = discountt_ViewModels.Data;
            All.subheader_ViewModels = subheader_ViewModels.Data;

            return View("ProductDetail", All);
        }



        //public async Task<IActionResult> ProductSearch(string productkeyword)
        //{

        //}
    }
}
/*
    Tìm kiếm trên thanh Search bar:
        - gõ keyword lên thanh input tag
        - lấy dữ liệu đó truyền sang controller nhận (Bằng 1 function js lấy value đó về - ajax sang controller)
        - lưu dữ liệu vào biến truyền đến service để kiếm thì thông tin từ sql
        - lấy nhửng dữ liệu trùng khớp lên
        - hiện ra ở trang product ds cần tìm
        - Mới:
 */
