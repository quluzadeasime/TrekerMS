using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TREKER.Business.Services.Abstractions;
using TREKER.Business.Services.Interfaces;

namespace TREKER.Business.Services
{
    public static class ServiceRegistration
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<ITeamMemberService, TeamMemberService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<ISendMessageService, SendMessageService>();
            services.AddScoped<ISettingService, SettingService>();
            services.AddScoped<IBlogService, BlogService>();
            services.AddScoped<IDestinationService, DestinationService>();
            services.AddScoped<IDifficultyService, DifficultyService>();
            services.AddScoped<IFacilityService, FacilityService>();
            services.AddScoped<IFeatureService, FeatureService>();
            services.AddScoped<IRegionService, RegionService>();
            services.AddScoped<ITrekkingService, TrekkingService>();
            services.AddScoped<IDayService, DayService>();
        }
    }
}
