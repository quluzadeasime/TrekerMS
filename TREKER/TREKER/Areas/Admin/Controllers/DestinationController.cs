using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TREKER.Business.Services.Abstractions;
using TREKER.Business.Services.Interfaces;
using TREKER.Business.ViewModels.DestinationVMs;
using TREKER.Business.ViewModels.RegionVMs;
using TREKER.Business.ViewModels.TeamMemberVMs;
using TREKER.Core.Enums;

namespace TREKER.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class DestinationController : Controller
    {
        private readonly IDestinationService _destinationService;
        private readonly IRegionService _regionService;
        public DestinationController(IDestinationService destinationService, IRegionService regionService)
        {
            _destinationService = destinationService;
            _regionService = regionService;
        }

        [HttpGet]
        [Authorize(Roles = "Moderator, Admin")]
        public async Task<IActionResult> Table()
        {
            if (User.IsInRole("Admin"))
            {
                return View(await _destinationService.GetAllAsync());
            }
            else
            {
                return View((await _destinationService.GetAllAsync()).Where(x => !x.IsDeleted));
            }
        }

        [HttpGet]
        [Authorize(Roles = "Moderator, Admin")]
        public async Task<IActionResult> Create()
        {
            CreateDestinationVM vm = new()
            {
                Regions = await (await _regionService.GetAllAsync()).ToListAsync()
            };

            return View(vm);
        }

        [HttpPost]
        [Authorize(Roles = "Moderator, Admin")]
        public async Task<IActionResult> Create(CreateDestinationVM vm)
        {
            vm.Regions = await (await _regionService.GetAllAsync()).ToListAsync();

            CreateDestinationVMValidator validations = new CreateDestinationVMValidator();
            var validationResult = await validations.ValidateAsync(vm);

            if (!validationResult.IsValid)
            {
                ModelState.Clear();

                validationResult.Errors.ForEach(x => ModelState.AddModelError(x.PropertyName, x.ErrorMessage));

                return View(vm);
            }

            await _destinationService.CreateAsync(vm);

            return RedirectToAction(nameof(Table));
        }

        [HttpGet]
        [Authorize(Roles = "Moderator, Admin")]
        public async Task<IActionResult> Update(int id)
        {
            var oldTDestination = await _destinationService.GetByIdAsync(id);

            UpdateDestinationVM vm = new()
            {
                Title = oldTDestination.Title,
                Regions = await (await _regionService.GetAllAsync()).ToListAsync(),
                OldImage = oldTDestination.ImageUrl,
                RegionId = oldTDestination.RegionId
            };

            return View(vm);
        }

        [HttpPost]
        [Authorize(Roles = "Moderator, Admin")]
        public async Task<IActionResult> Update(UpdateDestinationVM vm)
        {
            vm.Regions = await(await _regionService.GetAllAsync()).ToListAsync();
            //vm.OldImage = (await _destinationService.GetByIdAsync(vm.Id)).ImageUrl;

            UpdateDestinationVMValidator validations = new UpdateDestinationVMValidator();
            var validationResult = await validations.ValidateAsync(vm);

            if (!validationResult.IsValid)
            {
                ModelState.Clear();

                validationResult.Errors.ForEach(x => ModelState.AddModelError(x.PropertyName, x.ErrorMessage));

                return View(vm);
            }

            await _destinationService.UpdateAsync(vm);

            return RedirectToAction(nameof(Table));
        }

        [HttpGet]
        [Authorize(Roles = "Moderator, Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            await _destinationService.DeleteAsync(id);

            return RedirectToAction(nameof(Table));
        }

        [HttpGet]
        [Authorize(Roles = "Moderator, Admin")]
        public async Task<IActionResult> Detail(int id)
        {
            var destination = await _destinationService.GetByIdAsync(id);

            return View(destination);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Recover(int id)
        {
            await _destinationService.RecoverAsync(id);

            return RedirectToAction(nameof(Table));
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Remove(int id)
        {
            await _destinationService.RemoveAsync(id);

            return RedirectToAction(nameof(Table));
        }

    }
}
