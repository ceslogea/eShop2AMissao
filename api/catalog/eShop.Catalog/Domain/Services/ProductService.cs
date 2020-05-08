using CatalogApi;
using eShop.Catalog.Domain.Entity;
using eShop.Catalog.Domain.Repository.Interface;
using eShop.Catalog.Domain.Services.Interface;
using eShop.Common.Events.Product;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using RawRabbit;
using System;
using System.Threading.Tasks;
using static CatalogApi.Catalog;
using static eShop.Common.Kafka.Producer;
using static eShop.Common.Kafka.Consumer;
using eShop.Common.Kafka;

namespace eShop.Catalog.Domain.Services
{
    public class ProductService : CatalogBase, IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IBusClient _busClient;
        private readonly IKafkaProducer _kafkaProducerService;
        private readonly ILogger _logger;

        public ProductService(IProductRepository productRepository
                            , IBusClient busClient
                            , IKafkaProducer kafkaProducerService
                            , ILogger<ProductService> logger)
        {
            _productRepository = productRepository;
            _busClient = busClient;
            _kafkaProducerService = kafkaProducerService;
            _logger = logger;
        }

        public override async Task<CatalogItemResponse> GetItemById(CatalogItemRequest request, ServerCallContext context)
        {
            try
            {
                Console.WriteLine("CatalogService GetItemById: " + request.ToString());
                var product = await _productRepository.GetAsync(new Guid(request.Id));
                if (product == null) return new CatalogItemResponse();
                return new CatalogItemResponse()
                {
                    Id = product.Id.ToString(),
                    Description = product.Description,
                    Image = product.Image,
                    Name = product.Name,
                    Price = product.Price,
                };
            }
            catch (System.Exception)
            {
                throw;
            }
        }


        public override async Task<CatalogItemResponse> AddAsync(AddProductRequest productModel, ServerCallContext context)
        {
            try
            {
                // TODO como implementar Notifications ou algo parecido no GRPC usando Context
                // if(!productModel.IsValid()) return new CatalogItemResponse();
                var product = Product.NewProduct(productModel);
                await AddProduct(product);
                await PublishProductKafka(product);
                await PublishProductRabbitMQ(product);
                return await GetItemById(new CatalogItemRequest() { Id = product.Id.ToString() }, context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                // TODO como implementar Notifications ou algo parecido no GRPC usando Context
                // productModel.AddNotification(ex);
                return new CatalogItemResponse();
            }
        }

        private async Task AddProduct(Product product)
        {
            await _productRepository.AddAsync(product);
            _logger.LogInformation($"Product: '{product.Id}' was created for user: '{product.UserId}'.");
        }

        private async Task PublishProductRabbitMQ(Product product)
        {
            await _busClient.PublishAsync(new ProductCreated(
               product.Id
               , product.Name
               , product.Description
               , product.Price
               , product.Image
               , product.CreatedAt
               ));
        }

        private async Task PublishProductKafka(Product product)
        {
            await _kafkaProducerService.PublishAsync(new ProductCreated(
               product.Id
               , product.Name
               , product.Description
               , product.Price
               , product.Image
               , product.CreatedAt
               ));
        }
    }
}
