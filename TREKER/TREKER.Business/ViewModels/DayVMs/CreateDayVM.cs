using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TREKER.Core.Entities;

namespace TREKER.Business.ViewModels.DayVMs
{
    public class CreateDayVM
    {
        public string Description { get; set; }
        public int TrekkingId { get; set; }
    }

    public class CreateDayVMValidator : AbstractValidator<CreateDayVM>
    {
        public CreateDayVMValidator()
        {
            RuleFor(x => x.Description)
              .NotEmpty()
              .WithMessage("You must filled the input Description.");
        }
    }

}
