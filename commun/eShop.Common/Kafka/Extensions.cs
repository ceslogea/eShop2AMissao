using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace eShop.Common.Kafka
{
    public static class Extensions
    {
        /// <summary>
        /// It binds producer config sections.
        /// </summary>
        public static void AddKafkaProducer(this IServiceCollection services, IConfiguration configuration)
        {
            var producerConfig = new ProducerConfig();
            configuration.Bind("producer", producerConfig);
            services.AddSingleton(producerConfig);
            services.AddSingleton<IKafkaProducer>(_ => new KafkaProducer(_.GetRequiredService<ProducerConfig>()));
        }
    }
}
