using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using TREKER.Business.Services.Abstractions;
using TREKER.Business.Services.Interfaces;
using TREKER.Business.ViewModels.DayVMs;
using TREKER.Business.ViewModels.PageVM;
using TREKER.Business.ViewModels.TeamMemberVMs;

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
        public async Task<IActionResult> Detail()
        {
            var settings = await (await _settingService.GetAllAsync()).FirstOrDefaultAsync();

            return View(settings);
        }

        [HttpGet]
        public async Task<IActionResult> Update()
        {
            var oldSetting = await (await _settingService.GetAllAsync()).FirstOrDefaultAsync();

            SettingVM vm = new()
            {
                Phone1 = oldSetting.Phone1,
                Phone2 = oldSetting.Phone2,
                Address1 = oldSetting.Address1,
                Address2 = oldSetting.Address2,
                Instagram = oldSetting.Instagram,
                Facebook = oldSetting.Facebook,
                Youtube = oldSetting.Youtube,
                Twitter = oldSetting.Twitter,
                Email = oldSetting.Email,
                Description = oldSetting.Description
            };

            return View(vm);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Moderator")]
        public async Task<IActionResult> Update(SettingVM vm)
        {

            SettingVMValidator validations = new SettingVMValidator();
            var validationResult = await validations.ValidateAsync(vm);

            if (!validationResult.IsValid)
            {
                ModelState.Clear();

                validationResult.Errors.ForEach(x => ModelState.AddModelError(x.PropertyName, x.ErrorMessage));

                return View(vm);
            }

            await _settingService.UpdateAsync(vm);

            return RedirectToAction(nameof(Detail));
        }

    }
}
