using cwdemo.core.Services.Interfaces;
using cwdemo.data.Interfaces;

namespace cwdemo.core.Services
{
    public class StoreService : BaseService, IStoreService
    {
        public StoreService(IRepositories repositories) : base(repositories)
        { }
    }
}
