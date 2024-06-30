using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TREKER.Business.Services.Interfaces;
using TREKER.Business.ViewModels.AccountVMs;
using TREKER.Core.Entities.UserModels;
using TREKER.Core.Enums;

namespace TREKER.Business.Services.Abstractions
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager = null, RoleManager<IdentityRole> roleManager = null)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        public async Task CreateRoleAsync()
        {
            foreach(var item in Enum.GetValues(typeof(AdminPanelRoles)))
            {
                if(!await _roleManager.RoleExistsAsync(item.ToString()))
                {
                    await _roleManager.CreateAsync(new IdentityRole(item.ToString()));
                }
            }
        }

        public async Task<AppUser> GetUserByEmailAddress(string emailAddress)
        {
            return await _userManager.FindByEmailAsync(emailAddress);
        }

        public async Task<bool> CheckIsRegisteredOnUser(string emailAddress)
        {
            var oldUser = await _userManager.FindByEmailAsync(emailAddress);

            if(oldUser is null)
            {
                return false;
            }
            else
            {
                return oldUser.IsRegistered;
            }
        }

        public async Task LoginAsync(LoginVM vm)
        {
            var oldUser = await _userManager.FindByEmailAsync(vm.Email);

            await _signInManager.SignInAsync(oldUser, true);
        }

        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task RegisterAsync(RegisterVM vm)
        {
            var newUser = new AppUser()
            {
                FirstName = vm.FirstName,
                LastName = vm.LastName,
                Email = vm.Email,
                UserName = $"{vm.FirstName.ToLower()}_{vm.LastName.ToLower()}",
            };

            await _userManager.CreateAsync(newUser, vm.Password);

            await _userManager.AddToRoleAsync(newUser, AdminPanelRoles.Admin.ToString());
        }
    }
}
