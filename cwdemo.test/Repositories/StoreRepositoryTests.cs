using cwdemo.data.Entities;
using cwdemo.data.Repositories;
using cwdemo.infrastructure;
using NUnit.Framework;

[TestFixture]
public class StoreRepositoryTests
{
    private StoreRepository _storeRepository;

    [SetUp]
    public async Task SetUp()
    {
        // Arrange
        Singleton<List<StoreEntity>>.Instance = new List<StoreEntity>() {
        new StoreEntity
        {
            Id = 1,
            Name = "The Pizza Place",
            Active = true,
            Location = "Hawthorn"
        }};

        _storeRepository = new StoreRepository();
    }
    [Test]
    public async Task GetStoreById_Should_Return_Existing_Store()
    {
        // Arrange
        long storeId = 1;

        // Act
        var result = await _storeRepository.GetStoreById(storeId);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(storeId, result.Id);
        Assert.AreEqual("The Pizza Place", result.Name);
        Assert.IsTrue(result.Active);
        Assert.AreEqual("Hawthorn", result.Location);
    }

    [Test]
    public async Task GetStoreById_Should_Return_Null_When_Store_Not_Found()
    {
        // Arrange
        long storeId = 2;

        // Act
        var result = await _storeRepository.GetStoreById(storeId);

        // Assert
        Assert.IsNull(result);
    }

    [Test]
    public async Task GetAllStores_ShouldReturnAllStores()
    {
        // Act
        var stores = await _storeRepository.GetAllStores();

        // Assert
        Assert.IsNotNull(stores);
        Assert.AreEqual(1, stores.Count);
        Assert.AreEqual("The Pizza Place", stores[0].Name);
        Assert.AreEqual(true, stores[0].Active);
        Assert.AreEqual("Hawthorn", stores[0].Location);
    }

    [Test]
    public async Task AddStore_ShouldIncreaseStoreCount()
    {
        // Arrange
        var initialStoreCount = _storeRepository.GetAllStores().Result.Count;
        var newStore = new StoreEntity
        {
            Name = "New Store",
            Active = true,
            Location = "Melbourne"
        };

        // Act
        await _storeRepository.AddStore(newStore);

        // Assert
        var updatedStoreCount = _storeRepository.GetAllStores().Result.Count;
        Assert.AreEqual(initialStoreCount + 1, updatedStoreCount);
    }

    [Test]
    public async Task AddStore_ShouldAssignNewIdToStore()
    {
        // Arrange
        var newStore = new StoreEntity
        {
            Name = "New Store",
            Active = true,
            Location = "Melbourne"
        };

        // Act
        var addedStore = await _storeRepository.AddStore(newStore);

        // Assert
        Assert.Greater(addedStore.Id, 0);
    }

    [Test]
    public async Task AddStore_ShouldAddNewStoreToStoreEntitiesList()
    {
        // Arrange
        var newStore = new StoreEntity
        {
            Name = "New Store",
            Active = true,
            Location = "Melbourne"
        };

        // Act
        var addedStore = await _storeRepository.AddStore(newStore);

        // Assert
        var allStores = _storeRepository.GetAllStores().Result;
        Assert.IsTrue(allStores.Contains(addedStore));
    }

    [Test]
    public async Task UpdateStore_ShouldUpdateExistingStore()
    {
        // Arrange
        var storeToUpdate = new StoreEntity
        {
            Name = "The Burger Joint",
            Location = "Collingwood"
        };

        // Act
        var result = await _storeRepository.UpdateStore(1, storeToUpdate);

        // Assert
        Assert.IsTrue(result);
        var updatedStore = await _storeRepository.GetStoreById(1);
        Assert.AreEqual(storeToUpdate.Name, updatedStore.Name);
        Assert.AreEqual(storeToUpdate.Location, updatedStore.Location);
    }

    [Test]
    public async Task UpdateStore_ShouldReturnFalseForNonExistingStore()
    {
        // Arrange
        var storeToUpdate = new StoreEntity
        {
            Name = "The Burger Joint",
            Location = "Collingwood"
        };

        // Act
        var result = await _storeRepository.UpdateStore(2, storeToUpdate);

        // Assert
        Assert.IsFalse(result);
    }

    [Test]
    public async Task DeleteStore_DeletesExistingStore_ReturnsTrue()
    {
        // Arrange
        var existingStoreId = 1;

        // Act
        var result = await _storeRepository.DeleteStore(existingStoreId);

        // Assert
        Assert.IsTrue(result);
        Assert.IsNull(_storeRepository.GetStoreById(existingStoreId).Result);
    }

}
