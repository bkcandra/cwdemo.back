using cwdemo.data.Entities;

namespace cwdemo.data.Interfaces
{
    public interface ICatalogRepository
    {
        Task<CatalogEntity> GetCatalogById(long catalogId);
        Task<List<CatalogStoreEntity>> GetAllCatalogs();
        Task<CatalogEntity> CreateCatalog(CatalogEntity newCatalog);
        Task<bool> UpdateCatalog(long catalogId, CatalogEntity catalog);
        Task<bool> DeleteCatalog(long catalogId);


    }
}