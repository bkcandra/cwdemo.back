using AutoMapper;
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
        }
    }
}
