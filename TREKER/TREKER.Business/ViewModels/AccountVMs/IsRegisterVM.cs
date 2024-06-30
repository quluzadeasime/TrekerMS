using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TREKER.Business.ViewModels.AccountVMs
{
    public class IsRegisterVM
    {
        public string Email { get; set; }
    }

    public class IsRegisterVMValidator : AbstractValidator<IsRegisterVM>
    {
        public IsRegisterVMValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email address is required.");
        }
    }
}
