using FluentValidation;
using Microsoft.AspNetCore.Http;
using TREKER.Business.Helpers;
using TREKER.Core.Entities;

namespace TREKER.Business.ViewModels.TrekkingVMs
{
    public class CreateTrekkingVM
    {
        // Base Fields
        public string Title { get; set; }
        public decimal Price { get; set; }
        public DateTime Duration { get; set; }
        public int DifficultyId { get; set; }
        public int DestinationId { get; set; }
        public byte GroupSize { get; set; }
        public float RoadHeight { get; set; }
        public int ReviewCount { get; set; }
        public float Star { get; set; }
        public string SubTitle { get; set; }
        public string Description { get; set; }
        public IQueryable<int> FacilityIds { get; set; }
        public IQueryable<int> FeatureIds { get; set; }
        public IQueryable<IFormFile> Images { get; set; }

        // Relation Fileds
        public IQueryable<Facility> Facilities { get; set; }
        public IQueryable<Destination> Destinations { get; set; }
        public IQueryable<Difficulty> Difficulties { get; set; }
        public IQueryable<Feature> Features { get; set; }
    }

    public class CreateTrekkingVMValidator : AbstractValidator<CreateTrekkingVM>
    {
        public CreateTrekkingVMValidator()
        {
            RuleFor(x => x.Title)
              .NotEmpty()
              .WithMessage("You must filled the input Title.")
              .MinimumLength(5)
              .MaximumLength(50)
              .WithMessage("Title's length between 5-50 character.");
            RuleFor(x => x.SubTitle)
                .NotEmpty()
                .WithMessage("You must filled the input Subtitle.")
                .MinimumLength(5)
                .MaximumLength(70)
                .WithMessage("Username's length between 5-70 character.");
            RuleFor(x => x.Description)
                .NotEmpty()
                .WithMessage("You must filled the input Description.");

            RuleFor(x => x.Images)
                .NotEmpty()
                .WithMessage("You must filled the input Images.")
                .Must(x => x.Any(x => FileManager.CheckFile(x.File) == true))
                .WithMessage("File type must be image and lower than 10 MB.");

            RuleFor(x => x.Price)
                .NotEmpty()
                .WithMessage("You must filled the input Price.");

            RuleFor(x => x.Duration)
               .NotEmpty()
               .WithMessage("You must filled the input Duration.");

            RuleFor(x => x.DifficultyId)
               .NotEmpty()
               .WithMessage("You must filled the input Difficulty.");

            RuleFor(x => x.DestinationId)
                .NotEmpty()
                .WithMessage("You must filled the input Destination.");

            RuleFor(x => x.GroupSize)
                .NotEmpty()
                .WithMessage("You must filled the input Group Size.")
                .Must(x => x <= 30 || x >= 2)
                .WithMessage("File type must be between 2 and 30.");

            RuleFor(x => x.ReviewCount)
                .NotEmpty()
                .WithMessage("You must filled the input Review Count.");

            RuleFor(x => x.Star)
                .NotEmpty()
                .WithMessage("You must filled the input Star.")
                .Must(x => x <= 5 || x >= 1)
                .WithMessage("File type must be between 0 and 5.");

            RuleFor(x => x.FacilityIds)
                .NotEmpty()
                .WithMessage("You must filled the input Facility.");

            RuleFor(x => x.FeatureIds)
                .NotEmpty()
                .WithMessage("You must filled the input Feature.");
        }
    }


}
