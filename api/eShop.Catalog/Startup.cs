using System;
using System.IO;
using System.Reflection;
using eShop.Catalog.Domain.Repository;
using eShop.Catalog.Domain.Repository.Interface;
using eShop.Catalog.Domain.Services;
using eShop.Catalog.Domain.Services.Interface;
using eShop.Common.Auth;
using eShop.Common.Mongo;
using eShop.Common.RabbitMq;
using eShop.Common.Swagger;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

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
            services.AddGrpc();    
            services.AddJwt(Configuration);
            services.AddSwagger("Products", "v1");
            services.AddMongoDB(Configuration);
            services.AddRabbitMq(Configuration);
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<IProductRepository, ProductRepository>();
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
                endpoints.MapGet("/_proto/", async ctx =>
                {
                    ctx.Response.ContentType = "text/plain";
                    using var fs = new FileStream(Path.Combine(env.ContentRootPath, "Proto", "product.proto"), FileMode.Open, FileAccess.Read);
                    using var sr = new StreamReader(fs);
                    while (!sr.EndOfStream)
                    {
                        var line = await sr.ReadLineAsync();
                        if (line != "/* >>" || line != "<< */")
                        {
                            await ctx.Response.WriteAsync(Environment.NewLine + line);
                        }
                    }
                });
                endpoints.MapGrpcService<ProductService>();
            });
        }
    }
}
