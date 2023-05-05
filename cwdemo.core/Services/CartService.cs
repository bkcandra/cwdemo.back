using cwdemo.core.Services.Interfaces;
using cwdemo.data.Interfaces;

namespace cwdemo.core.Services
{
    public class CartService : BaseService, ICartService
    {
        public CartService(IRepositories repositories) : base(repositories)
        { }
    }
}
