using cwdemo.data.Entities;
using cwdemo.data.Interfaces;
using cwdemo.infrastructure;

namespace cwdemo.data.Repositories
{
    public class CatalogRepository : ICatalogRepository
    {
        private readonly List<CatalogEntity> _catalogEntities;
        private readonly List<StoreEntity> _storeEntities;


        public CatalogRepository()
        {
            _catalogEntities = Singleton<List<CatalogEntity>>.Instance ?? new List<CatalogEntity>();
            _storeEntities = Singleton<List<StoreEntity>>.Instance ?? new List<StoreEntity>();
        }

        public async Task<CatalogEntity> GetCatalogById(long catalogId)
        {
            var catalog = _catalogEntities.FirstOrDefault(x => x.Id == catalogId);
            if (catalog == null)
                return null;

            // Check if the store exists
            var store = _storeEntities.FirstOrDefault(x => x.Id == catalog.StoreId);
            if (store == null)
                return null;

            return new CatalogStoreEntity
            {
                Id = catalog.Id,
                Name = catalog.Name,
                Description = catalog.Description,
                Price = catalog.Price,
                Active = catalog.Active,
                Type = catalog.Type,
                StoreId = catalog.StoreId,
                StoreName = store.Name
            };
        }

        public async Task<List<CatalogStoreEntity>> GetAllCatalogs()
        {
            var catalogs = _catalogEntities;
            var catalogStoreEntities = new List<CatalogStoreEntity>();
            foreach (var catalog in catalogs)
            {
                var store = _storeEntities.FirstOrDefault(x => x.Id == catalog.StoreId);
                if (store == null)
                    continue;
                catalogStoreEntities.Add(new CatalogStoreEntity
                {
                    Id = catalog.Id,
                    Name = catalog.Name,
                    Description = catalog.Description,
                    Price = catalog.Price,
                    Active = catalog.Active,
                    Type = catalog.Type,
                    StoreId = catalog.StoreId,
                    StoreName = store.Name
                });
            }
            return catalogStoreEntities;
        }

        public async Task<CatalogEntity> CreateCatalog(CatalogEntity newCatalog)
        {
            // Check if the store exists
            var store = _storeEntities.FirstOrDefault(x => x.Id == newCatalog.StoreId);
            if (store == null)
                return null;


            _catalogEntities.Add(newCatalog);

            return newCatalog;
        }

        public async Task<bool> UpdateCatalog(long catalogId, CatalogEntity catalog)
        {
            // Get the catalog by ID
            var existingCatalog = await GetCatalogById(catalogId);
            if (existingCatalog == null)
                return false;

            // Check if the store exists
            var store = _storeEntities.FirstOrDefault(x => x.Id == catalog.StoreId);
            if (store == null)
                return false;

            // Update the existing catalog
            existingCatalog.Name = catalog.Name;
            existingCatalog.Description = catalog.Description;
            existingCatalog.Price = catalog.Price;
            existingCatalog.Type = catalog.Type;
            existingCatalog.StoreId = catalog.StoreId;

            return true;
        }

        public async Task<bool> DeleteCatalog(long catalogId)
        {
            // Get the catalog by ID
            var existingCatalog = await GetCatalogById(catalogId);
            if (existingCatalog == null)
                return false;

            // Remove the catalog
            _catalogEntities.Remove(existingCatalog);

            return true;
        }
    }


}
