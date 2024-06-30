using Microsoft.AspNetCore.Mvc;
using TREKER.Business.Services.Interfaces;

namespace TREKER.MVC.Controllers
{
    public class AboutController : Controller
    {
        private readonly ITeamMemberService _teamMemberService;

        public AboutController(ITeamMemberService teamMemberService)
        {
            _teamMemberService = teamMemberService;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.ActivePage = "About";
            var teamMembers = (await _teamMemberService.GetAllAsync()).Where(x => !x.IsDeleted);
            return View(teamMembers);
        }
    }
}
