using MiniECommerceCatalog.Data.Models;
using MiniECommerceCatalog.Data.Repositories;

namespace MiniECommerceCatalog.Tests.Repositories
{
    public class ProductRepositoryTests
    {
        private readonly IProductRepository _repository;

        public ProductRepositoryTests(IProductRepository repository)
        {
            _repository = repository;
        }

        [Fact]
        public async Task AddProduct_ShouldAddProductSuccessfully()
        {
            var product = new Product { Name = "Test Product", Price = 9.99M, Category = "Test Category" };

            await _repository.AddAsync(product);
            var retrievedProduct = await _repository.GetByIdAsync(product.ID);

            Assert.NotNull(retrievedProduct);
            Assert.Equal("Test Product", retrievedProduct.Name);
        }

        [Fact]
        public async Task DeleteProduct_ShouldRemoveProductSuccessfully()
        {
            var product = new Product { Name = "Test Product", Price = 9.99M, Category = "Test Category" };

            await _repository.AddAsync(product);
            await _repository.DeleteAsync(product.ID);
            var retrievedProduct = await _repository.GetByIdAsync(product.ID);

            Assert.Null(retrievedProduct);
        }
    }
}