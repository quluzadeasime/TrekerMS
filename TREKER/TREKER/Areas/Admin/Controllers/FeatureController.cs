using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TREKER.Business.Services.Abstractions;
using TREKER.Business.Services.Interfaces;
using TREKER.Business.ViewModels.FeatureVMs;

namespace TREKER.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class FeatureController : Controller
    {
        private readonly IFeatureService _featureService;

        public FeatureController(IFeatureService featureService)
        {
            _featureService = featureService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Moderator")]
        public async Task<IActionResult> Table()
        {
            if (User.IsInRole("Admin"))
            {
                return View(await _featureService.GetAllAsync());
            }
            else
            {
                return View((await _featureService.GetAllAsync()).Where(x => !x.IsDeleted));
            }
        }


        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Moderator")]
        public async Task<IActionResult> Create(CreateFeatureVM vm)
        {
            CreateFeatureVMValidator validations = new CreateFeatureVMValidator();
            var validationResult = await validations.ValidateAsync(vm);

            if (!validationResult.IsValid)
            {
                ModelState.Clear();

                validationResult.Errors.ForEach(x => ModelState.AddModelError(x.PropertyName, x.ErrorMessage));

                return View(vm);
            }

            await _featureService.CreateAsync(vm);

            return RedirectToAction(nameof(Table));
        }

        [HttpGet]
        [Authorize(Roles ="Admin, Moderator")]
        public async Task<IActionResult> Update(int id)
        {
            var oldFeature = await _featureService.GetByIdAsync(id);

            UpdateFeatureVM vm = new()
            {
                Name = oldFeature.Name
            };
            return View(vm);
        }

        [HttpPost]
        [Authorize(Roles ="Admin,Moderator")]
        public async Task<IActionResult> Update(UpdateFeatureVM vm)
        {
            UpdateFeatureVMValidator validations = new UpdateFeatureVMValidator();
            var validationResult = await validations.ValidateAsync(vm);

            if (!validationResult.IsValid)
            {
                ModelState.Clear();

                validationResult.Errors.ForEach(x => ModelState.AddModelError(x.PropertyName, x.ErrorMessage));

                return View(vm);
            }

            await _featureService.UpdateAsync(vm);

            return RedirectToAction(nameof(Table));
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Moderator")]
        public async Task<IActionResult> Detail(int id)
        {
            var feature = await _featureService.GetByIdAsync(id);

            return View(feature);
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Moderator")]
        public async Task<IActionResult> Delete(int id)
        {
            await _featureService.DeleteAsync(id);

            return RedirectToAction(nameof(Table));
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Remove(int id)
        {
            await _featureService.RemoveAsync(id);

            return RedirectToAction(nameof(Table));
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Recover(int id)
        {
            await _featureService.RecoverAsync(id);

            return RedirectToAction(nameof(Table));
        }
    }
}
