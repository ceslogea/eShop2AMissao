using eShop.Catalog.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eShop.Catalog.Domain.Repository.Interface
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> BrowseAsync();
        Task<Product> GetAsync(Guid id);
        Task AddAsync(Product product);
    }
}
