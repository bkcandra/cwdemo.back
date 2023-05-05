using AutoMapper;
using cwdemo.core.Models;
using cwdemo.core.Services.Interfaces;
using cwdemo.data.Entities;
using cwdemo.data.Interfaces;
using cwdemo.infrastructure.Models;
using System.Net;

namespace cwdemo.core.Services
{
    public class CatalogService : BaseService, ICatalogService
    {
        public CatalogService(IRepositories repositories, IMapper mapper) : base(repositories, mapper)
        { }

        public async Task<ServiceResponse<CatalogStoreEntity>> Get(int catalogId)
        {
            var catalog = await _repositories.Catalogs.GetCatalogById(catalogId);
            if (catalog == null)
                return new ServiceResponse<CatalogStoreEntity>(false, "Not Found", (int)HttpStatusCode.NotFound);

            var store = await _repositories.Stores.GetStoreById(catalog.StoreId);
            if (store == null)
                return new ServiceResponse<CatalogStoreEntity>(false, "Not Found", (int)HttpStatusCode.NotFound);

            var catalogStore = _mapper.Map<CatalogStoreEntity>(catalog);
            catalogStore.StoreName = store.Name;

            return new ServiceResponse<CatalogStoreEntity>(catalogStore);
        }


        public async Task<ServiceResponse<List<Catalog>>> GetAll()
        {
            var catalogs = await _repositories.Catalogs.GetAllCatalogs();
            if (catalogs == null || catalogs.Count == 0)
                return new ServiceResponse<List<Catalog>>();

            var result = _mapper.Map<List<Catalog>>(catalogs);

            return new ServiceResponse<List<Catalog>>(result);
        }

        public async Task<ServiceResponse<Catalog>> Create(CreateCatalog catalog)
        {
            var store = await _repositories.Stores.GetStoreById(catalog.StoreId);
            if (store == null)
                return new ServiceResponse<Catalog>(false, "Store does not exist", (int)HttpStatusCode.NotFound);

            var newCatalog = _mapper.Map<CatalogEntity>(catalog);
            newCatalog.Active = true;

            var result = await _repositories.Catalogs.CreateCatalog(newCatalog);
            if (result == null)
                return new ServiceResponse<Catalog>(false);

            var resp = _mapper.Map<Catalog>(newCatalog);
            return new ServiceResponse<Catalog>(resp);
        }

        public async Task<ServiceResponse<Catalog>> Update(Catalog catalog)
        {
            var serviceResponse = new ServiceResponse<Catalog>();

            // Check if the catalog exists
            var existingCatalog = await _repositories.Catalogs.GetCatalogById(catalog.Id);
            if (existingCatalog == null)
            {
                serviceResponse.Valid = false;
                serviceResponse.Message.Add("Catalog not found");
                return serviceResponse;
            }

            // Map the updated catalog to the entity model
            var updatedCatalogEntity = _mapper.Map<CatalogEntity>(catalog);
            updatedCatalogEntity.Id = existingCatalog.Id; // Make sure the ID is set correctly

            // Update the existing catalog
            var updateResult = await _repositories.Catalogs.UpdateCatalog(catalog.Id, updatedCatalogEntity);
            if (!updateResult)
            {
                serviceResponse.Valid = false;
                serviceResponse.Message.Add("Failed to update catalog");
                return serviceResponse;
            }

            // Map the updated catalog back to the domain model
            var updatedCatalog = _mapper.Map<Catalog>(updatedCatalogEntity);

            serviceResponse.Content = updatedCatalog;
            serviceResponse.Message.Add("Catalog updated successfully");
            return serviceResponse;
        }

        public async Task<ServiceResponse> Delete(long catalogId)
        {
            var response = new ServiceResponse();

            var deleted = await _repositories.Catalogs.DeleteCatalog(catalogId);
            if (!deleted)
            {
                response.Message.Add($"Catalog with ID {catalogId} not found.");
                response.Valid = false;
            }
            else
            {
                response.Message.Add($"Catalog with ID {catalogId} deleted successfully.");
                response.Valid = true;
            }

            return response;
        }

    }
}