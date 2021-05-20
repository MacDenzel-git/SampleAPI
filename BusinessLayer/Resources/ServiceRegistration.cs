using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Services;


namespace BusinessLogicLayer.Resources
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddRepositories(this IServiceCollection serviceDescriptors)
        {
            return null;
        }

        public static IServiceCollection AddDomainServices(this IServiceCollection services)
        {
            return null;
        }
    }
}
