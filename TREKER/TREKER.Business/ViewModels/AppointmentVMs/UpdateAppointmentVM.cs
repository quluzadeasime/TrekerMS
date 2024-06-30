using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TREKER.Business.ViewModels.Commons;

namespace TREKER.Business.ViewModels.AppointmentVMs
{
	public class UpdateAppointmentVM : BaseEntityVm
	{
        public string? Fullname { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public DateTime Date { get; set; }
        public bool IsVerified { get; set; }
        public bool IsFinished { get; set; }
    }

    public class UpdateAppointmentVMValidator : AbstractValidator<UpdateAppointmentVM>
    {
        public UpdateAppointmentVMValidator()
        {
            RuleFor(x => x.Fullname)
                 .MaximumLength(110)
                 .WithMessage("The name must not exceed 110 characters.");

            RuleFor(x => x.Email)
                   .MaximumLength(75)
                   .WithMessage("The email address must not exceed 75 characters.");

            RuleFor(x => x.Phone)
                  .MaximumLength(15)
                  .WithMessage("The phone must not exceed 15 characters.");

            RuleFor(x => x.Date)
                .NotEmpty()
                .WithMessage("You must filled date input.");
                

        }
    }
}
