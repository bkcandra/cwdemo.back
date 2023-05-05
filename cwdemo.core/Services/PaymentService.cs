using AutoMapper;
using cwdemo.core.Services.Interfaces;
using cwdemo.data.Interfaces;

namespace cwdemo.core.Services
{
    public class PaymentService : BaseService, IPaymentService
    {
        public PaymentService(IRepositories repositories, IMapper mapper) : base(repositories, mapper)
        { }
    }
}
