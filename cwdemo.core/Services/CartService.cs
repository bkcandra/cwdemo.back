using cwdemo.core.Models;
using cwdemo.core.Services.Interfaces;
using cwdemo.data.Interfaces;
using cwdemo.infrastructure.Models;

namespace cwdemo.core.Services
{
    public class CartService : BaseService, ICartService
    {
        public CartService(IRepositories repositories) : base(repositories)
        {
        }

        public async Task<ServiceResponse<Cart>> Get(int cartId)
        {
            return new ServiceResponse<Cart>();
        }
        public async Task<ServiceResponse<List<Cart>>> GetaAll()
        {
            return new ServiceResponse<List<Cart>>();
        }
    }
}