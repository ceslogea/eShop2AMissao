using System;

namespace eShop.Basket.Domain.Entity
{
    public class Product
    {
        public Guid Id { get; internal set; }
        public Guid UserId { get; internal set; }
        public DateTime CreatedAt { get; internal set; }
        public string Name { get; internal set; }
        public string Description { get; internal set; }
        public string Price { get; internal set; }
        public string Image { get; internal set; }
    }
}