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
    public class DiscountController : Controller
    {
        private readonly ILogger<DiscountController> _logger;
        private readonly IDiscountNumberFunctionService _discountnumberfunctionService;

        public DiscountController(ILogger<DiscountController> logger, IDiscountNumberFunctionService discountnumberfunctionService)
        {
            _logger = logger;
            _discountnumberfunctionService = discountnumberfunctionService;
        }

        [HttpGet]
        public async Task<IActionResult> DiscountNumberFunction(string? discountranid, string? gmail) // truyền tên biến đúng với tên biến sau ? url của ajax
        {
            var discount = await _discountnumberfunctionService.DiscountNumberFunction(discountranid);
            if(discount.success == true) // nếu kết quả thành công
            {
                return Json(true);
            }
            return Json(false);
        }
    }
}


       
