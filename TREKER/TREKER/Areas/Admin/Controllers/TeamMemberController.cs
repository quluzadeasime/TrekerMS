using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TREKER.Business.Services.Interfaces;
using TREKER.Business.ViewModels.TeamMemberVMs;
using TREKER.Core.Enums;

namespace TREKER.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class TeamMemberController : Controller
    {
        private readonly ITeamMemberService _teamMemberService;

        public TeamMemberController(ITeamMemberService teamMemberService)
        {
            _teamMemberService = teamMemberService;
        }

        [HttpGet]
        [Authorize(Roles = "Moderator, Admin")]
        public async Task<IActionResult> Table()
        {
            if (User.IsInRole("Admin"))
            {
                return View(await _teamMemberService.GetAllAsync());
            }
            else
            {
                return View((await _teamMemberService.GetAllAsync()).Where(x => !x.IsDeleted));
            }
        }

        [HttpGet]
        [Authorize(Roles = "Moderator, Admin")]
        public async Task<IActionResult> Create()
        {
            CreateTeamMemberVm vm = new()
            {
                TeamMemberRoles = Enum.GetValues<TeamMemberRoles>()
            };

            return View(vm);
        }

        [HttpPost]
        [Authorize(Roles = "Moderator, Admin")]
        public async Task<IActionResult> Create(CreateTeamMemberVm vm)
        {
            vm.TeamMemberRoles = Enum.GetValues<TeamMemberRoles>();

            CreateTeamMemberVmValidator validations = new CreateTeamMemberVmValidator();
            var validationResult = await validations.ValidateAsync(vm);

            if (!validationResult.IsValid)
            {
                ModelState.Clear();

                validationResult.Errors.ForEach(x => ModelState.AddModelError(x.PropertyName, x.ErrorMessage));

                return View(vm);
            }

            await _teamMemberService.CreateAsync(vm);

            return RedirectToAction(nameof(Table));
        }

        [HttpGet]
        [Authorize(Roles = "Moderator, Admin")]
        public async Task<IActionResult> Update(int id)
        {
            var oldTeamMember = await _teamMemberService.GetByIdAsync(id);

            UpdateTeamMemberVm vm = new()
            {
                FullName = oldTeamMember.FullName,
                MemberRole = oldTeamMember.TeamMemberRoles,
                TeamMemberRoles = Enum.GetValues<TeamMemberRoles>()
            };

            return View(vm);
        }

        [HttpPost]
        [Authorize(Roles = "Moderator, Admin")]
        public async Task<IActionResult> Update(UpdateTeamMemberVm vm)
        {
            vm.TeamMemberRoles = Enum.GetValues<TeamMemberRoles>();

            UpdateTeamMemberVmValidator validations = new UpdateTeamMemberVmValidator();
            var validationResult = await validations.ValidateAsync(vm);

            if (!validationResult.IsValid)
            {
                ModelState.Clear();

                validationResult.Errors.ForEach(x => ModelState.AddModelError(x.PropertyName, x.ErrorMessage));

                return View(vm);
            }

            await _teamMemberService.UpdateAsync(vm);

            return RedirectToAction(nameof(Table));
        }

        [HttpGet]
        [Authorize(Roles = "Moderator, Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            await _teamMemberService.DeleteAsync(id);

            return RedirectToAction(nameof(Table));
        }

        [HttpGet]
        [Authorize(Roles = "Moderator, Admin")]
        public async Task<IActionResult> Detail(int id)
        {
            var teamMember = await _teamMemberService.GetByIdAsync(id);

            return View(teamMember);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Recover(int id)
        {
            await _teamMemberService.RecoverAsync(id);

            return RedirectToAction(nameof(Table));
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Remove(int id)
        {
            await _teamMemberService.RemoveAsync(id);

            return RedirectToAction(nameof(Table));
        }
    }
}
