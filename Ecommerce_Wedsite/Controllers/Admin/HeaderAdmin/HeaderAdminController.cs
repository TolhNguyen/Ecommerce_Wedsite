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
    public class HeaderAdminController : Controller
    {
        private readonly ILogger<HeaderAdminController> _logger;
        private readonly IHeaderAdminDeleteService _headeradminDeleteService;
        private readonly IHeaderAdminCreateService _headeradminCreateService;
        private readonly IHeaderAdminEditService _headeradminEditService;
        private readonly IHeaderAdminService _headeradminService;
        private readonly IHeaderAdminEditFunctionService _headeradminEditFunctionService;

        public HeaderAdminController(ILogger<HeaderAdminController> logger, IHeaderAdminService headeradminService, IHeaderAdminEditService headeradminEditService, IHeaderAdminDeleteService headeradminDeleteService, IHeaderAdminCreateService headeradminCreateService, IHeaderAdminEditFunctionService headeradminEditFunctionService)
        {
            _logger = logger;
            _headeradminService = headeradminService;
            _headeradminDeleteService = headeradminDeleteService;
            _headeradminCreateService = headeradminCreateService;
            _headeradminEditService = headeradminEditService;
            _headeradminEditFunctionService = headeradminEditFunctionService;
        }

        [Route("~/headeradmin")]
        public async Task<IActionResult> HeaderAdmin(Header headeritem) // Delete, Edit nên chung 1 controller hay tách ra
        {
            var All = new AllLayout();

            var header_ViewModels = await _headeradminService.Service_Test();

            All.header_ViewModels = header_ViewModels.Data;

            return View("HeaderAdmin", All);
        }

        [Route("~/headeradmindelete")]
        public async Task<IActionResult> HeaderAdminDelete(Header headeritem) // Delete, Edit nên chung 1 controller hay tách ra
        {
            var All = new AllLayout();

            var header_ViewModels = await _headeradminDeleteService.Service_Test(headeritem);

            All.header_ViewModels = header_ViewModels.Data;

            return View("SuccessPage", All); // sau khi chạy service ở trên r mới trả view ra
        }

        [Route("~/headeradmincreate")]
        public async Task<IActionResult> HeaderAdminCreate() // không cần service chỉ cần trả trang thôi
        {
            var All = new AllLayout();

            var header_ViewModels = await _headeradminService.Service_Test();

            All.header_ViewModels = header_ViewModels.Data;

            return View("HeaderAdminCreate", All);
        }

        [Route("~/headeradmincreatefunction")]
        public async Task<IActionResult> HeaderAdminCreateFunction(Header headeritem) 
        {
            var All = new AllLayout();

            var header_ViewModels = await _headeradminCreateService.Service_Test(headeritem);

            All.header_ViewModels = header_ViewModels.Data;

            return View("SuccessPage", All); // sau khi chạy service ở trên r mới trả view ra
        }

        [Route("~/headeradminedit")]
        public async Task<IActionResult> HeaderAdminEdit(int Header_Id) // Tạo thêm trang riêng function riêng
        {
            var All = new AllLayout();

            var header_ViewModels = await _headeradminEditService.Service_Test(Header_Id);

            All.header_ViewModels = header_ViewModels.Data;

            return View("HeaderAdminEdit", All);
        }

        public async Task<IActionResult> HeaderAdminEditFunction(Header headeritem) // controller kiểu ajax thì mới đọc đc 
            // Tạo thêm trang riêng function riêng
        {
            // cần có 1  model layout admin riêng
            var header_ViewModels = await _headeradminEditFunctionService.Service_Test(headeritem);
            return Json(header_ViewModels); // sau khi chạy service ở trên r mới trả view ra
        }
    }
}
/*
    - Tìm cách gom những action crud này vào 1 controller nhỏ và gọi ra lại trong controller này 
    - Tìm cách gom các function lại với nhau thành ít controller (nhiều service)
    => Giải: do action form controller và ajax chạy cùng nhau (nên ajax k chạy dc, dùng 1 trong 2).Dùng 1 trong 3 cách chạy:
            + 1. dùng form action dẫn tới controller thôi
            + 2. dùng ajax nhận giá trị dẫn tới controller
            + 3. dùng submit form function
 */

