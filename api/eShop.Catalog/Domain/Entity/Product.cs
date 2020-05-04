using System;

namespace eShop.Catalog.Domain.Entity
{
    public class Product
    {
         public Guid Id { get; private set; }
        public Guid UserId { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public string Price { get; private set; }
        public string Image { get; private set; }

        Product() { }

        public static Product NewProduct(AddProductModel productModel)
        {
            return new Product() 
            {
                Id = new Guid(),
                UserId = productModel.UserId,
                Description = productModel.Description,
                Image  = productModel.Image,
                Name = productModel.Name,
                Price = productModel.Price,
            };
        }
    }
}
