using AutoMapper;
using Identity.Application.Config;
using Identity.Application.Services;
using Identity.Application.Services.Interfaces;
using Identity.Controllers.Filters;
using Identity.Core.Repository;
using Identity.Persistence.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace Identity.Extensions
{
    public static class ServiceCollectionExtenstion
    {
        public static IServiceCollection AddAutoMapper(this IServiceCollection services)
        {
            var mappingConfig = new MapperConfiguration(cfg => { cfg.AddProfile(new IdentityAutoMapperConfig()); });
            var mapper = mappingConfig.CreateMapper();

            services.AddSingleton(mapper);

            return services;
        }

        public static IServiceCollection AddFilters(this IServiceCollection services)
        {
            services.AddScoped<ContentTypeFilter>();
            services.AddScoped<ValidateModelAttribute>();
            return services;
        }
        
        public static IServiceCollection AddIdentityRepositories(this IServiceCollection services)
        {
            services.AddScoped<IIdentityRepository, IdentityRepository>();
            services.AddScoped<ITokenRepository, TokenRepository>();

            return services;
        }

        public static IServiceCollection AddIdentityServices(this IServiceCollection services)
        {
            services.AddScoped<IEncrypter, Encrypter>();
            
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IIdentityService, IdentityService>();

            return services;
        }
    }
}