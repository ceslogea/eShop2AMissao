using System;
using System.Threading.Tasks;
using eShop.Basket.Domain.Entity;

namespace eShop.Basket.Domain.Repository.Interface
{
    public interface IProductRepository
    {
        Task<Product> GetAsync(Guid id);
        Task AddAsync(Product product);
    }
}