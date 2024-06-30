using FluentValidation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TREKER.Business.Helpers;
using TREKER.Business.ViewModels.Commons;
using TREKER.Business.ViewModels.TrekkingVMs;
using TREKER.Core.Entities;

namespace TREKER.Business.ViewModels.BlogVMs
{
    public class UpdateBlogVM : BaseEntityVm
    {
        // Base Fields
        public string? Title { get; set; }
        public string? ByUsername { get; set; }
        public string? Description { get; set; }
        public int DestinationId { get; set; }
        public ICollection<IFormFile>? Images { get; set; }

        // Relation Fields
        public ICollection<BlogImage> OldImages { get; set; }
        public ICollection<int>? ViewImageIds { get; set; }
        public ICollection<Destination> Destinations { get; set; }
    }

    public class UpdateBlogVMValidator : AbstractValidator<UpdateBlogVM>
    {
        public UpdateBlogVMValidator()
        {
            RuleFor(x => x.Title)
                .MinimumLength(5)
                .MaximumLength(70)
                .WithMessage("Title's length between 5-70 character.");
            
            RuleFor(x => x.ByUsername)
                .MinimumLength(5)
                .MaximumLength(50)
                .WithMessage("Username's length between 5-50 character.");

            RuleFor(x => x.Images)
           .Must(x => x == null || x.All(FileManager.CheckFile))
           .WithMessage("File type must be image and lower than 10 MB.");

            RuleFor(x => x)
                .Must(HaveAtLeastOneImageOrViewImage)
                .WithMessage("You must upload at least one image.");
        }

        private bool HaveAtLeastOneImageOrViewImage(UpdateBlogVM model)
        {
            return (model.Images != null && model.Images.Any(FileManager.CheckFile)) ||
                   (model.ViewImageIds != null && model.ViewImageIds.Count > 0);
        }
    }
}
