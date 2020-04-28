using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eShop.Common.Commands.Product;
using eShop.Common.Mediator;
using eShop.Common.Mediator.Result;
using eShop.Gateway.Query;
using MediatR;
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

        private readonly IMediator _mediator;

        public ProductController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }


        //[HttpGet("{id}")]
        //public async Task<IActionResult> Get(Guid id)
        //{
        //    var product = await _mediator.Send(new GetProductDetailHandler(id));
        //    if (product == null) {
        //        return NotFound();
        //    }
        //    return Ok(product);
        //}


        //[HttpGet("")]
        //public async Task<IActionResult> Get()
        //{
        //    // TODO create handlers
        //    // TODO create Query
        //    return Ok();
        //}

        //[HttpGet("{id}")]
        //public async Task<IActionResult> Get(Guid id)
        //{
        //    // TODO create handlers
        //    // TODO create Query
        //    return Ok();
        //}

        [HttpPost("")]
        public async Task<IActionResult> Post([FromBody]CreateProduct command)
        {
            // TODO trak user
            //command.UserId = Guid.Parse(User.Identity.Name);
            
            command.Id = Guid.NewGuid();
            command.CreatedAt = DateTime.UtcNow;
            MediatorResult result = await _mediator.Send(command);
            // TODO usar Polly client 
            // TODO Criar Sender/Responsers

            return Accepted($"products/{command.Id}");
        }

    }

}