using Confluent.Kafka;
using eShop.Common.Events;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace eShop.Common.HostedServices
{
    public class EventHostedService<TEvent> : BackgroundService where TEvent : IEvent
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly string _topicName;
        private readonly ConsumerConfig _consumerConfig;
        private readonly IConsumer<string, string> _consumer;

        public EventHostedService(IServiceProvider serviceProvider, string topic)
        {
            _serviceProvider = serviceProvider;
            _topicName = topic;
            _consumerConfig = (ConsumerConfig)_serviceProvider.GetRequiredService(typeof(ConsumerConfig));
            _consumer = new ConsumerBuilder<string, string>(_consumerConfig).Build();
            _consumer.Subscribe(_topicName);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Console.WriteLine($"Service Started consuming Kafka Topic {_topicName}");

            while (!stoppingToken.IsCancellationRequested)
            {
                var consumeResult = _consumer.Consume(stoppingToken);
                if (consumeResult.Message != null && !string.IsNullOrEmpty(consumeResult.Message.Value))
                {
                    IEventHandler<TEvent> service = _serviceProvider.GetRequiredService<IEventHandler<TEvent>>();
                    TEvent processEntry = JsonConvert.DeserializeObject<TEvent>(consumeResult.Message.Value);
                    await service.HandleAsync(processEntry);
                }
            }
        }
    }
}
