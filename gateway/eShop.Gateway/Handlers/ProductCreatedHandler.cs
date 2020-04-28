using eShop.Common.Events.Product;
using eShop.Gateway.Domain.Repository.Interface;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace eShop.Gateway.Handlers
{
    public class ProductCreatedHandler : INotificationHandler<ProductCreated>
    {
        private readonly IProductRepository _productRepository;
        private readonly ILogger _logger;

        public ProductCreatedHandler(IProductRepository productRepository, ILogger<ProductCreatedHandler> logger)
        {
            _productRepository = productRepository;
            _logger = logger;
        }

        public Task Handle(ProductCreated notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Product Created in gateway, product id: {notification.Id} from publisher");
            return Task.FromResult(0);
        }
    }
}
