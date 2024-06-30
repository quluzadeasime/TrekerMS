using FluentValidation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TREKER.Business.Helpers;
using TREKER.Business.ViewModels.Commons;
using TREKER.Core.Entities;

namespace TREKER.Business.ViewModels.DestinationVMs
{
    public class UpdateDestinationVM : BaseEntityVm
    {
        // Base Fields
        public string? Title { get; set; }
        public IFormFile? File { get; set; }
        public int? RegionId { get; set; }

        // Relation Fields
        public IQueryable<Region> Regions { get; set; }
        public string OldImage { get; set; }
    }

    public class UpdateDestinationVMValidator : AbstractValidator<UpdateDestinationVM>
    {
        public UpdateDestinationVMValidator()
        {
            RuleFor(x => x.Title)
                .MinimumLength(5)
                .MaximumLength(70)
                .WithMessage("Title's length between 5-70 character.");
            RuleFor(x => x.File)
                .Must(x => FileManager.CheckFile(x) == true)
                .WithMessage("File type must be image and lower than 10 MB.");
        }
    }
}
