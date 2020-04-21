using System.Text;
using HomeBackend.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace HomeBackend.Extensions
{
    public static class IdentityExtensions
    {
        public static IServiceCollection AddCustomIdentity(this IServiceCollection services)
        {
            var config = services.BuildServiceProvider().GetRequiredService<IdentitySettings>();
            
            var key = Encoding.ASCII.GetBytes(config.Secret);
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
                        ValidateAudience = true,
                        ValidAudience = config.Audience,
                        RequireExpirationTime = true
                    };
                });
            return services;
        }

        public static IApplicationBuilder UseCustomIdentity(this IApplicationBuilder app)
        {
            app.UseAuthorization();
            app.UseAuthentication();
            
            return app;
        }
    }
}