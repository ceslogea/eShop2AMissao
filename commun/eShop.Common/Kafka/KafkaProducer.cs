using Confluent.Kafka;
using eShop.Common.Events;
using Newtonsoft.Json;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace eShop.Common.Kafka
{
    public class KafkaProducer : IKafkaProducer
    {
        private readonly ProducerConfig producerConfig;
        private readonly IProducer<Null, string> _producer;
        private static readonly Action<DeliveryReport<Null, string>> _handler = r =>
               Console.WriteLine(!r.Error.IsError
                   ? $"Delivered message to {r.Topic}, {r.TopicPartitionOffset}"
                   : $"Delivery Error: {r.Topic}, {r.Error.Reason}");

        public KafkaProducer(ProducerConfig producerConfig)
        {
            this.producerConfig = producerConfig;
            _producer = new ProducerBuilder<Null, string>(this.producerConfig).Build();
        }

        public async Task PublishAsync<TEvent>(TEvent @event) where TEvent : IEvent
        {
            var value = JsonConvert.SerializeObject(@event);
            var topic = GetTopicName<TEvent>();
            _producer.Produce(topic, new Message<Null, string> { Value = value }, _handler);
            Flush(TimeSpan.FromSeconds(10));
            await Task.CompletedTask;
        }

        public static string GetTopicName<T>()
            => $"{Assembly.GetEntryAssembly().GetName().Name}-{typeof(T).Name}";

        /// <summary>
        /// wait for up to X seconds for any inflight messages to be delivered.
        /// </summary>
        private void Flush(TimeSpan timeSpan) => _producer.Flush(timeSpan);
    }
}
