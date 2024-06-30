using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TREKER.Business.Helpers;
using TREKER.Core.Entities;

namespace TREKER.Business.ViewModels.DestinationVMs
{
    public class CreateDestinationVM
    {
        // Base Fields
        public string Title { get; set; }
        public IFormFile File { get; set; }
        public int RegionId { get; set; }

        // Relation Fields
        public IQueryable<Region> Regions { get; set; }
    }

    public class CreateDestinationVMValidator : AbstractValidator<CreateDestinationVM>
    {
        public CreateDestinationVMValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .WithMessage("You must filled the input Title.")
                .MinimumLength(5)
                .MaximumLength(70)
                .WithMessage("Title's length between 5-70 character.");
            RuleFor(x => x.File)
                .NotNull()
                .WithMessage("You must filled the input image.")
                .Must(x => FileManager.CheckFile(x) == true)
                .WithMessage("File type must be image and lower than 10 MB.");
        }
    }
}
