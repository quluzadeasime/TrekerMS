using FluentValidation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TREKER.Business.Helpers;
using TREKER.Business.ViewModels.Commons;
using TREKER.Core.Entities.UserModels;

namespace TREKER.Business.ViewModels.PageVM
{
    public class SettingVM : BaseEntityVm
    {
        public string? Email { get; set; }
        public string? Phone1 { get; set; }
        public string? Phone2 { get; set; }
        public string? Description { get; set; }
        public string? Address1 { get; set; }
        public string? Address2 { get; set; }
        public string? Facebook { get; set; }
        public string? Instagram { get; set; }
        public string? Twitter { get; set; }
        public string? Youtube { get; set; }
        public IFormFile? File { get; set; }
    }

    public class SettingVMValidator : AbstractValidator<SettingVM>
    {
        public SettingVMValidator()
        {
            RuleFor(x => x.Email)
                .MinimumLength(6)
                .MaximumLength(100)
                .WithMessage("Email address's length between 6-100 character.");

            RuleFor(x => x.Address1)
                .MinimumLength(15)
                .MaximumLength(200)
                .WithMessage("Address's length between 15-200 character.");

            RuleFor(x => x.Address2)
                .MinimumLength(15)
                .MaximumLength(200)
                .WithMessage("Address's length between 15-200 character.");

            RuleFor(x => x.Description)
                .MinimumLength(25)
                .MaximumLength(1000)
                .WithMessage("Description's length between 25-200 character.");

            RuleFor(x => x.Phone1)
                .MinimumLength(3)
                .MaximumLength(14)
                .WithMessage("Phone's length between 3-14 character.");

            RuleFor(x => x.Phone2)
               .MinimumLength(3)
               .MaximumLength(14)
               .WithMessage("Phone's length between 3-14 character.");

            RuleFor(x => x.File)
                .Must(x => x is not null ? FileManager.CheckFile(x) == true : true)
                .WithMessage("File type must be image and lower than 10 MB.");
        }
    }
}
