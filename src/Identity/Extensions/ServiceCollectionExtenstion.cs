using AutoMapper;
using Identity.Application.Config;
using Identity.Application.Repository;
using Identity.Application.Services;
using Identity.Application.Services.Interfaces;
using Identity.Core.Repository;
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

        public static IServiceCollection AddIdentityRepositories(this IServiceCollection services)
        {
            services.AddTransient<IIdentityRepository, IdentityRepository>();
            services.AddTransient<ITokenRepository, TokenRepository>();

            return services;
        }

        public static IServiceCollection AddIdentityServices(this IServiceCollection services)
        {
            services.AddTransient<IEncrypter, Encrypter>();
            
            services.AddTransient<ITokenService, TokenService>();
            services.AddTransient<IIdentityService, IdentityService>();

            return services;
        }
    }
}