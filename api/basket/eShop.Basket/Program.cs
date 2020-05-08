using eShop.Common.Events.Product;
using eShop.Common.Services;

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
        }
    }
}
