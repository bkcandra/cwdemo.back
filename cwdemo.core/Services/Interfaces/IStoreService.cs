using cwdemo.core.Models;
using cwdemo.infrastructure.Models;

namespace cwdemo.core.Services.Interfaces
{
    public interface IStoreService
    {
        Task<ServiceResponse<Store>> Get(int storeId);
        Task<ServiceResponse<List<Store>>> GetAll();
        Task<ServiceResponse<Store>> Create(CreateStore store);
        Task<ServiceResponse<Store>> Update(Store store);
        Task<ServiceResponse> Delete(long storeId);
    }
}