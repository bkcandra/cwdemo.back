using cwdemo.core.Services;
using cwdemo.core.Services.Interfaces;
using cwdemo.infrastructure.DependencyManagement;
using Microsoft.Extensions.DependencyInjection;

namespace cwdemo.core.Infrastructure
{
    public class DependencyRegistrar : IDependencyRegistrar
    {
        public int Order => 1;

        public void Register(IServiceCollection services)
        {

            services.AddTransient<ICatalogService, CatalogService>();
            services.AddTransient<IStoreService, StoreService>();
        }
    }
}
