using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TREKER.Business.ViewModels.ContactVMs
{
    public class ContactVM
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
    }

    public class ContactVMValidator : AbstractValidator<ContactVM>
    {
        public ContactVMValidator()
        {
            RuleFor(x => x.Name)
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

            RuleFor(x => x.Subject)
                   .NotEmpty()
                   .WithMessage("Please enter your subject.")
                   .MaximumLength(80)
                   .WithMessage("The subject must not exceed 80 characters.");

            RuleFor(x => x.Message)
                 .NotEmpty()
                 .WithMessage("Please enter your message.");

        }
    }
}
