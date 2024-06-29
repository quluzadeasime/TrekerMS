using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TREKER.Business.ViewModels.Commons;

namespace TREKER.Business.ViewModels.RegionVMs
{
    public class UpdateRegionVM : BaseEntityVm
    {
        public string? Name { get; set; }
    }

    public class UpdateRegionVMValidator : AbstractValidator<UpdateRegionVM>
    {
        public UpdateRegionVMValidator()
        {
            RuleFor(x => x.Name)
               .MinimumLength(4)
               .MaximumLength(50)
               .WithMessage("Name's length between 4-50 character.");
        }
    }
}
