using cwdemo.core.Models;
using cwdemo.core.Services.Interfaces;
using cwdemo.data.Interfaces;
using cwdemo.infrastructure.Models;

namespace cwdemo.core.Services
{
    public class StoreService : BaseService, IStoreService
    {
        public StoreService(IRepositories repositories) : base(repositories)
        {

        }

        public async Task<ServiceResponse<Store>> Get(int storeId)
        {
            // TODO: Implement logic to get store by id
            var store = await _repositories.StoreRepository.GetById(storeId);
            if (store == null)
            {
                return ServiceResponse<Store>.NotFound();
            }
            return ServiceResponse<Store>.Ok(store);
        }

        public async Task<ServiceResponse<List<Store>>> GetAll()
        {
            // TODO: Implement logic to get all stores
            var stores = await _repositories.StoreRepository.GetAll();
            return ServiceResponse<List<Store>>.Ok(stores);
        }

        public async Task<ServiceResponse<Store>> Create(CreateStore store)
        {
            // TODO: Implement logic to create a new store
            var newStore = new Store
            {
                Name = store.Name,
                Location = store.Location
            };
            await _repositories.StoreRepository.Add(newStore);
            return ServiceResponse<Store>.Ok(newStore);
        }
    }
}
