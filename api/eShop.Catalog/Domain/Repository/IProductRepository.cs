using eShop.Catalog.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eShop.Catalog.Domain.Repository
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> BrowseAsync();
        Task<Product> GetAsync(Guid id);
    }
}
