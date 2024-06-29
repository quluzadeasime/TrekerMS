using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TREKER.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class FacilityController : Controller
    {
        public IActionResult Table()
        {
            return View();
        }
    }
}
