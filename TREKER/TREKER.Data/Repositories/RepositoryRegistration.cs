using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TREKER.DAL.Repositories
{
    public static class RepositoryRegistration
    {
        public static void AddRepository(this IServiceCollection services)
        {
            services.AddScoped<ITeamMemberRepository>
        }
    }
}
