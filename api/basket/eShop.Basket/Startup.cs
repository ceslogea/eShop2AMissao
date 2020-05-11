using Confluent.Kafka;
using eShop.Basket.Domain.Repository;
using eShop.Basket.Domain.Repository.Interface;
using eShop.Basket.Handlers;
using eShop.Common.Events;
using eShop.Common.Events.Product;
using eShop.Common.HostedServices;
using eShop.Common.Kafka;
using eShop.Common.Mongo;
using eShop.Common.RabbitMq;
using eShop.Common.Swagger;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace eShop.Basket
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
            services.AddRabbitMq(Configuration);
            services.AddMongoDBTransient(Configuration);
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddSwagger("Basket", "v1");
            services.AddKafkaConsumerConfig(Configuration);
            services.AddTransient<IEventHandler<ProductCreated>, ProductCreatedHandler>();
            services.AddKafkaConsumerEventHandlers<ProductCreated>("eShop.Catalog-ProductCreated");
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
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
