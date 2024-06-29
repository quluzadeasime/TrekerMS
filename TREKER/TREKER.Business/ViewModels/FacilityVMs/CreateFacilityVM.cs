using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TREKER.Business.ViewModels.FacilityVMs
{
    public class CreateFacilityVM
    {
        public string Name { get; set; }
        public string Icon { get; set; }
    }

    public class CreateDFacilityVMValidator : AbstractValidator<CreateFacilityVM>
    {
        public CreateDFacilityVMValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MinimumLength(4)
                .MaximumLength(50)
                .WithMessage("Name's length between 4-50 character.");
        }
    }
}
