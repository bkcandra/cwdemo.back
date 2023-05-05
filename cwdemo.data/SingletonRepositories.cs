using cwdemo.data.Interfaces;
using cwdemo.data.Repositories;

namespace cwdemo.data
{
    public class SingletonRepositories : IRepositories
    {
        public ICartRepository Carts => throw new NotImplementedException();

        public ICatalogRepository Catalogs => new CatalogRepository();

        public ICustomerRepository Customers => throw new NotImplementedException();

        public IOrderRepository Orders => throw new NotImplementedException();

        public IPaymentRepository Payments => throw new NotImplementedException();

        public IStoreRepository Stores => new StoreRepository();
    }
}
