using Confluent.Kafka;
using eShop.Common.Commands;
using eShop.Common.Events;
using eShop.Common.Kafka;
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
        private readonly IEventHandler<TEvent> _service;
        private readonly string _topicName;
        private readonly ConsumerConfig _consumerConfig;
        private readonly IConsumer<string, string> _consumer;

        public EventHostedService(IServiceProvider serviceProvider)
        {
            _service = (IEventHandler<TEvent>)serviceProvider.GetRequiredService(typeof(IEventHandler<TEvent>));
            _consumerConfig = (ConsumerConfig)serviceProvider.GetRequiredService(typeof(ConsumerConfig));
            _consumer = new ConsumerBuilder<string, string>(_consumerConfig).Build();
            _topicName = KafkaProducer.GetTopicName<TEvent>();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Console.WriteLine($"{_service.GetType().Name} Service Started consuming Kafka Topic {_topicName}");
            _consumer.Subscribe(_topicName);

            while (!stoppingToken.IsCancellationRequested)
            {
                var consumeResult = _consumer.Consume(stoppingToken);
                TEvent processEntry = JsonConvert.DeserializeObject<TEvent>(consumeResult.Message.Value);
                await _service.HandleAsync(processEntry);
            }
        }
    }
}
