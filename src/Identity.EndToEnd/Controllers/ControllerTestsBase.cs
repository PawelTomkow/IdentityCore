using System;
using System.IO;
using System.Net.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;

namespace Identity.EndToEnd.Controllers
{
    public abstract class ControllerTestsBase
    {
        protected readonly HttpClient Client;
        protected readonly TestServer Server;

        private static IConfiguration Configuration { get; } = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", false, true)
            .AddJsonFile(
                $"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json",
                true)
            .Build();
        
        protected ControllerTestsBase()
        {
            Server = new TestServer(new WebHostBuilder()
                .UseStartup<Startup>()
                .UseConfiguration(Configuration));
            Client = Server.CreateClient();
        }
    }
}