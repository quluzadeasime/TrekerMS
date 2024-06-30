using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TREKER.Business.Services.Abstractions;
using TREKER.Business.Services.Interfaces;
using TREKER.Business.ViewModels.BlogVMs;
using TREKER.Business.ViewModels.TrekkingVMs;
using TREKER.DAL.Repositories.Interfaces;

namespace TREKER.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class BlogController : Controller
    {
        private readonly IBlogService _blogService;
        private readonly IDestinationService _destinationService;

        public BlogController(IBlogService blogService, IDestinationService destinationService)
        {
            _blogService = blogService;
            _destinationService = destinationService;
        }

        [HttpGet]
        [Authorize(Roles ="Admin,Moderator")]
        public async Task<IActionResult> Table()
        {
            if (User.IsInRole("Admin"))
            {
                return View(await _blogService.GetAllAsync());
            }
            else
            {
                return View((await _blogService.GetAllAsync()).Where(x => !x.IsDeleted));
            }
        }

        [HttpGet]
        [Authorize(Roles = "Moderator, Admin")]
        public async Task<IActionResult> Create()
        {
            CreateBlogVM vm = new()
            {
                Destinations = await (await _destinationService.GetAllAsync()).ToListAsync(),
            };

            return View(vm);
        }

        [HttpPost]
        [Authorize(Roles = "Moderator, Admin")]
        public async Task<IActionResult> Create(CreateBlogVM vm)
        {
            if (!ModelState.IsValid)
            {
                vm.Destinations = await (await _destinationService.GetAllAsync()).ToListAsync();
                return View(vm);
            }

            CreateBlogVMValidator validations = new();
            var validationResult = await validations.ValidateAsync(vm);

            if (!validationResult.IsValid)
            {
                vm.Destinations = await (await _destinationService.GetAllAsync()).ToListAsync();
                ModelState.Clear();
                validationResult.Errors.ForEach(x => ModelState.AddModelError(x.PropertyName, x.ErrorMessage));
                return View(vm);
            }

            await _blogService.CreateAsync(vm);

            return RedirectToAction(nameof(Table));
        }

        [HttpGet]
        [Authorize(Roles = "Moderator, Admin")]
        public async Task<IActionResult> Update(int id)
        {
            var oldBlog = await _blogService.GetByIdAsync(id);

            UpdateBlogVM vm = new()
            {
                Title = oldBlog.Title,
                ByUsername = oldBlog.ByUsername,
                Description = oldBlog.Description,
                OldImages = oldBlog.Images,
                DestinationId = oldBlog.DestinationId,
                Destinations = await (await _destinationService.GetAllAsync()).ToListAsync(),
            };

            return View(vm);
        }

        [HttpPost]
        [Authorize(Roles = "Moderator, Admin")]
        public async Task<IActionResult> Update(UpdateBlogVM vm)
        {
            var oldBlog = await _blogService.GetByIdAsync(vm.Id);
            vm.Destinations = await (await _destinationService.GetAllAsync()).ToListAsync();
            vm.OldImages = oldBlog.Images;

            UpdateBlogVMValidator validations = new UpdateBlogVMValidator();
            var validationResult = await validations.ValidateAsync(vm);

            if (!validationResult.IsValid)
            {
                ModelState.Clear();

                validationResult.Errors.ForEach(x => ModelState.AddModelError(x.PropertyName, x.ErrorMessage));

                return View(vm);
            }

            await _blogService.UpdateAsync(vm);

            return RedirectToAction(nameof(Table));
        }


        [HttpGet]
        [Authorize(Roles = "Moderator, Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            await _blogService.DeleteAsync(id);

            return RedirectToAction(nameof(Table));
        }

        [HttpGet]
        [Authorize(Roles = "Moderator, Admin")]
        public async Task<IActionResult> Detail(int id)
        {
            var blog = await _blogService.GetByIdAsync(id);

            return View(blog);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Recover(int id)
        {
            await _blogService.RecoverAsync(id);

            return RedirectToAction(nameof(Table));
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Remove(int id)
        {
            await _blogService.RemoveAsync(id);

            return RedirectToAction(nameof(Table));
        }
    }
}
