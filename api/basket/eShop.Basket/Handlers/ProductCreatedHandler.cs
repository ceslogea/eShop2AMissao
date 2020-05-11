using System;
using System.Threading.Tasks;
using eShop.Basket.Domain.Entity;
using eShop.Basket.Domain.Repository.Interface;
using eShop.Common.Events;
using eShop.Common.Events.Product;
using eShop.Common.Mongo;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace eShop.Basket.Handlers
{
    public class ProductCreatedHandler : IEventHandler<ProductCreated>
    {
        private readonly ILogger _logger;
        private readonly IProductRepository _productRepository;

        public ProductCreatedHandler(ILogger<ProductCreatedHandler> logger, IProductRepository productRepository)
        {
            _logger = logger;
            _productRepository = productRepository;
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