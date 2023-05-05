using AutoMapper;
using cwdemo.core.Models;
using cwdemo.core.Services;
using cwdemo.data.Entities;
using cwdemo.data.Interfaces;
using Moq;
using NUnit.Framework;
using System.Net;

namespace cwdemo.test.Services
{
    [TestFixture]
    public class StoreServiceTest
    {
        private Mock<IRepositories> _mockRepositories;
        private Mock<IMapper> _mockMapper;
        private StoreService _storeService;

        [SetUp]
        public async Task SetUp()
        {
            _mockRepositories = new Mock<IRepositories>();
            _mockMapper = new Mock<IMapper>();
            _storeService = new StoreService(_mockRepositories.Object, _mockMapper.Object);
        }

        [Test]
        public async Task Get_StoreExists_ReturnsServiceResponseWithStore()
        {
            // Arrange
            var storeId = 1;
            var storeEntity = new StoreEntity { Id = storeId, Name = "The Pizza Place", Active = true, Location = "Hawthorn" };
            var store = new Store { Id = storeId, Name = "The Pizza Place", Location = "Hawthorn" };
            _mockRepositories.Setup(x => x.Stores.GetStoreById(storeId)).ReturnsAsync(storeEntity);
            _mockMapper.Setup(x => x.Map<Store>(storeEntity)).Returns(store);

            // Act
            var response = await _storeService.Get(storeId);

            // Assert
            Assert.That(response.Valid, Is.True);
            Assert.That(response.Content, Is.EqualTo(store));
            Assert.That(response.StatusCode, Is.EqualTo((int)HttpStatusCode.OK));
        }

        [Test]
        public async Task Get_StoreDoesNotExist_ReturnsServiceResponseWithError()
        {
            // Arrange
            var storeId = 1;
            StoreEntity storeEntity = null;
            _mockRepositories.Setup(x => x.Stores.GetStoreById(storeId)).ReturnsAsync(storeEntity);

            // Act
            var response = await _storeService.Get(storeId);

            // Assert
            Assert.That(response.Valid, Is.False);
            Assert.That(response.Message.First(), Is.EqualTo("Store not found"));
            Assert.That(response.StatusCode, Is.EqualTo((int)HttpStatusCode.NotFound));
        }



        [Test]
        public async Task GetAll_StoresExist_ReturnsServiceResponseWithStores()
        {
            // Arrange
            var storeEntities = new List<StoreEntity>()
    {
        new StoreEntity { Id = 1, Name = "The Pizza Place", Active = true, Location = "Hawthorn" },
        new StoreEntity { Id = 2, Name = "The Burger Joint", Active = true, Location = "Richmond" },
        new StoreEntity { Id = 3, Name = "The Sushi Spot", Active = true, Location = "CBD" },
    };
            var stores = new List<Store>()
    {
        new Store { Id = 1, Name = "The Pizza Place", Location = "Hawthorn" },
        new Store { Id = 2, Name = "The Burger Joint", Location = "Richmond" },
        new Store { Id = 3, Name = "The Sushi Spot", Location = "CBD" },
    };
            _mockRepositories.Setup(x => x.Stores.GetAllStores()).ReturnsAsync(storeEntities);
            _mockMapper.Setup(x => x.Map<List<Store>>(storeEntities)).Returns(stores);

            // Act
            var response = await _storeService.GetAll();

            // Assert
            Assert.That(response.Valid, Is.True);
            Assert.That(response.Content, Is.EqualTo(stores));
        }

        [Test]
        public async Task GetAll_NoStoresExist_ReturnsEmptyServiceResponse()
        {
            // Arrange
            List<StoreEntity> storeEntities = null;
            _mockRepositories.Setup(x => x.Stores.GetAllStores()).ReturnsAsync(storeEntities);

            // Act
            var response = await _storeService.GetAll();

            // Assert
            Assert.That(response.Valid, Is.True);
            Assert.That(response.Content, Is.Empty);
        }

        [Test]
        public async Task Create_NewStore_ReturnsServiceResponseWithStore()
        {
            // Arrange
            var createStore = new CreateStore { Name = "The Pizza Place", Location = "Hawthorn" };
            var storeEntity = new StoreEntity { Id = 1, Name = createStore.Name, Active = true, Location = createStore.Location };
            var store = new Store { Id = 1, Name = createStore.Name, Location = createStore.Location };
            _mockMapper.Setup(x => x.Map<StoreEntity>(createStore)).Returns(storeEntity);
            _mockRepositories.Setup(x => x.Stores.AddStore(storeEntity)).ReturnsAsync(storeEntity);
            _mockMapper.Setup(x => x.Map<Store>(storeEntity)).Returns(store);

            // Act
            var response = await _storeService.Create(createStore);

            // Assert
            Assert.That(response.Valid, Is.True);
            Assert.That(response.Content, Is.EqualTo(store));
        }

        [Test]
        public async Task Update_StoreExists_ReturnsServiceResponseWithUpdatedStore()
        {
            // Arrange
            var storeId = 1;
            var updatedStore = new Store { Id = storeId, Name = "The Pizza Place Updated", Location = "Hawthorn Updated", Active = false };
            var existingStoreEntity = new StoreEntity { Id = storeId, Name = "The Pizza Place", Location = "Hawthorn", Active = true };
            _mockRepositories.Setup(x => x.Stores.GetStoreById(storeId)).ReturnsAsync(existingStoreEntity);
            _mockMapper.Setup(x => x.Map<Store>(existingStoreEntity)).Returns(new Store { Id = storeId, Name = "The Pizza Place", Location = "Hawthorn", Active = true });
            _mockMapper.Setup(x => x.Map<StoreEntity>(updatedStore)).Returns(new StoreEntity { Id = storeId, Name = "The Pizza Place Updated", Location = "Hawthorn Updated", Active = false });
            _mockMapper.Setup(x => x.Map<Store>(It.IsAny<StoreEntity>())).Returns(updatedStore);
            _mockRepositories.Setup(x => x.Stores.UpdateStore(storeId, It.IsAny<StoreEntity>())).ReturnsAsync(true);

            // Act
            var response = await _storeService.Update(updatedStore);

            // Assert
            Assert.That(response.Valid, Is.True);
            Assert.That(response.Content, Is.EqualTo(updatedStore));
            Assert.That(response.Message.Contains("Store updated successfully"), Is.True);
        }


        [Test]
        public async Task Delete_StoreExists_ReturnsServiceResponseWithSuccessMessage()
        {
            // Arrange
            var storeId = 1;
            _mockRepositories.Setup(x => x.Stores.DeleteStore(storeId)).ReturnsAsync(true);

            // Act
            var response = await _storeService.Delete(storeId);

            // Assert
            Assert.That(response.Valid, Is.True);
            Assert.That(response.Message, Contains.Item($"Store with ID {storeId} deleted successfully."));
        }

        [Test]
        public async Task Delete_StoreDoesNotExist_ReturnsServiceResponseWithErrorMessage()
        {
            // Arrange
            var storeId = 1;
            _mockRepositories.Setup(x => x.Stores.DeleteStore(storeId)).ReturnsAsync(false);

            // Act
            var response = await _storeService.Delete(storeId);

            // Assert
            Assert.That(response.Valid, Is.False);
            Assert.That(response.Message, Contains.Item($"Store with ID {storeId} not found."));
        }
    }

}
