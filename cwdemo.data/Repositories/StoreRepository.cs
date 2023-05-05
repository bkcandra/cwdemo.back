using cwdemo.data.Entities;
using cwdemo.data.Interfaces;
using cwdemo.infrastructure;

namespace cwdemo.data.Repositories
{
    public class StoreRepository : IStoreRepository
    {
        private readonly List<StoreEntity> _storeEntities;

        public StoreRepository()
        {
            _storeEntities = Singleton<List<StoreEntity>>.Instance ?? new List<StoreEntity>();
        }



        public async Task<StoreEntity> GetStoreById(long storeId)
        {
            var store = _storeEntities.FirstOrDefault(x => x.Id == storeId);
            if (store == null)
                return null;

            return store;
        }

        public async Task<List<StoreEntity>> GetAllStores()
        {
            return _storeEntities;
        }

        public async Task<StoreEntity> AddStore(StoreEntity store)
        {
            store.Id = _storeEntities.Count > 0 ? _storeEntities.Max(x => x.Id) + 1 : 1;
            _storeEntities.Add(store);
            return store;
        }

        public async Task<bool> UpdateStore(long storeId, StoreEntity store)
        {
            var existingStore = await GetStoreById(storeId);
            if (existingStore == null)
            {
                return false;
            }
            existingStore.Name = store.Name;
            existingStore.Location = store.Location;
            return true;
        }

        public async Task<bool> DeleteStore(long storeId)
        {
            var store = await GetStoreById(storeId);
            if (store == null)
            {
                return false;
            }
            _storeEntities.Remove(store);
            return true;
        }
    }
}