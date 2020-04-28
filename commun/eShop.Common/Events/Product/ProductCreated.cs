using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace eShop.Common.Events.Product
{
    public class ProductCreated : INotification, IEvent
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Price { get; set; }
        public string Image { get; set; }

        protected ProductCreated()
        {
        }

        public ProductCreated(Guid id, string name, string description, string price, string image ,DateTime createdAt)
        {
            Id = id;
            Name = name;
            Description = description;
            Price = price;
            Image = image;
            CreatedAt = createdAt;
        }
    }
}
