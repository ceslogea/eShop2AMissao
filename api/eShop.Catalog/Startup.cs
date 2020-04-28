using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using eShop.Catalog.Domain.Services;
using eShop.Catalog.Domain.Services.Interface;
using eShop.Catalog.Handlers;
using eShop.Common.Auth;
using eShop.Common.Commands.Product;
using eShop.Common.Mediator;
using eShop.Common.Mediator.Result;
using eShop.Common.Mongo;
using eShop.Common.RabbitMq;
using eShop.Common.Swagger;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace eShop.Catalog
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddJwt(Configuration);
            services.AddSwagger("Products", "v1");
            services.AddMediatR(typeof(Startup).GetTypeInfo().Assembly);
            services.AddScoped<IRequestHandler<CreateProduct, MediatorResult>, CreateProductHandler>();

            services.AddScoped<IMediatorHandler, InMemoryBus>();

            services.AddTransient<IProductService, ProductService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.ConfigSwaggerServices("/swagger/v1/swagger.json", "Products V1");
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
