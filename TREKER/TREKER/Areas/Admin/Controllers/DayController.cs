using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using TREKER.Business.Services.Abstractions;
using TREKER.Business.Services.Interfaces;
using TREKER.Business.ViewModels.DayVMs;
using TREKER.Business.ViewModels.DifficultyVMs;

namespace TREKER.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class DayController : Controller
    {
        private readonly IDayService _dayService;
        private readonly ITrekkingService _trekkingService;

        public DayController(IDayService dayService, ITrekkingService trekkingService)
        {
            _dayService = dayService;
            _trekkingService = trekkingService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Moderator")]
        public async Task<IActionResult> Table()
        {
            if (User.IsInRole("Admin"))
            {
                return View(await _dayService.GetAllAsync());
            }
            else
            {
                return View((await _dayService.GetAllAsync()).Where(x => !x.IsDeleted));
            }
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var vm = new CreateDayVM()
            {
                Trekkings = await (await _trekkingService.GetAllAsync()).ToListAsync()
            };

            return View(vm);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Moderator")]
        public async Task<IActionResult> Create(CreateDayVM vm)
        {
            vm.Trekkings = await (await _trekkingService.GetAllAsync()).ToListAsync();

            CreateDayVMValidator validations = new CreateDayVMValidator();
            var validationResult = await validations.ValidateAsync(vm);

            if (!validationResult.IsValid)
            {
                ModelState.Clear();

                validationResult.Errors.ForEach(x => ModelState.AddModelError(x.PropertyName, x.ErrorMessage));

                return View(vm);
            }

            await _dayService.CreateAsync(vm);

            return RedirectToAction(nameof(Table));
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var oldDay = await _dayService.GetByIdAsync(id);
            var trekkings = await (await _trekkingService.GetAllAsync()).ToListAsync();

            UpdateDayVM vm = new()
            {
                Description = oldDay.Description,
                Trekkings = trekkings,
            };

            return View(vm);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Moderator")]
        public async Task<IActionResult> Update(UpdateDayVM vm)
        {
            await _dayService.UpdateAsync(vm);

            return RedirectToAction(nameof(Table));
        }

        [HttpGet]
        [Authorize(Roles = "Moderator, Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            await _dayService.DeleteAsync(id);

            return RedirectToAction(nameof(Table));
        }

        [HttpGet]
        [Authorize(Roles = "Moderator, Admin")]
        public async Task<IActionResult> Detail(int id)
        {
            var day = await _dayService.GetByIdAsync(id);

            return View(day);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Recover(int id)
        {
            await _dayService.RecoverAsync(id);

            return RedirectToAction(nameof(Table));
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Remove(int id)
        {
            await _dayService.RemoveAsync(id);

            return RedirectToAction(nameof(Table));
        }

    }
}
