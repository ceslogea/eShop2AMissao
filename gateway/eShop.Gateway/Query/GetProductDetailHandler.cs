using eShop.Gateway.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eShop.Gateway.Query
{
    public class GetProductDetailHandler : IRequest<ProductModel>
    {
        //[JsonConstructor]
        public GetProductDetailHandler(Guid customerId)
        {
            CustomerId = customerId;
        }

        //[JsonProperty("id")]
        //[Required]
        public Guid CustomerId { get; set; }
    }
}
