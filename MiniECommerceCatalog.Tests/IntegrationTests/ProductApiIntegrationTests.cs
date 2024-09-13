using MiniECommerceCatalog.Data.Models;
using System.Net;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.Testing;

namespace MiniECommerceCatalog.Tests.IntegrationTests
{
    public class ProductApiIntegrationTests : IClassFixture<WebApplicationFactory<MiniECommerceCatalog.Api.Controllers.ProductController>>
    {
        private readonly HttpClient _client;

        public ProductApiIntegrationTests(WebApplicationFactory<MiniECommerceCatalog.Api.Controllers.ProductController> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GetAllProducts_ReturnsOkAndListOfProducts()
        {
            var response = await _client.GetAsync("/api/Product");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var content = await response.Content.ReadAsStringAsync();
            var products = JsonSerializer.Deserialize<List<Product>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            Assert.NotNull(products);
            Assert.True(products.Count > 0);
        }

        [Fact]
        public async Task GetProductById_ValidId_ReturnsOk()
        {
            var response = await _client.GetAsync("/api/Product/1");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var content = await response.Content.ReadAsStringAsync();
            var product = JsonSerializer.Deserialize<Product>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            Assert.NotNull(product);
            Assert.Equal(1, product.ID);
        }

        [Fact]
        public async Task AddProduct_ValidProduct_ReturnsCreatedAtAction()
        {
            var product = new Product { Name = "Test Product", Price = 10.00M, Category = "Test" };
            var productContent = new StringContent(JsonSerializer.Serialize(product), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("/api/Product", productContent);

            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }
    }
}
