using System;
using System.Threading.Tasks;
using eShop.Basket.Domain.Entity;
using eShop.Basket.Domain.Repository.Interface;
using eShop.Common.Mongo;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace eShop.Basket.Domain.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly IMongoDatabase _database;
        private readonly IMongoClient _client;
        
        public ProductRepository(IMongoDatabase database)
        {
           _database = database;
        }

        // public ProductRepository(IServiceProvider serviceProvider, IMongoClient client)
        // {
        //     _client = client;
        //     _database = _client.GetDatabase(serviceProvider.GetService<IOptions<MongoOptions>>().Value.Database);
        // }

        public async Task<Product> GetAsync(Guid id)
        => await Collection
            .AsQueryable()
            .FirstOrDefaultAsync(x => x.Id == id);

        public async Task AddAsync(Product product)
            => await Collection.InsertOneAsync(product);


        private IMongoCollection<Product> Collection
            => _database.GetCollection<Product>("Products");
        
    }
}