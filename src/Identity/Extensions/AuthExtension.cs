using System;
using System.Text;
using Identity.Application.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Identity.Extensions
{
    public static class AuthExtension
    {
        public static IServiceCollection AddCustomIdentity(this IServiceCollection services)
        {
            var config = services.BuildServiceProvider().GetRequiredService<SecuritySettings>();
            
            var key = Encoding.ASCII.GetBytes(config.Key);
            var keySinging = new SymmetricSecurityKey(key);
            
            services.AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = keySinging,
                        ValidateIssuer = true,
                        ValidIssuer = config.Issuer,
                        ValidateAudience = false,
                        ValidAudience = config.Audience,
                        RequireExpirationTime = true,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero
                    };
                });
            return services;
        }

        public static IApplicationBuilder UseCustomIdentity(this IApplicationBuilder app)
        {
            app.UseAuthentication();
            app.UseAuthorization();

            return app;
        }
    }
}