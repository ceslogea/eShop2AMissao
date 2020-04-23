using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eShop.Common.Commands.Product;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RawRabbit;

namespace eShop.Gateway.Controllers
{

    [Route("api/product")]
    [ApiController]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ProductController : ControllerBase
    {
        private readonly IBusClient _busClient;

        public ProductController(IBusClient busClient)
        {
            _busClient = busClient;
        }


        [HttpGet("")]
        public async Task<IActionResult> Get()
        {
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            return Ok();
        }

        [HttpPost("")]
        public async Task<IActionResult> Post([FromBody]CreateProduct command)
        {
            // trak user
            //command.UserId = Guid.Parse(User.Identity.Name);
            
            command.Id = Guid.NewGuid();
            command.CreatedAt = DateTime.UtcNow;
            await _busClient.PublishAsync(command);
            
            // await process from bus
            //var entry = await Get(command.Id) as Product;

            return Accepted($"products/{command.Id}");
        }

    }

}