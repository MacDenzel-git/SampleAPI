using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Services;
using PODL.Standard.Data.Services.RepositoryService;
using DataAccessLayer.GenericRepoSettings;
using DataAccessLayer.Models;
using BusinessLogicLayer.Services.TeamServiceContainer;

namespace BusinessLogicLayer.Resources
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddRepositories(this IServiceCollection serviceDescriptors)
        {
            return serviceDescriptors.AddScoped<IRepository<TeamMember>, GenericRepository<TeamMember>>();
        }

        public static IServiceCollection AddDomainServices(this IServiceCollection services)
        {
            return services.AddScoped<ITeamService, TeamService>();
        }
    }
}
