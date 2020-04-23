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

        public ProductController()
        {
        }


        [HttpGet("")]
        public async Task<IActionResult> Get()
        {
            // TODO create handlers
            // TODO create Query
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            // TODO create handlers
            // TODO create Query
            return Ok();
        }

        [HttpPost("")]
        public async Task<IActionResult> Post([FromBody]CreateProduct command)
        {
            // TODO trak user
            //command.UserId = Guid.Parse(User.Identity.Name);
            
            command.Id = Guid.NewGuid();
            command.CreatedAt = DateTime.UtcNow;

            // TODO usar Polly client 
            // TODO Criar Sender/Responsers

            return Accepted($"products/{command.Id}");
        }

    }

}