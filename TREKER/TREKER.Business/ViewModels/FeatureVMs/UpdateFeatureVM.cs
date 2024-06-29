using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TREKER.Business.ViewModels.Commons;

namespace TREKER.Business.ViewModels.FeatureVMs
{
    public class UpdateFeatureVM : BaseEntityVm
    {
        public string? Name { get; set; }
    }

    public class UpdateFeatureVMValidator : AbstractValidator<UpdateFeatureVM>
    {
        public UpdateFeatureVMValidator()
        {
            RuleFor(x => x.Name)
              .MinimumLength(3)
              .MaximumLength(50)
              .WithMessage("Name's length between 3-50 character.");
        }
    }
}
