using CatalogApi;
using eShop.Catalog.Domain.Entity;
using eShop.Catalog.Domain.Repository.Interface;
using eShop.Catalog.Domain.Services.Interface;
using eShop.Common.Events.Product;
using eShop.Common.Exceptions;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using RawRabbit;
using System;
using System.Threading.Tasks;
using static CatalogApi.Catalog;

namespace eShop.Catalog.Domain.Services
{
    public class ProductService : CatalogBase, IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IBusClient _busClient;
        private readonly ILogger _logger;

        public ProductService(IProductRepository productRepository
                            , IBusClient busClient
                            , ILogger<ProductService> logger)
        {
            _productRepository = productRepository;
            _busClient = busClient;
            _logger = logger;
        }

        public override Task<CatalogItemResponse> GetItemById(CatalogItemRequest request, ServerCallContext context)
        {
            try
            {
                Console.WriteLine("CatalogService GetItemById: " + request.ToString());
                return Task.FromResult(new CatalogItemResponse());
            }
            catch (System.Exception)
            {

                throw;
            }
        }

        public async Task<Guid> AddAsync(AddProductModel productModel)
        {
            try
            {
                productModel.IsValid();
                var product = Product.NewProduct(productModel);
                await _productRepository.AddAsync(product);
                _logger.LogInformation($"Product: '{product.Id}' was created for user: '{product.UserId}'.");
                
                await _busClient.PublishAsync(new ProductCreated(
                product.Id
                , product.Name
                , product.Description
                , product.Price
                , product.Image
                , product.CreatedAt
                ));

                _logger.LogInformation($"Product published: '{product.Id}' was created for user: '{product.UserId}'.");

                return product.Id;
            }
            catch (EshopException ex)
            {
                _logger.LogError(ex, ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
            
            return Guid.Empty;
        }
    }
}
