using AutoMapper;
using cwdemo.core.Models;
using cwdemo.data.Entities;

namespace cwdemo.core.Mapping
{
    public class MappingProfile : Profile
    {
        /// <summary>
        /// Initializes the mapping configuration
        /// </summary>
        public MappingProfile()
        {
            CreateMap<CatalogStoreEntity, CatalogEntity>().ReverseMap();
            CreateMap<CreateStore, StoreEntity>().ReverseMap();
            CreateMap<Store, StoreEntity>().ReverseMap();
            CreateMap<CreateStore, Store>().ReverseMap();

            CreateMap<CatalogEntity, CreateCatalog>().ReverseMap();
            CreateMap<CatalogEntity, Catalog>().ReverseMap();
            CreateMap<CreateCatalog, Catalog>().ReverseMap();



            CreateMap<CatalogStoreEntity, Catalog>().ReverseMap();

        }
    }
}
