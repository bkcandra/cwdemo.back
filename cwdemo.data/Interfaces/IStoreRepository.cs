using cwdemo.data.Entities;

namespace cwdemo.data.Interfaces
{

    public interface IStoreRepository
    {
        Task<StoreEntity> GetStoreById(long storeId);
        Task<List<StoreEntity>> GetAllStores();
        Task<StoreEntity> AddStore(StoreEntity store);
        Task<bool> UpdateStore(long storeId, StoreEntity store);
        Task<bool> DeleteStore(long storeId);
    }
}