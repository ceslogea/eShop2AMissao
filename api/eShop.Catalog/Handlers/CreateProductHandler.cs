using eShop.Common.Commands;
using eShop.Common.Commands.Product;
using eShop.Common.Events.Product;
using eShop.Common.Exceptions;
using Microsoft.Extensions.Logging;
using RawRabbit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eShop.Catalog.Handlers
{
    public class CreateProductHandler : ICommandHandler<CreateProduct>
    {
        private readonly ILogger _logger;
        private readonly IBusClient _busClient;
        private readonly IProductService _productService;

        public CreateProductHandler(IBusClient busClient,
            IProductService activityService,
            ILogger<CreateProductHandler> logger)
        {
            _busClient = busClient;
            _productService = activityService;
            _logger = logger;
        }

        public async Task HandleAsync(CreateProduct command)
        {
            _logger.LogInformation($"Creating activity: '{command.Id}' for user: '{command.UserId}'.");
            try
            {
                await _productService.AddAsync(command);
                await _busClient.PublishAsync(new ProductCreated(command.Id, command.Name, command.Description, command.Price, command.Image, command.CreatedAt));
                _logger.LogInformation($"Activity: '{command.Id}' was created for user: '{command.UserId}'.");

                return;
            }
            catch (EshopException ex)
            {
                _logger.LogError(ex, ex.Message);
                await _busClient.PublishAsync(new CreateProductRejected(command.Id, ex.Message, ex.Code));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                await _busClient.PublishAsync(new CreateProductRejected(command.Id, ex.Message, "error"));
            }
        }
    }
}
