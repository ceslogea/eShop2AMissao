using System;

namespace eShop.Catalog.Domain
{
    public class AddProductModel
    {
        public Guid Id { get; private set; }
        public Guid UserId { get; private set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Price { get; set; }
        public string Image { get; set; }

        internal bool IsValid()
        {
           return true;
        }
    }
}