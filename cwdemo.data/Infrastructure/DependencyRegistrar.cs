using cwdemo.data.Interfaces;
using cwdemo.infrastructure.DependencyManagement;
using Microsoft.Extensions.DependencyInjection;

namespace cwdemo.data.Infrastructure
{
    public class DependencyRegistrar : IDependencyRegistrar
    {
        public int Order => 4;

        public void Register(IServiceCollection services)
        {

            services.AddTransient<IRepositories, SingletonRepositories>();
        }
    }
}
