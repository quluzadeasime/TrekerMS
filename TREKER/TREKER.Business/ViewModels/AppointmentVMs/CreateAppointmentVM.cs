using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TREKER.Business.ViewModels.Commons;
using TREKER.Core.Entities;

namespace TREKER.Business.ViewModels.AppointmentVMs
{
    public class CreateAppointmentVM 
    {
        public string Fullname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime Date { get; set; }


        // View Models
        public Trekking? Trekking { get; set; }
		public IQueryable<Trekking>? Trekkings { get; set; }
		public IQueryable<Destination>? Destinations { get; set; }
	}

    public class CreateAppointmentVMValidator : AbstractValidator<CreateAppointmentVM>
    {
        public CreateAppointmentVMValidator()
        {
            RuleFor(x => x.Fullname)
                  .NotEmpty()
                  .WithMessage("Please enter your full name.")
                  .MaximumLength(110)
                  .WithMessage("The name must not exceed 110 characters.");

            RuleFor(x => x.Email)
                   .NotEmpty()
                   .WithMessage("Please enter your email address.")
                   .MaximumLength(75)
                   .WithMessage("The email address must not exceed 75 characters.");

            RuleFor(x => x.Phone)
                  .NotEmpty()
                  .WithMessage("Please enter your phone.")
                  .MaximumLength(15)
                  .WithMessage("The phone must not exceed 15 characters.");

          
            RuleFor(x => x.Date)
                 .NotEmpty()
                 .WithMessage("Please enter your date.");
        }
    }
}
