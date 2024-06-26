using FluentValidation;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TREKER.Core.Entities.UserModels;

namespace TREKER.Business.ViewModels.AccountVMs
{
    public class RegisterVM
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }

    public class RegisterVMValidator : AbstractValidator<RegisterVM>
    {
        private readonly UserManager<AppUser> _userManager;

        public RegisterVMValidator(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
            RuleFor(vm => vm.FirstName)
               .NotEmpty()
               .WithMessage("First name is required.")
               .MaximumLength(50)
               .WithMessage("First name cannot exced 50 characters.");

            RuleFor(vm => vm.LastName)
                .NotEmpty()
                .WithMessage("Last name is required.")
                .MaximumLength(50)
                .WithMessage("Last name cannot exced 50 characters.");

            RuleFor(vm => vm.Email)
                .NotEmpty()
                .WithMessage("Email address is required.")
                .EmailAddress()
                .WithMessage("Invalid email address.");

            RuleFor(vm => vm.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(6).WithMessage("Password must be at least 6 characters long.")
                .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
                .Matches("[a-z]").WithMessage("Password must contain at least one lowercase letter.")
                .Matches("[0-9]").WithMessage("Password must contain at least one numeric digit.")
                .Matches("[^a-zA-Z0-9]").WithMessage("Password must contain at least one non-alphanumeric character.");

            RuleFor(x => x.ConfirmPassword)
                 .NotEmpty()
                 .WithMessage("Please enter your confirm password.")
                 .Equal(x => x.Password)
                 .WithMessage("Password and Confirm Password do not match.");
        }

      
        private async Task<bool> BeAValidUser(string email, CancellationToken cancellationToken)
        {
            var user = email is null ? null : await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
