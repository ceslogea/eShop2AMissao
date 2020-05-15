using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System.Net.Http;

namespace eShop.Gateway.Tests.Integration
{
    public class BaseServerHttpClientTests
    {
        protected readonly TestServer _server;
        protected readonly TestServer _serverCatalog;
        protected readonly HttpClient _client;

        protected BaseServerHttpClientTests()
        {
            _server = new TestServer(WebHost.CreateDefaultBuilder()
                            .UseStartup<eShop.Gateway.Startup>());
            _serverCatalog = new TestServer(WebHost.CreateDefaultBuilder()
                            .UseUrls("https://localhost:5005/")
                            .UseStartup<eShop.Catalog.Startup>());
            _client = _server.CreateClient();
        }
    }
}