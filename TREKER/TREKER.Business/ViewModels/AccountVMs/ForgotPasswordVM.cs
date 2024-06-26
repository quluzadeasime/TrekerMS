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
    public class ForgotPasswordVM
    {
        public string Email { get; set; }
    }

    public class ForgotPasswordVMValidator : AbstractValidator<ForgotPasswordVM>
    {
        private readonly UserManager<AppUser> _userManager;

        public ForgotPasswordVMValidator(UserManager<AppUser> userManager)
        {
            _userManager = userManager;

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email address is required.")
                .EmailAddress().WithMessage("Invalid email address")
                .MustAsync(BeAValidUser).WithMessage("Email address already does not exist.");
        }

        private async Task<bool> BeAValidUser(string email, CancellationToken cancellationToken)
        {
            var user = email is null ? null : await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
