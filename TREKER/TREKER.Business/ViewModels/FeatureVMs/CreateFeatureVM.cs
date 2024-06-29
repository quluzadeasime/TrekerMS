using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TREKER.Business.ViewModels.FeatureVMs
{
    public class CreateFeatureVM
    {
        public string Name { get; set; }
    }

    public class CreateFeatureVMValidator : AbstractValidator<CreateFeatureVM>
    {
        public CreateFeatureVMValidator()
        {
            RuleFor(x=>x.Name)
                .NotEmpty()
                .MinimumLength(3)
                .MaximumLength(50)
                .WithMessage("Name's length between 3-50 character.");
        }
    }
}
