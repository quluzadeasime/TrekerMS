using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TREKER.Business.Services.Interfaces;

namespace TREKER.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SettingController : Controller
    {
        private readonly ISettingService _settingService;

        public SettingController(ISettingService settingService)
        {
            _settingService = settingService;
        }

        [HttpGet]
        [Authorize(Roles = "Moderator,Admin")]
        public IActionResult Detail()
        {
            var settings = _settingService.GetAllAsync();
            return View(settings);
        }

        [HttpGet]
        [Authorize(Roles = "Moderator,Admin")]
        public IActionResult Update()
        {
            return View();
        }
    }
}
