using Ecommerce_Wedsite.Service.WebApp;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce_Wedsite.Controllers.WebApp
{
    public class NewWebAppController : Controller
    {
        private readonly INewWebAppService _newWebApp;
        public NewWebAppController(INewWebAppService newWebApp)
        {
            _newWebApp = newWebApp;
        }
        [Route("~/New")]
        public async Task<IActionResult> ReadSetting(int id)
        {
            var it = await _newWebApp.Service_Test();
            return View("WebApp/New", it);
        }
    }
}
