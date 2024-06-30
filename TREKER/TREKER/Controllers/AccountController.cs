using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using System.Net.Mail;
using TREKER.Business.Services.Interfaces;
using TREKER.Business.ViewModels.AccountVMs;
using TREKER.Core.Entities.UserModels;
using TREKER.DAL.Exceptions.Account;

namespace TREKER.MVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly LinkGenerator _linkGenerator;
        private readonly IHttpContextAccessor _http;
        private readonly ISendMessageService _sendMessageService;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(IAccountService accountService, LinkGenerator linkGenerator, IHttpContextAccessor http, ISendMessageService sendMessageService, SignInManager<AppUser> signInManager, UserManager<AppUser> userManager)
        {
            _accountService = accountService;
            _linkGenerator = linkGenerator;
            _http = http;
            _sendMessageService = sendMessageService;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> CheckIsRegisteredUser()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CheckIsRegisteredUser(IsRegisterVM vm)
        {
            IsRegisterVMValidator validator = new IsRegisterVMValidator();
            var validationResult = await validator.ValidateAsync(vm);

            if (!validationResult.IsValid)
            {
                ModelState.Clear();
                validationResult.Errors.ForEach(x => ModelState.AddModelError(x.PropertyName, x.ErrorMessage));
                return View(vm);
            }

            if (await _accountService.CheckIsRegisteredOnUser(vm.Email))
            {
                return RedirectToAction(nameof(Login));
            }
            else
            {
                return Redirect("/Account/Register?notRegistered=1");
            }
        }

        [HttpGet]
        public IActionResult CheckEmailAddress()
        {
            if (HttpContext.Session.Keys.Any(x => x == "MessageSended"))
            {
                return View();
            }
            else
            {
                return Redirect("/Account/Register?messageSended=1");
            }
        }

        [HttpGet]
        public IActionResult CheckChangePassword()
        {
            if (HttpContext.Session.Keys.Any(x => x == "PasswordMessageSended"))
            {
                HttpContext.Session.SetString("ChangePasswordIsOpen", "true");

                return View();
            }
            else
            {
                return Redirect("/Account/Register?passwordMessageSended=1");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Register(int? messageSended, int? passwordMessageSended, int? changePasswordIsOpen, int? notRegistered)
        {
            if (messageSended == 1) return NotFound();
            if (passwordMessageSended == 1) return NotFound();
            if (changePasswordIsOpen == 1) return NotFound();
            if (notRegistered == 1) ViewData["notRegistered"] = "This email address was not registered!";

            if (User.Identity.IsAuthenticated)
            {
                return Redirect("/Admin");
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM vm)
        {
            RegisterVMValidator validator = new RegisterVMValidator(_userManager);
            var validationResult = await validator.ValidateAsync(vm);
            var emailAddress = vm.Email;

            if (!validationResult.IsValid)
            {
                ModelState.Clear();
                validationResult.Errors.ForEach(x => ModelState.AddModelError(x.PropertyName, x.ErrorMessage));
                return View(vm);
            }

            await _accountService.RegisterAsync(vm);

            var token = await _sendMessageService.GenerateTokenAsync(emailAddress);

            string url = _linkGenerator.GetUriByAction(_http.HttpContext, action: "ConfirmEmailAddress", controller: "SendMessage",
            values: new
            {
                token,
                emailAddress
            });

            await _sendMessageService.SendUrlMessageAsync(emailAddress, url);

            Response.Cookies.Append("ConfirmationLinkSent", "true", new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                Expires = DateTimeOffset.UtcNow.AddMinutes(30)
            });

            HttpContext.Session.SetString("MessageSended", "true");

            return RedirectToAction(nameof(CheckEmailAddress));
        }


        [HttpGet]
        public async Task<IActionResult> Login()
        {

            await _accountService.CreateRoleAsync();

            if (User.Identity.IsAuthenticated)
            {
                return Redirect("/Admin");
            }
            else
            {
                HttpContext.Session.Remove("MessageSended");

                return View();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM vm)
        {
            try
            {
                LoginVMValidator validations = new LoginVMValidator(_userManager, _signInManager);
                var validationResult = await validations.ValidateAsync(vm);

                if (!validationResult.IsValid)
                {
                    ModelState.Clear();

                    var addedErrors = new HashSet<string>();

                    foreach (var error in validationResult.Errors)
                    {
                        var errorMessage = error.ErrorMessage;
                        if (addedErrors.Add(errorMessage))
                        {
                            ModelState.AddModelError(error.PropertyName, errorMessage);
                        }
                    }

                    return View(vm);
                }

                await _accountService.LoginAsync(vm);

                return Redirect("/Admin");
            }
            catch (UserLoginException ex)
            {
                ModelState.AddModelError(ex.PropertyName, ex.Message);

                return View(vm);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            if (User.Identity.IsAuthenticated)
            {
                await _accountService.LogoutAsync();

                return RedirectToAction(nameof(Login));
            }
            else
            {
                return Redirect("/Admin");
            }

        }

        [HttpGet]
        public async Task<IActionResult> ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordVM vm)
        {
            ForgotPasswordVMValidator validations = new ForgotPasswordVMValidator(_userManager);
            var validationResult = await validations.ValidateAsync(vm);
            var emailAddress = vm.Email;

            if (!validationResult.IsValid)
            {
                ModelState.Clear();

                validationResult.Errors.ForEach(x => ModelState.AddModelError(x.PropertyName, x.ErrorMessage));

                return View(vm);
            }

            var token = await _sendMessageService.GenerateUserPasswordResetTokenAsync(emailAddress);

            string url = _linkGenerator.GetUriByAction(_http.HttpContext, action: "ConfirmChangePassword", controller: "SendMessage",
            values: new
            {
                token,
                emailAddress
            });

            await _sendMessageService.SendUrlMessageAsync(emailAddress, url);

            Response.Cookies.Append("ConfirmationLinkSent", "true", new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                Expires = DateTimeOffset.UtcNow.AddMinutes(30)
            });

            HttpContext.Session.SetString("PasswordMessageSended", "true");

            return RedirectToAction(nameof(CheckChangePassword));

        }

        [HttpGet]
        public async Task<IActionResult> ChangePassword()
        {
            if (User.Identity.IsAuthenticated)
            {
                return Redirect("/Admin");
            }
            else
            {
                if (HttpContext.Session.Keys.Any(x => x == "ChangePasswordIsOpen"))
                {
                    HttpContext.Session.Remove("PasswordMessageSended");

                    return View();
                }
                else
                {
                    return Redirect("/Account/Register?changePasswordIsOpen=1");
                }

            }
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordVM vm, string token, string emailAddress)
        {
            ChangePasswordVMValidator validations = new ChangePasswordVMValidator();
            var validationResult = await validations.ValidateAsync(vm);

            if (!validationResult.IsValid)
            {
                ModelState.Clear();

                validationResult.Errors.ForEach(x => ModelState.AddModelError(x.PropertyName, x.ErrorMessage));

                return View(vm);
            }

            await _sendMessageService.ConfirmChangePassword(emailAddress, token, vm.NewPassword);

            return RedirectToAction(nameof(Login));
        }

    }
}