using Ecommerce_Wedsite.Models;
using Ecommerce_Wedsite.Models.Entities;
using Ecommerce_Wedsite.Models.Helpers.Response;
using Ecommerce_Wedsite.Models.ViewModel;
using Ecommerce_Wedsite.Service.WebApp;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text.Json;

namespace Ecommerce_Wedsite.Controllers.Main
{
    public class ChatMemoryController : Controller
    {
        private readonly ILogger<ChatMemoryController> _logger;
        private readonly IChatMemoryService _chatmemoryService;

        public ChatMemoryController(ILogger<ChatMemoryController> logger, IChatMemoryService chatmemoryService)
        {
            _logger = logger;
            _chatmemoryService = chatmemoryService;
        }

        public async Task<IActionResult> ChatMemoryFunc(string? botmess, string? usermess) // chạy ra trang checkout trước function phải là 1 controller khác
        {
            await _chatmemoryService.ChatMemoryFunction(botmess, usermess); // xong
            return Json(false);
        }
    }
}
