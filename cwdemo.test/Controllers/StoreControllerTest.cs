using cwdemo.api.Controllers;
using cwdemo.core.Models;
using cwdemo.core.Services.Interfaces;
using cwdemo.data.Entities;
using cwdemo.infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace cwdemo.test.Controllers
{
    [TestFixture]
    public class CatalogControllerTests
    {
        private Mock<ICatalogService> _catalogServiceMock;
        private CatalogController _controller;

        [SetUp]
        public void SetUp()
        {
            _catalogServiceMock = new Mock<ICatalogService>();
            _controller = new CatalogController(_catalogServiceMock.Object);
        }

        [Test]
        public async Task GetProducts_Returns_Products()
        {
            // Arrange
            var expectedProducts = new ServiceResponse<List<Catalog>>(new List<Catalog>
        {
            new Catalog { Id = 1, Name = "Product 1" },
            new Catalog { Id = 2, Name = "Product 2" },
            new Catalog { Id = 3, Name = "Product 3" }
        });
            _catalogServiceMock.Setup(x => x.GetAll()).ReturnsAsync(expectedProducts);

            // Act
            var result = await _controller.GetProducts();

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = (OkObjectResult)result;
            Assert.AreEqual(expectedProducts, okResult.Value);
        }

        [Test]
        public async Task GetProduct_Returns_Product()
        {
            // Arrange
            var expectedProduct = new ServiceResponse<CatalogStoreEntity>(new CatalogStoreEntity { Id = 1, Name = "Product 1" });
            _catalogServiceMock.Setup(x => x.Get(1)).ReturnsAsync(expectedProduct);

            // Act
            var result = await _controller.GetProduct(1);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = (OkObjectResult)result;
            Assert.AreEqual(expectedProduct, okResult.Value);
        }

        [Test]
        public async Task CreateProduct_Calls_CatalogService_And_Returns_CreatedProduct()
        {
            // Arrange
            var createCatalog = new CreateCatalog { Name = "New Product" };
            var createdCatalog = new ServiceResponse<Catalog>(new Catalog { Id = 1, Name = "New Product" });
            _catalogServiceMock.Setup(x => x.Create(createCatalog)).ReturnsAsync(createdCatalog);

            // Act
            var result = await _controller.CreateProduct(createCatalog);

            // Assert
            _catalogServiceMock.Verify(x => x.Create(createCatalog), Times.Once);
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = (OkObjectResult)result;
            Assert.AreEqual(createdCatalog, okResult.Value);
        }

        [Test]
        public async Task UpdateProduct_Calls_CatalogService_And_Returns_UpdatedProduct()
        {
            // Arrange
            var updateCatalog = new UpdateCatalog { Id = 1, Name = "Updated Product" };
            var updatedCatalog = new ServiceResponse<Catalog>(new Catalog { Id = 1, Name = "Updated Product" });
            _catalogServiceMock.Setup(x => x.Update(updateCatalog)).ReturnsAsync(updatedCatalog);

            // Act
            var result = await _controller.UpdateProduct(1, updateCatalog);

            // Assert
            _catalogServiceMock.Verify(x => x.Update(updateCatalog), Times.Once);
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = (OkObjectResult)result;
            Assert.AreEqual(updatedCatalog, okResult.Value);
        }
        [Test]
        public async Task DeleteProduct_Calls_CatalogService_And_Returns_DeletedProduct()
        {
            // Arrange
            var deletedCatalog = new ServiceResponse<Catalog>(new Catalog { Id = 1, Name = "Deleted Product" });
            _catalogServiceMock.Setup(x => x.Delete(1)).ReturnsAsync(deletedCatalog);

            // Act
            var result = await _controller.DeleteProduct(1);

            // Assert
            _catalogServiceMock.Verify(x => x.Delete(1), Times.Once);
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = (OkObjectResult)result;
            Assert.AreEqual(deletedCatalog, okResult.Value);
        }

    }
}
