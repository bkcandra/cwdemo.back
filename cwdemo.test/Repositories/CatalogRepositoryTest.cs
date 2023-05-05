using cwdemo.core.Common.Constants;
using cwdemo.data.Entities;
using cwdemo.data.Repositories;
using cwdemo.infrastructure;
using NUnit.Framework;

[TestFixture]
public class CatalogRepositoryTests
{
    private CatalogRepository _catalogRepository;
    private StoreRepository _storeRepository;

    [SetUp]
    public async Task SetUp()
    {
        // Arrange
        Singleton<List<CatalogEntity>>.Instance = new List<CatalogEntity>(){ new CatalogEntity
        {
            Id = 1,
            Name = "Margherita",
            Description = "Classic tomato and mozzarella cheese pizza",
            Price = 8.99m,
            Active = true,
            Type = (int)CatalogType.Pizza,
            StoreId = 1

        },new CatalogEntity
        {
            Id = 2,
            Name = "Pepperoni",
            Description = "Tomato sauce, mozzarella cheese, and pepperoni",
            Price = 10.99m,
            Active = true,
            Type = (int)CatalogType.Pizza,
            StoreId = 1
        },new CatalogEntity
        {
            Id = 3,
            Name = "Mushrooms",
            Description = "Fresh mushrooms on top of classic tomato sauce and mozzarella cheese",
            Price = 2.99m,
            Active = true,
            Type = (int)CatalogType.Toppings,
            StoreId = 1
        }

        };
        Singleton<List<StoreEntity>>.Instance = new List<StoreEntity>() {
        new StoreEntity
        {
            Id = 1,
            Name = "The Pizza Place",
            Active = true,
            Location = "Hawthorn"
        }};

        _catalogRepository = new CatalogRepository();
        _storeRepository = new StoreRepository();
    }

    [Test]
    public async Task GetAllCatalogs_Should_Return_All_CatalogStoreEntities()
    {
        // Act
        var result = await _catalogRepository.GetAllCatalogs();

        // Assert
        Assert.AreEqual(3, result.Count);

        var catalog1 = result.Find(x => x.Id == 1);
        var catalog2 = result.Find(x => x.Id == 2);
        var catalog3 = result.Find(x => x.Id == 3);

        Assert.IsNotNull(catalog1);
        Assert.IsNotNull(catalog2);
        Assert.IsNotNull(catalog3);

        Assert.AreEqual("Margherita", catalog1.Name);
        Assert.AreEqual("Pepperoni", catalog2.Name);
        Assert.AreEqual("Mushrooms", catalog3.Name);

        Assert.AreEqual((int)CatalogType.Pizza, catalog1.Type);
        Assert.AreEqual((int)CatalogType.Pizza, catalog2.Type);
        Assert.AreEqual((int)CatalogType.Toppings, catalog3.Type);
    }

    [Test]
    public async Task GetCatalogById_Should_Return_CatalogStoreEntity_With_Valid_Store()
    {
        // Arrange
        long catalogId = 1;

        // Act
        var result = await _catalogRepository.GetCatalogById(catalogId);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual("Margherita", result.Name);
        Assert.AreEqual((int)CatalogType.Pizza, result.Type);
        Assert.AreEqual(1, result.StoreId);
    }

    [Test]
    public async Task GetCatalogById_Should_Return_Null_When_Catalog_Not_Found()
    {
        // Arrange
        long catalogId = 4;

        // Act
        var result = await _catalogRepository.GetCatalogById(catalogId);

        // Assert
        Assert.IsNull(result);
    }

    [Test]
    public async Task GetCatalogById_Should_Return_Null_When_Store_Not_Found()
    {
        // Arrange
        long catalogId = 5;

        // Act
        var result = await _catalogRepository.GetCatalogById(catalogId);

        // Assert
        Assert.IsNull(result);
    }

    [Test]
    public async Task CreateCatalog_Should_Add_New_Catalog()
    {
        // Arrange
        var newCatalog = new CatalogEntity
        {
            Id = 4,
            Name = "Vegetarian",
            Description = "Tomato sauce, mozzarella cheese, and fresh vegetables",
            Price = 12.99m,
            Active = true,
            Type = (int)CatalogType.Pizza,
            StoreId = 1
        };

        // Act
        var result = await _catalogRepository.CreateCatalog(newCatalog);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(newCatalog, result);

        var catalogs = Singleton<List<CatalogEntity>>.Instance;
        Assert.AreEqual(4, catalogs.Count);
        Assert.IsTrue(catalogs.Contains(newCatalog));
    }

    [Test]
    public async Task UpdateCatalog_Should_Update_Existing_Catalog_And_Return_True()
    {
        // Arrange
        long catalogId = 1;
        var updatedCatalog = new CatalogEntity
        {
            Id = 1,
            Name = "Margherita (Updated)",
            Description = "Classic tomato and mozzarella cheese pizza (Updated)",
            Price = 10.99m,
            Active = false,
            Type = (int)CatalogType.Pizza,
            StoreId = 1
        };

        // Act
        var result = await _catalogRepository.UpdateCatalog(catalogId, updatedCatalog);

        // Assert
        Assert.IsTrue(result);
        var catalog = await _catalogRepository.GetCatalogById(1);
        Assert.IsNotNull(catalog);
        Assert.AreEqual("Margherita (Updated)", catalog.Name);
        Assert.AreEqual("Classic tomato and mozzarella cheese pizza (Updated)", catalog.Description);
        Assert.AreEqual(10.99m, catalog.Price);
        Assert.AreEqual(false, catalog.Active);
        Assert.AreEqual((int)CatalogType.Pizza, catalog.Type);
        Assert.AreEqual(1, catalog.StoreId);
    }
    [Test]
    public async Task UpdateCatalog_Should_Return_False_When_Catalog_Not_Found()
    {
        // Arrange
        long catalogId = 4;
        var catalog = new CatalogEntity
        {
            Id = 4,
            Name = "Veggie Deluxe",
            Description = "Tomato sauce, mozzarella cheese, and a variety of fresh vegetables",
            Price = 14.99m,
            Active = true,
            Type = (int)CatalogType.Pizza,
            StoreId = 1
        };

        // Act
        var result = await _catalogRepository.UpdateCatalog(catalogId, catalog);

        // Assert
        Assert.IsFalse(result);
    }

    [Test]
    public async Task UpdateCatalog_Should_Return_False_When_Store_Not_Found()
    {
        // Arrange
        long catalogId = 1;
        var catalog = new CatalogEntity
        {
            Id = 1,
            Name = "Margherita",
            Description = "Classic tomato and mozzarella cheese pizza",
            Price = 8.99m,
            Active = true,
            Type = (int)CatalogType.Pizza,
            StoreId = 2
        };

        // Act
        var result = await _catalogRepository.UpdateCatalog(catalogId, catalog);

        // Assert
        Assert.IsFalse(result);
    }

    [Test]
    public async Task DeleteCatalog_Should_Return_True_When_Catalog_Deleted()
    {
        // Arrange
        long catalogId = 1;

        // Act
        var result = await _catalogRepository.DeleteCatalog(catalogId);

        // Assert
        Assert.IsTrue(result);
        Assert.IsNull(await _catalogRepository.GetCatalogById(catalogId));
    }

    [Test]
    public async Task DeleteCatalog_Should_Return_False_When_Catalog_Not_Found()
    {
        // Arrange
        long catalogId = 4;

        // Act
        var result = await _catalogRepository.DeleteCatalog(catalogId);

        // Assert
        Assert.IsFalse(result);
    }

}