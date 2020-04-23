using System;
using System.Threading.Tasks;
using eShop.Catalog.Domain.Repository;
using eShop.Common.Commands.Product;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RawRabbit;

namespace eShop.Catalog.Controllers
{
    [Route("api/product")]
    [ApiController]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _repository;

        public ProductController(IProductRepository repository)
        {
            _repository = repository;
        }


        [HttpGet("")]
        public async Task<IActionResult> Get()
        {
            var products = await _repository
                .BrowseAsync();

            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var product = await _repository.GetAsync(id);
            if (product == null)
            {
                return NotFound();
            }
         
            return Ok(product);
        }
    }
}