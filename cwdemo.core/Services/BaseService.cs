using AutoMapper;
using cwdemo.data.Interfaces;

namespace cwdemo.core.Services
{
    public class BaseService
    {
        protected readonly IRepositories _repositories;
        protected readonly IMapper _mapper;

        public BaseService(IRepositories repositories, IMapper mapper)
        {
            this._repositories = repositories;
            this._mapper = mapper;
        }
    }
}