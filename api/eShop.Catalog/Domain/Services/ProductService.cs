using eShop.Catalog.Domain.Models;
using eShop.Catalog.Domain.Services.Interface;
using eShop.Common.Commands.Product;
using System.Threading.Tasks;

namespace eShop.Catalog.Domain.Services
{
    public class ProductService : IProductService
    {
        public Task<Product> AddAsync(CreateProduct command)
        {
            return Task.FromResult(new Product());
        }
    }
}
