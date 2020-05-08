using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eShop.Common.Events.Product;
using eShop.Common.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace eShop.Basket
{
    public class Program
    {
        public static void Main(string[] args)
        {
             ServiceHost.Create<Startup>(args)
                .UseRabbitMq()
                .SubscribeToEvent<ProductCreated>()
                .Build()
                .Run();
            // CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
