using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Services;
using BusinessLogicLayer.Services.BranchServiceContainer;
using BusinessLogicLayer.Services.EventContainer;
using BusinessLogicLayer.Services.projectArmsService;
using BusinessLogicLayer.Services.QouteServiceContainer;
using BusinessLogicLayer.Services.SermonCategoryContainer;
using BusinessLogicLayer.Services.ResourceContainer;
using BusinessLogicLayer.Services.TeamServiceContainer;
using BusinessLogicLayer.Services.UtilsContainer;
using BusinessLogicLayer.Services.PositionsContainer;
using DataAccessLayer.GenericRepoSettings;
using DataAccessLayer.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogicLayer.Services.TestimonyServiceContainer;
using BusinessLogicLayer.Services.ResourceTypeContainer;
using Microsoft.AspNetCore.Authentication;
using BusinessLogicLayer.Services.AuthenticationService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using DataAccessLayer.Authentication.JSONWebToken;
using Microsoft.AspNetCore.Builder;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace projectWebAPI
{
    public static class Registration
    {
        public static IServiceCollection AddRepositories(this IServiceCollection service)
        {
              service.AddScoped<GenericRepository<ResourceCategory>>();
              service.AddScoped<GenericRepository<TeamMember>>();
              service.AddScoped<GenericRepository<Branch>>();
              service.AddScoped<GenericRepository<Event>>();
              service.AddScoped<GenericRepository<Qoute>>();
              service.AddScoped<GenericRepository<projectArm>>();
              service.AddScoped<GenericRepository<Testimony>>();
              service.AddScoped<GenericRepository<Position>>();
              service.AddScoped<GenericRepository<ProjectPartnershipPlatform>>();
              service.AddScoped<GenericRepository<ResourceType>>();
            return service.AddScoped<GenericRepository<DataAccessLayer.Models.Resource>>();
        }

        public static IServiceCollection AddServices(this IServiceCollection service)
        {
             service.AddScoped<IResourceCategoryService, ResourceCategoryService>();
             service.AddScoped<IResourceTypeService, ResourceTypeService>();
             service.AddScoped<IBranchService, BranchService>();
             service.AddScoped<ITeamService, TeamService>();
             service.AddScoped<IUtilService, UtilService>();
             service.AddScoped<IPositionService, PositionService>();
             service.AddScoped<IEventService, EventService>();
             service.AddScoped<ITestimonyService, TestimonyService>();
             service.AddScoped<IQouteService, QouteService>();
             service.AddScoped<IprojectArmService, projectArmService>();
             service.AddScoped<IAuthenticateUserService, AuthenticateUserService>();

            return service.AddScoped<IResourceService, ResourceService>();
        }

        public static IServiceCollection AddAuth(this IServiceCollection services, JwtSettings jwtSettings)
        {
            services
                .AddAuthorization()
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidIssuer = jwtSettings.Issuer,
                        ValidAudience = jwtSettings.Issuer,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret)),
                        ClockSkew = TimeSpan.Zero
                    };
                });

            return services;
        }

        
    }
}
