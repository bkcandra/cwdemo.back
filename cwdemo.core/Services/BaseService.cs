using cwdemo.data.Interfaces;

namespace cwdemo.core.Services
{
    public class BaseService
    {
        protected IRepositories _repositories;

        public BaseService(IRepositories repositories)
        {
            this._repositories = repositories;
        }
    }
}