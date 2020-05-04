using System.Threading.Tasks;
using eShop.Basket.Domain.Entity;
using eShop.Basket.Domain.Repository.Interface;
using eShop.Common.Events;
using eShop.Common.Events.Product;
using Microsoft.Extensions.Logging;

namespace eShop.Basket.Handlers
{
    public class ProductCreatedHandler : IEventHandler<ProductCreated>
    {
        private readonly IProductRepository _productRepository;
        private readonly ILogger _logger;

        public ProductCreatedHandler(IProductRepository productRepository, ILogger<ProductCreatedHandler> logger)
        {
            _productRepository = productRepository;
            _logger = logger;
        }

        public async Task HandleAsync(ProductCreated @event)
        {
            await _productRepository.AddAsync(new Product
            {
                Id = @event.Id,
                // UserId = @event.UserId,
                Price = @event.Price,
                Image = @event.Image,
                Name = @event.Name,
                CreatedAt = @event.CreatedAt,
                Description = @event.Description
            });
            _logger.LogInformation($"Produto created: {@event.Name}, {@event.Id}");
        }
    }
}