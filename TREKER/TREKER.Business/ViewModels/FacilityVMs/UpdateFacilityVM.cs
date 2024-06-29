using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TREKER.Business.ViewModels.Commons;

namespace TREKER.Business.ViewModels.FacilityVMs
{
    public class UpdateFacilityVM : BaseEntityVm
    {
        public string? Name { get; set; }
        public string? Icon { get; set; }
    }

    public class UpdateFacilityVMValidator : AbstractValidator<UpdateFacilityVM>
    {
        public UpdateFacilityVMValidator()
        {
            RuleFor(x => x.Name)
              .MinimumLength(4)
              .MaximumLength(50)
              .WithMessage("Name's length between 4-50 character.");
        }
    }
}
