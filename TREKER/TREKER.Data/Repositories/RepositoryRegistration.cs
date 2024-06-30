using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TREKER.DAL.Repositories.Implementations;
using TREKER.DAL.Repositories.Interfaces;

namespace TREKER.DAL.Repositories
{
    public static class RepositoryRegistration
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<ITeamMemberRepository, TeamMemberRepository>();
            services.AddScoped<ISettingRepository, SettingRepository>();
            services.AddScoped<IBlogRepository, BlogRepository>();
            services.AddScoped<IDestinationRepository, DestinationRepository>();
            services.AddScoped<IDifficultyRepository, DifficultyRepository>();
            services.AddScoped<IFacilityRepository, FacilityRepository>();
            services.AddScoped<IFeatureRepository, FeatureRepository>();
            services.AddScoped<IRegionRepository, RegionRepository>();
            services.AddScoped<ITrekkingRepository, TrekkingRepository>();
            services.AddScoped<IDayRepository, DayRepository>();
            services.AddScoped<IContactRepository, ContactRepository>();
            services.AddScoped<ITrekkingImageRepository, TrekkingImageRepository>();
            services.AddScoped<IBlogImageRepository, BlogImageRepository>();
            services.AddScoped<ITrekkingFeatureRepository, TrekkingFeatureRepository>();
            services.AddScoped<ITrekkingFacilityRepository, TrekkingFacilityRepository>();
            services.AddScoped<IAppointmentRepository, AppointmentRepository>();
        }
    }
}
