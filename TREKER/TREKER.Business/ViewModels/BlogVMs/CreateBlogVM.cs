using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TREKER.Business.Helpers;
using TREKER.Core.Entities;

namespace TREKER.Business.ViewModels.BlogVMs
{
    public class CreateBlogVM
    {
        // Base Fields
        public string Title { get; set; }
        public string ByUsername { get; set; }
        public string Description { get; set; }
        public int DestinationId { get; set; }
        public IQueryable<BlogImageVM> Images { get; set; }

        // Relation Fields
        public IQueryable<Destination>? Destinations { get; set; } 
    }

    public class BlogImageVM
    {
        public IFormFile File { get; set; }
    }

    public class CreateBlogVMValidator : AbstractValidator<CreateBlogVM>
    {
        public CreateBlogVMValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .WithMessage("You must filled the input Title.")
                .MinimumLength(5)
                .MaximumLength(70)
                .WithMessage("Title's length between 5-70 character.");
            RuleFor(x => x.ByUsername)
                .NotEmpty()
                .WithMessage("You must filled the input Username.")
                .MinimumLength(5)
                .MaximumLength(50)
                .WithMessage("Username's length between 5-50 character.");
            RuleFor(x => x.Description)
                .NotEmpty()
                .WithMessage("You must filled the input Description.");

            RuleFor(x => x.Images)
                .NotEmpty()
                .WithMessage("You must filled the input Images.")
                .Must(x => x.Any(x => FileManager.CheckFile(x.File) == true))
                .WithMessage("File type must be image and lower than 10 MB.");

        }
    }
}
