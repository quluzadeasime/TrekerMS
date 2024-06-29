using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TREKER.Business.ViewModels.RegionVMs
{
    public class CreateRegionVM
    {
        public string Name { get; set; }
    }

    public class CreateRegionVMValidator : AbstractValidator<CreateRegionVM>
    {
        public CreateRegionVMValidator()
        {
            RuleFor(x => x.Name)
               .NotEmpty()
               .MinimumLength(4)
               .MaximumLength(50)
               .WithMessage("Name's length between 4-50 character.");
        }
    }
}
