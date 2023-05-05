using cwdemo.data.Interfaces;

namespace cwdemo.core.Services
{
    public class BaseService
    {
        protected IRepositories _repository;

        public BaseService(IRepositories repositories)
        {
            this._repository = repositories;
        }
    }
}