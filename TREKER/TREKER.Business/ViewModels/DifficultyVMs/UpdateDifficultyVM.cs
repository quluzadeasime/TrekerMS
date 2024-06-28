using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TREKER.Business.ViewModels.Commons;

namespace TREKER.Business.ViewModels.DifficultyVMs
{
    public class UpdateDifficultyVM : BaseEntityVm
    {
        public string? Name { get; set; }
    }

    public class UpdateDifficultyVMValidator : AbstractValidator<UpdateDifficultyVM>
    {
        public UpdateDifficultyVMValidator()
        {
            RuleFor(x => x.Name)
               .MinimumLength(4)
               .MaximumLength(50)
               .WithMessage("Full name's length between 4-50 character.");
        }
    }
}
