using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TREKER.Business.ViewModels.DifficultyVMs
{
    public class CreateDifficultyVM
    {
        public string Name { get; set; }
    }

    public class CreateDifficultyVMValidator : AbstractValidator<CreateDifficultyVM>
    {
        public CreateDifficultyVMValidator()
        {
            RuleFor(x=>x.Name)
                .NotEmpty()
                .MinimumLength(4)
                .MaximumLength(50)
                .WithMessage("Name's length between 4-50 character.");
        }
    }
}
