using System;
using System.Threading.Tasks;
using Grpc.Net.Client;
using Microsoft.AspNetCore.Mvc;
using CatalogApi;

namespace eShop.Gateway.Controllers
{

    [Route("api/product")]
    [ApiController]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ProductController : ControllerBase
    {

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            // TODO trak user
            //command.UserId = Guid.Parse(User.Identity.Name);
            
            using var channel = GrpcChannel.ForAddress("https://localhost:5005/api/product");
            var client = new Catalog.CatalogClient(channel);
            var request = new CatalogItemRequest { Id = id };
            var reply = await client.GetItemByIdAsync(request);
            Console.WriteLine("Catalog: " + reply.ToString());
            return Accepted($"products/{reply}");
        }
       
        // [HttpPost]
        // public async Task<IActionResult> Post(AddProductModel productModel)
        // {
        //     using(HttpClient client = new HttpClient())
        //     {
        //         client.BaseAddress = new Uri("https://localhost:5005/api");
        //         HttpResponseMessage response = await client.PostAsJsonAsync(
        //         "api/product", productModel);
                
        //         response.EnsureSuccessStatusCode();
        //     }
        //     var productCreated = await _productService.AddAsync(productModel);
        //     if(productCreated != null){
        //         return Ok(productCreated);
        //     }
        //     return BadRequest();
        // }

    }

}