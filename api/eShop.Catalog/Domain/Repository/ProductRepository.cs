using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using eShop.Catalog.Domain.Entity;
using eShop.Catalog.Domain.Repository.Interface;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace eShop.Catalog.Domain.Repository
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

        public async Task<IEnumerable<Product>> BrowseAsync()
         => await Collection.AsQueryable().ToListAsync();

        private IMongoCollection<Product> Collection
            => _database.GetCollection<Product>("Products");
    }
}