using Confluent.Kafka;
using eShop.Common.Events;
using eShop.Common.HostedServices;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace eShop.Common.Kafka
{
    public static class Extensions
    {
        /// <summary>
        /// Binds producer config section.
        /// </summary>
        public static void AddKafkaProducer(this IServiceCollection services, IConfiguration configuration)
        {
            var producerConfig = new ProducerConfig();
            configuration.Bind("producer", producerConfig);
            services.AddSingleton(producerConfig);
            services.AddSingleton<IKafkaProducer>(_ => new KafkaProducer(_.GetRequiredService<ProducerConfig>()));
        }

        /// <summary>
        /// Binds consumer config section.
        /// </summary>
        public static void AddKafkaConsumerConfig(this IServiceCollection services, IConfiguration configuration)
        {
            var consumerConfig = new ConsumerConfig();
            configuration.Bind("consumer", consumerConfig);
            services.AddSingleton(consumerConfig);
        }


        /// <summary>
        /// Binds consumer config section.
        /// </summary>
        public static void AddKafkaConsumerEventHandlers<TEvent>(this IServiceCollection services, IConfiguration configuration) where TEvent : IEvent
        {
            services.AddSingleton<IHostedService, EventHostedService<TEvent>>(_ => new EventHostedService<TEvent>(_));
        }

    }
}
