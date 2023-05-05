using AutoMapper;
using cwdemo.core.Models;
using cwdemo.core.Services.Interfaces;
using cwdemo.data.Entities;
using cwdemo.data.Interfaces;
using cwdemo.infrastructure.Models;
using System.Net;

namespace cwdemo.core.Services
{
    public class StoreService : BaseService, IStoreService
    {
        public StoreService(IRepositories repositories, IMapper mapper) : base(repositories, mapper)
        { }

        public async Task<ServiceResponse<Store>> Get(int storeId)
        {
            var store = await _repositories.Stores.GetStoreById(storeId);
            if (store == null)
                return new ServiceResponse<Store>(false, "Store not found", (int)HttpStatusCode.NotFound);

            var response = new ServiceResponse<Store>(_mapper.Map<Store>(store));
            return response;
        }

        public async Task<ServiceResponse<List<Store>>> GetAll()
        {
            var stores = await _repositories.Stores.GetAllStores();
            if (stores == null || stores.Count == 0)
                return new ServiceResponse<List<Store>>();

            var result = _mapper.Map<List<Store>>(stores);

            return new ServiceResponse<List<Store>>(result);
        }


        public async Task<ServiceResponse<Store>> Create(CreateStore store)
        {
            var newStore = _mapper.Map<StoreEntity>(store);
            newStore.Active = true;

            var result = await _repositories.Stores.AddStore(newStore);
            if (result == null)
                return new ServiceResponse<Store>(false);

            var resp = _mapper.Map<Store>(newStore);
            return new ServiceResponse<Store>(resp);
        }

        public async Task<ServiceResponse<Store>> Update(Store store)
        {
            var serviceResponse = new ServiceResponse<Store>();

            // Check if the store exists
            var existingStore = await _repositories.Stores.GetStoreById(store.Id);
            if (existingStore == null)
            {
                serviceResponse.Valid = false;
                serviceResponse.Message.Add("Store not found");
                return serviceResponse;
            }

            // Update the existing store
            existingStore.Name = store.Name;
            existingStore.Location = store.Location;
            existingStore.Active = store.Active;

            // Save the changes
            var updateResult = await _repositories.Stores.UpdateStore(store.Id, existingStore);
            if (!updateResult)
            {
                serviceResponse.Valid = false;
                serviceResponse.Message.Add("Failed to update store");
                return serviceResponse;
            }

            // Map the updated store to the domain model
            var updatedStore = _mapper.Map<Store>(existingStore);

            serviceResponse.Content = updatedStore;
            serviceResponse.Message.Add("Store updated successfully");
            return serviceResponse;
        }

        public async Task<ServiceResponse> Delete(long storeId)
        {
            var response = new ServiceResponse();

            var deleted = await _repositories.Stores.DeleteStore(storeId);
            if (!deleted)
            {
                response.Message.Add($"Store with ID {storeId} not found.");
                response.Valid = false;
            }
            else
            {
                response.Message.Add($"Store with ID {storeId} deleted successfully.");
                response.Valid = true;
            }

            return response;
        }

    }
}
