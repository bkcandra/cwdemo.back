using cwdemo.core.Services.Interfaces;
using cwdemo.data.Interfaces;

namespace cwdemo.core.Services
{
    public class CatalogService : BaseService, ICatalogService
    {
        public CatalogService(IRepositories repositories) : base(repositories)
        { }
    }
}
