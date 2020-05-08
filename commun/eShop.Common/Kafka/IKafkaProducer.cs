using eShop.Common.Events;
using System.Threading.Tasks;

namespace eShop.Common.Kafka
{
    public interface IKafkaProducer
    {
        Task PublishAsync<TEvent>(TEvent @event) where TEvent : IEvent;
    }
}
