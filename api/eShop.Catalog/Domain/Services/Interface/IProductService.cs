using System;
using System.Threading.Tasks;
using eShop.Catalog.Domain.Entity;

namespace eShop.Catalog.Domain.Services.Interface
{
    public interface IProductService
    {
        Task<Guid> AddAsync(AddProductModel product);
    }
}
