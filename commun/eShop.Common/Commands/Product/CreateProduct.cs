using System;
using System.Collections.Generic;
using System.Text;

namespace eShop.Common.Commands.Product
{
    public class CreateProduct : ICommand
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Price { get; set; }
        public string Image { get; set; }
    }
}
