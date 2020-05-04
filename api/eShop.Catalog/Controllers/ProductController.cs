using System.Threading.Tasks;
using eShop.Catalog.Domain;
using eShop.Catalog.Domain.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace eShop.Catalog.Controllers
{
    [Route("api/product")]
    [ApiController]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }


        [HttpPost]
        public async Task<IActionResult> Post(AddProductModel productModel)
        {
            var productCreated = await _productService.AddAsync(productModel);
            if(productCreated != null){
                return Ok(productCreated);
            }
            return BadRequest();
        }
    }
}