using CatalogApi;

namespace eShop.Gateway.Domain.Model
{
    public class AddProductModel
    {
        public string Image { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
    }
}