using FluentValidation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TREKER.Business.Helpers;
using TREKER.Business.ViewModels.Commons;
using TREKER.Core.Enums;

namespace TREKER.Business.ViewModels.TeamMemberVMs
{
    public class UpdateTeamMemberVm : BaseEntityVm
    {
        // Base Fields
        public string? FullName { get; set; }
        public IFormFile? File { get; set; }
        public TeamMemberRoles? MemberRole { get; set; }
        public ICollection<TeamMemberRoles>? TeamMemberRoles { get; set; }

        // Relation Fields
        public string OldImage { get; set; }
    }

    public class UpdateTeamMemberVmValidator : AbstractValidator<UpdateTeamMemberVm>
    {
        public UpdateTeamMemberVmValidator()
        {
            RuleFor(x => x.FullName)
                .MaximumLength(100)
                .MinimumLength(6)
                .WithMessage("Full name's length between 6-100 character.");

            RuleFor(x => x.File)
                .Must(x => x == null || FileManager.CheckFile(x))
                .WithMessage("File type must be image and lower than 10 MB.");
        }
    }
}
