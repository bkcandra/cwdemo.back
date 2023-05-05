using AutoMapper;
using cwdemo.core.Services.Interfaces;
using cwdemo.data.Interfaces;

namespace cwdemo.core.Services
{
    public class OrderService : BaseService, IOrderService
    {
        public OrderService(IRepositories repositories, IMapper mapper) : base(repositories, mapper)
        { }
    }
}
