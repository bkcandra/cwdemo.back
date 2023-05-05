namespace cwdemo.data.Interfaces
{
    /// <summary>
    /// Repositories for Account API
    /// </summary>
    public interface IRepositories
    {
        ICartRepository Carts { get; }
        ICatalogRepository Catalogs { get; }
        ICustomerRepository Customers { get; }
        IOrderRepository Orders { get; }
        IPaymentRepository Payments { get; }
        IStoreRepository Stores { get; }
    }
}
