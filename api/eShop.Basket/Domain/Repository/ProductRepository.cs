using System;
using System.Threading.Tasks;
using eShop.Basket.Domain.Entity;
using eShop.Basket.Domain.Repository.Interface;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace eShop.Basket.Domain.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly IMongoDatabase _database;

        public ProductRepository(IMongoDatabase database)
        {
            _database = database;
        }

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