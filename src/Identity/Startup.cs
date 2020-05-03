using AutoMapper;
using Identity.Application.Repository;
using Identity.Application.Services;
using Identity.Application.Services.Interfaces;
using Identity.Application.Settings;
using Identity.Controllers.Filters;
using Identity.Core.Models;
using Identity.Core.Repository;
using Identity.Extensions;
using Identity.Persistence.Cache;
using Identity.Persistence.Context;
using Identity.Persistence.Extensions;
using Identity.Persistence.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using MemoryCache = Identity.Persistence.Cache.MemoryCache;
    
namespace Identity
{
    public class Startup
    {
        private readonly string _projectName;
        private IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            _projectName = Configuration.GetSection("Project").GetValue<string>("Name");
        }
        
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(Configuration);

            services
                .Configure<CacheSettings>(
                    Configuration.GetSection(nameof(CacheSettings)))
                .Configure<SecuritySettings>(
                    Configuration.GetSection(nameof(SecuritySettings)))
                .Configure<DatabaseConfig>(
                    Configuration.GetSection(nameof(DatabaseConfig)));
            
            services
                .AddSingleton(sp =>
                    sp.GetRequiredService<IOptions<CacheSettings>>().Value)
                .AddSingleton(sp =>
                    sp.GetRequiredService<IOptions<SecuritySettings>>().Value)
                .AddSingleton(sp =>
                    sp.GetRequiredService<IOptions<DatabaseConfig>>().Value);

            services
                .AddMemoryCache()
                .AddCustomContext(Configuration, _projectName)
                .AddSingleton<ICache, MemoryCache>()
                .AddSingleton<IPasswordHasher<User>, PasswordHasher<User>>()
                .AddAutoMapper()
                .AddIdentityRepositories()
                .AddIdentityServices()
                .AddSwaggerGen(
                    c => { c.SwaggerDoc("v1", new OpenApiInfo {Title = "HomeBackend", Version = "v1"}); })
                .AddCors(options =>
                {
                    options.AddPolicy("AllowAll",
                        builder =>
                        {
                            builder
                                .AllowAnyOrigin()
                                .AllowAnyMethod()
                                .AllowAnyHeader();
                            // .AllowCredentials(); //TODO: turn on when add auth!
                        });
                })
                .AddFilters()
                .AddControllers(options =>
                {
                    options.RespectBrowserAcceptHeader = true;
                    options.ReturnHttpNotAcceptable = true;
                    options.Filters.Add(typeof(ContentTypeFilter));
                    options.Filters.Add(typeof(ValidateModelAttribute));
                });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "Identity"); });
            }
            
            app
                .UseMigrationIdentity()
                .UseHttpsRedirection()
                .UseRouting()
                .UseCustomExceptionHandler()
                .UseCors("AllowAll")
                .UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}