using eShop.Catalog.Domain.Models;
using eShop.Common.Commands.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eShop.Catalog.Domain.Services.Interface
{
    public interface IProductService
    {
        Task<Product> AddAsync(CreateProduct command);
    }
}
