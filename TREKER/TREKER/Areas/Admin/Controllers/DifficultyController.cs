using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TREKER.Business.Services.Abstractions;
using TREKER.Business.Services.Interfaces;
using TREKER.Business.ViewModels.DifficultyVMs;
using TREKER.Business.ViewModels.TeamMemberVMs;
using TREKER.Core.Enums;

namespace TREKER.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DifficultyController : Controller
    {
        private readonly IDifficultyService _difficultyService;

        public DifficultyController(IDifficultyService difficultyService)
        {
            _difficultyService = difficultyService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Moderator")]

        public async Task<IActionResult> Table()
        {
            if (User.IsInRole("Admin"))
            {
                return View(await _difficultyService.GetAllAsync());
            }
            else
            {
                return View((await _difficultyService.GetAllAsync()).Where(x => !x.IsDeleted));
            }
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles ="Admin,Moderator")]
        public async Task<IActionResult> Create(CreateDifficultyVM vm)
        {
            CreateDifficultyVMValidator validations = new CreateDifficultyVMValidator();
            var validationResult = await validations.ValidateAsync(vm);

            if (!validationResult.IsValid)
            {
                ModelState.Clear();

                validationResult.Errors.ForEach(x => ModelState.AddModelError(x.PropertyName, x.ErrorMessage));

                return View(vm);
            }

            await _difficultyService.CreateAsync(vm);

            return RedirectToAction(nameof(Table));
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var oldDifficulty = await _difficultyService.GetByIdAsync(id);

            UpdateDifficultyVM vm = new()
            {
                Name = oldDifficulty.Name
            };

            return View(vm);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Moderator")]
        public async Task<IActionResult> Update(UpdateDifficultyVM vm)
        {
            UpdateDifficultyVMValidator validations = new UpdateDifficultyVMValidator();
            var validationResult = await validations.ValidateAsync(vm);

            if (!validationResult.IsValid)
            {
                ModelState.Clear();

                validationResult.Errors.ForEach(x => ModelState.AddModelError(x.PropertyName, x.ErrorMessage));

                return View(vm);
            }

            await _difficultyService.UpdateAsync(vm);

            return RedirectToAction(nameof(Table));
        }


        [HttpGet]
        [Authorize(Roles = "Admin,Moderator")]
        public async Task<IActionResult> Detail(int id)
        {
            var difficulty = await _difficultyService.GetByIdAsync(id);

            return View(difficulty);
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Moderator")]
        public async Task<IActionResult> Delete(int id)
        {
            await _difficultyService.DeleteAsync(id);

            return RedirectToAction(nameof(Table));
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Remove(int id)
        {
            await _difficultyService.RemoveAsync(id);

            return RedirectToAction(nameof(Table));
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Recover(int id)
        {
            await _difficultyService.RecoverAsync(id);

            return RedirectToAction(nameof(Table));
        }
    }
}
