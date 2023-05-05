using cwdemo.core.Models;
using cwdemo.data.Entities;
using cwdemo.infrastructure.Models;

namespace cwdemo.core.Services.Interfaces
{
    public interface ICatalogService
    {
        Task<ServiceResponse<CatalogStoreEntity>> Get(int catalogId);
        Task<ServiceResponse<List<Catalog>>> GetAll();
        Task<ServiceResponse<Catalog>> Create(CreateCatalog catalog);
        Task<ServiceResponse<Catalog>> Update(UpdateCatalog catalog);
        Task<ServiceResponse> Delete(long catalogId);
    }

}