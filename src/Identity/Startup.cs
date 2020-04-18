using AutoMapper;
using Identity.Application.Repository;
using Identity.Application.Services;
using Identity.Application.Services.Interfaces;
using Identity.Application.Settings;
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
        private readonly string _persistenceProjectName;
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            _projectName = Configuration.GetSection("Project").GetValue<string>("Name");
            _persistenceProjectName = Configuration.GetSection("Subprojects").GetValue<string>("Persistence");
        }
        
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            InitializeConfigurations(services);
            RegisterConfigurations(services);
            RegisterSwagger(services);
            RegisterContext(services);
            RegisterServices(services);
            ConfigureControllers(services);
            ConfigureCors(services);
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
                .UseCustomExceptionHandler();


            
            // app.UseAuthorization();
            app.UseCors("AllowAll")
                .UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }

        private static void ConfigureControllers(IServiceCollection services)
        {
            services.AddControllers(options =>
            {
                options.RespectBrowserAcceptHeader = false;
                options.ReturnHttpNotAcceptable = false;
            });
        }

        private static void ConfigureCors(IServiceCollection services)
        {
            services.AddCors(options =>
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
            });
        }

        private void RegisterContext(IServiceCollection services)
        {
            services.AddDbContext<IdentityContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("IdentityDatabase"), x
                    => x.MigrationsAssembly(_projectName)));
        }

        private static void RegisterSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(
                c => { c.SwaggerDoc("v1", new OpenApiInfo {Title = "HomeBackend", Version = "v1"}); });
        }

        private static void RegisterConfigurations(IServiceCollection services)
        {
            services.AddSingleton(sp =>
                sp.GetRequiredService<IOptions<CacheSettings>>().Value);
            services.AddSingleton(sp =>
                sp.GetRequiredService<IOptions<SecuritySettings>>().Value);
            services.AddSingleton(sp =>
                sp.GetRequiredService<IOptions<DatabaseConfig>>().Value);
        }

        private void InitializeConfigurations(IServiceCollection services)
        {
            services.AddSingleton(Configuration);

            services.Configure<CacheSettings>(
                Configuration.GetSection(nameof(CacheSettings)));
            services.Configure<SecuritySettings>(
                Configuration.GetSection(nameof(SecuritySettings)));
            services.Configure<DatabaseConfig>(
                Configuration.GetSection(nameof(DatabaseConfig)));
        }

        private void RegisterServices(IServiceCollection services)
        {
            services.AddMemoryCache();
            
            RegisterIdentity(services);
            RegisterPersistence(services);
        }

        private void RegisterPersistence(IServiceCollection services)
        {
            services.AddSingleton<ICache, MemoryCache>();
            services
                .AddSingleton<IPasswordHasher<User>, PasswordHasher<User>>();
        }

        private void RegisterIdentity(IServiceCollection services)
        {
            services
                .AddAutoMapper()
                .AddIdentityRepositories()
                .AddIdentityServices();
        }
        
    }
}