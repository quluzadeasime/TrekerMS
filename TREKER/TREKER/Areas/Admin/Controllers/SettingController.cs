using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TREKER.Business.Services.Interfaces;

namespace TREKER.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class SettingController : Controller
    {
        private readonly ISettingService _settingService;

        public SettingController(ISettingService settingService)
        {
            _settingService = settingService;
        }

        [HttpGet]
        [Authorize(Roles = "Moderator,Admin")]
        public async Task<IActionResult> Detail(int id)
        {
            var settings =await _settingService.GetByIdAsync(id);
            return View(settings);
        }

        [HttpGet]
        [Authorize(Roles = "Moderator,Admin")]
        public async Task<IActionResult> Update()
        {
            return View();
        }
    }
}
