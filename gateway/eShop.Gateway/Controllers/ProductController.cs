using System;
using System.Threading.Tasks;
using Grpc.Net.Client;
using Microsoft.AspNetCore.Mvc;
using CatalogApi;
using eShop.Gateway.Domain.Model;

namespace eShop.Gateway.Controllers
{

    [Route("api/product")]
    [ApiController]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ProductController : ControllerBase
    {

        [HttpGet]
        public async Task<IActionResult> Get(CatalogItemsRequest request)
        {
            using var channel = GrpcChannel.ForAddress("https://localhost:5005");
            var client = new Catalog.CatalogClient(channel);
            var reply = await client.GetItemsByIdsAsync(request);
            return Ok(reply);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            using var channel = GrpcChannel.ForAddress("https://localhost:5005");
            var client = new Catalog.CatalogClient(channel);
            var request = new CatalogItemRequest { Id = id.ToString() };
            var reply = await client.GetItemByIdAsync(request);
            return Accepted($"products/{reply}");
        }

        [HttpPost]
        public async Task<IActionResult> Post(AddProductModel productModel)
        {

            // TODO trak user
            //command.UserId = Guid.Parse(User.Identity.Name);

            using var channel = GrpcChannel.ForAddress("https://localhost:5005/api/product");
            var client = new Catalog.CatalogClient(channel);
            var request = new AddProductRequest { 
                Name = productModel.Name,
                Description = productModel.Description,
                Price = productModel.Price,
                Image = productModel.Image
            };
            var reply = await client.AddAsyncAsync(request);
            return Created($"products/{reply.Id}", reply);
        }

    }

}