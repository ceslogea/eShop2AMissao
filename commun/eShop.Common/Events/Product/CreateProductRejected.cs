using System;
using System.Collections.Generic;
using System.Text;

namespace eShop.Common.Events.Product
{
    public class CreateProductRejected : IRejectedEvent
    {
        public Guid Id { get; }

        public string Reason { get; }

        public string Code { get; }

        protected CreateProductRejected()
        {
        }

        public CreateProductRejected(Guid id,
         string reason, string code)
        {
            Id = id;
            Reason = reason;
            Code = code;
        }

    }
}
