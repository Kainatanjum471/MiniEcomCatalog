using Moq;
using MiniECommerceCatalog.Data.Models;
using MiniECommerceCatalog.Data.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MiniECommerceCatalog.Api.Controllers;
using Xunit;

namespace MiniECommerceCatalog.Tests.Controllers
{
    public class ProductControllerTests
    {
        private readonly Mock<IProductRepository> _mockRepo;
        private readonly ProductController _controller;

        public ProductControllerTests()
        {
            _mockRepo = new Mock<IProductRepository>();
            _controller = new ProductController(_mockRepo.Object);
        }

        [Fact]
        public async Task GetAllProducts_ReturnsOkResult_WithListOfProducts()
        {
            var mockProducts = new List<Product>
            {
                new Product { ID = 1, Name = "Test Product 1" },
                new Product { ID = 2, Name = "Test Product 2" }
            };
            _mockRepo.Setup(repo => repo.GetAllAsync()).ReturnsAsync(mockProducts);

            var result = await _controller.GetAllProducts();

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var products = Assert.IsAssignableFrom<IEnumerable<Product>>(okResult.Value);
            Assert.Equal(2, products.Count());
        }

        [Fact]
        public async Task GetProductById_ProductExists_ReturnsOkResult()
        {
            var mockProduct = new Product { ID = 1, Name = "Test Product" };
            _mockRepo.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(mockProduct);

            var result = await _controller.GetProductById(1);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var product = Assert.IsType<Product>(okResult.Value);
            Assert.Equal(1, product.ID);
        }

        [Fact]
        public async Task GetProductById_ProductDoesNotExist_ReturnsNotFound()
        {
             _mockRepo.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync((Product)null);

            var result = await _controller.GetProductById(1);

            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task AddProduct_ValidProduct_ReturnsCreatedAtActionResult()
        {
            var newProduct = new Product { ID = 1, Name = "New Product" };
            _mockRepo.Setup(repo => repo.AddAsync(newProduct)).Returns(Task.CompletedTask);

            var result = await _controller.AddProduct(newProduct);

            var createdAtResult = Assert.IsType<CreatedAtActionResult>(result);
            var product = Assert.IsType<Product>(createdAtResult.Value);
            Assert.Equal(1, product.ID);
        }

        [Fact]
        public async Task DeleteProduct_ValidId_ReturnsNoContentResult()
        {
            _mockRepo.Setup(repo => repo.DeleteAsync(1)).Returns(Task.CompletedTask);

            var result = await _controller.DeleteProduct(1);

            Assert.IsType<NoContentResult>(result);
        }
    }
}
