using FluentValidation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TREKER.Business.Helpers;
using TREKER.Core.Enums;

namespace TREKER.Business.ViewModels.TeamMemberVMs
{
    public class CreateTeamMemberVm 
    {
        public string FullName { get; set; }
        public IFormFile File { get; set; }
        public TeamMemberRoles MemberRole { get; set; }

        public ICollection<TeamMemberRoles>? TeamMemberRoles { get; set; }
    }

    public class CreateTeamMemberVmValidator:AbstractValidator<CreateTeamMemberVm>
    {
        public CreateTeamMemberVmValidator()
        {
            RuleFor(x => x.FullName)
                .NotEmpty()
                .WithMessage("You must filleed the input full name.")
                .MinimumLength(6)
                .MaximumLength(100)
                .WithMessage("Full name's length between 6-100 character.");

            RuleFor(x => x.File)
                .NotNull()
                .WithMessage("You must filled the input image.")
                .Must(x => FileManager.CheckFile(x) == true)
                .WithMessage("File type must be image and lower than 10 MB.");
        }
    }
}
