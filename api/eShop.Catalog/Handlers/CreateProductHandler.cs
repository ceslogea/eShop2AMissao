using eShop.Catalog.Domain.Services.Interface;
using eShop.Common.Commands;
using eShop.Common.Commands.Product;
using eShop.Common.Events.Product;
using eShop.Common.Exceptions;
using eShop.Common.Mediator.Result;
using MediatR;
using Microsoft.Extensions.Logging;
using RawRabbit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace eShop.Catalog.Handlers
{
    public class CreateProductHandler : IRequestHandler<CreateProduct, MediatorResult>
    {
        private readonly ILogger _logger;
        private readonly IMediator _mediator;
        //private readonly IProductService _productService;

        public CreateProductHandler(IMediator mediator, ILogger<CreateProductHandler> logger)
        {
            _mediator = mediator;
            //_productService = activityService;
            _logger = logger;
        }

        public async Task<MediatorResult> Handle(CreateProduct command, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Creating product: '{command.Id}' for user: '{command.UserId}'.");
            try
            {
                //await _productService.AddAsync(command);
                var productCreated = new ProductCreated(command.Id, command.Name, command.Description, command.Price, command.Image, command.CreatedAt);
                _logger.LogInformation($"Product: '{command.Id}' was created for user: '{command.UserId}'.");
                await _mediator.Publish(productCreated);
                _logger.LogInformation($"Product published: '{command.Id}' was created for user: '{command.UserId}'.");
            }
            catch (EshopException ex)
            {
                _logger.LogError(ex, ex.Message);
                //await _busClient.PublishAsync(new CreateProductRejected(command.Id, ex.Message, ex.Code));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                //await _busClient.PublishAsync(new CreateProductRejected(command.Id, ex.Message, "error"));
            }
            return await Task.FromResult(MediatorResult.Ok);
        }

    }
}
