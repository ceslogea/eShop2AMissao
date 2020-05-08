using System;
using CatalogApi;

namespace eShop.Catalog.Domain.Entity
{
    public class Product
    {
        public Guid Id { get; private set; }
        public Guid UserId { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public double Price { get; private set; }
        public string Image { get; private set; }

        Product() { }

        private Product(Guid id, Guid userId, DateTime createdAt, string name, string description, double price, string image)
        {
            this.Id = id;
            this.UserId = userId;
            this.CreatedAt = createdAt;
            this.Name = name;
            this.Description = description;
            this.Price = price;
            this.Image = image;

        }

        public static Product NewProduct(AddProductRequest productModel)
        {
            return new Product(
                new Guid()
                , Guid.Empty
                , DateTime.Now
                , productModel.Name
                , productModel.Description
                , productModel.Price
                , productModel.Image
            );
        }
    }
}