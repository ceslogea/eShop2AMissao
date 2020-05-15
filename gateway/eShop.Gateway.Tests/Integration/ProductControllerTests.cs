using FluentAssertions;
using System;
using System.Threading.Tasks;
using Xunit;

namespace eShop.Gateway.Tests.Integration
{
    public class ProductControllerTests : BaseServerHttpClientTests
    {
        [Fact]
        public async Task Product_controller_get_should_return_a_product()
        {
            // Arrange (BaseServerHttpClientTests)

            // Act
            var response = await _client.GetAsync($"/api/catalog/v1/product?id={new Guid()}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();

            // Assert
            content.Should().BeEquivalentTo("Hello from Actio API!");
        }
    }
}