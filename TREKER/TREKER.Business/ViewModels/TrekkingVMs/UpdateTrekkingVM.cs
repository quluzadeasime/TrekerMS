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

namespace TREKER.Business.ViewModels.TrekkingVMs
{
    public class UpdateTrekkingVM : BaseEntityVm
    {
        public string? Title { get; set; }
        public decimal? Price { get; set; }
        public DateTime? Duration { get; set; }
        public int? DifficultyId { get; set; }
        public int? DestinationId { get; set; }
        public byte? GroupSize { get; set; }
        public float? RoadHeight { get; set; }
        public int? ReviewCount { get; set; }
        public float? Star { get; set; }
        public string? SubTitle { get; set; }
        public string? Description { get; set; }
        public IQueryable<int>? FacilityIds { get; set; }
        public IQueryable<int>? FeatureIds { get; set; }
        public IQueryable<IFormFile>? Images { get; set; }

        // Relation Fileds
        public IQueryable<TrekkingImage> OldImages { get; set; }
        public IQueryable<Facility> Facilities { get; set; }
        public IQueryable<Feature> Features { get; set; }
        public IQueryable<Difficulty> Difficulties { get; set; }
        public IQueryable<Destination> Destinations { get; set; }
        public IQueryable<int>? ViewImageIds { get; set; }
    }

    public class UpdateTrekkingVMValidator : AbstractValidator<UpdateTrekkingVM>
    {
        public UpdateTrekkingVMValidator()
        {
            RuleFor(x => x.Title)
              .MinimumLength(5)
              .MaximumLength(50)
              .WithMessage("Title's length between 5-50 character.");

            RuleFor(x => x.SubTitle)
                .MinimumLength(5)
                .MaximumLength(70)
                .WithMessage("Username's length between 5-70 character.");

            RuleFor(x => x.Images)
                .Must(x => x.Any(x => FileManager.CheckFile(x.File) == true))
                .WithMessage("File type must be image and lower than 10 MB.");

            RuleFor(x => x.GroupSize)
                 .Must(x => x <= 30 || x >= 2)
                 .WithMessage("File type must be between 2 and 30.");

            RuleFor(x => x.Star)
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
