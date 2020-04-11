using System.Net.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;

namespace Identity.EndToEnd.Controllers
{
    public abstract class ControllerTestsBase
    {
        protected readonly HttpClient Client;
        protected readonly TestServer Server;

        protected ControllerTestsBase()
        {
            Server = new TestServer(new WebHostBuilder()
                .UseStartup<Startup>());
            Client = Server.CreateClient();
        }
    }
}