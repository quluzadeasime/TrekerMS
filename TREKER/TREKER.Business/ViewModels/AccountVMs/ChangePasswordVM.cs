using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TREKER.Business.ViewModels.AccountVMs
{
    public class ChangePasswordVM
    {
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
    }

    public class ChangePasswordVMValidator : AbstractValidator<ChangePasswordVM>
    {
        public ChangePasswordVMValidator()
        {
            RuleFor(x => x.NewPassword)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(6).WithMessage("Password must be at least 6 characters long.")
                .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter")
                .Matches("[a-z]").WithMessage("Password must contain at least one lowercase letter")
                .Matches("[0-9]").WithMessage("Password must be contain at least one numeric digit.")
                .Matches("[^a-zA-Z0-9]").WithMessage("Password must contain at least one non-alphanumeric character.");

            RuleFor(x => x.ConfirmPassword)
                .NotEmpty().WithMessage("Please enter your confirm password")
                .Equal(x => x.NewPassword).WithMessage("Password and Confirm Password do not match");
        }
    }
}
