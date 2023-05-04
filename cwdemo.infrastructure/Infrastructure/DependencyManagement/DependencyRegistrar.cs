using cwdemo.infrastructure.Caching;
using Microsoft.Extensions.DependencyInjection;

namespace cwdemo.infrastructure.DependencyManagement
{
    public static class DependencyRegistrar
    {
        public static void RegisterDependencies(this IServiceCollection services)
        {
            var typeFinder = new WebAppTypeFinder();
            //find dependency registrars provided by other assemblies
            var dependencyRegistrars = typeFinder.FindClassesOfType<IDependencyRegistrar>();

            //create and sort instances of dependency registrars
            var instances = dependencyRegistrars
                .Select(dependencyRegistrar => (IDependencyRegistrar)Activator.CreateInstance(dependencyRegistrar))
                .OrderBy(dependencyRegistrar => dependencyRegistrar.Order);

            //register all provided dependencies
            foreach (var dependencyRegistrar in instances)
            {
                dependencyRegistrar.Register(services);
            }
        }

        /// <summary>
        /// Register easy caching  as memory cache manager
        /// </summary>
        /// <param name="services"></param>
        public static void MemoryCacheManager(this IServiceCollection services)
        {
            // Add caching service
            services.AddEasyCaching(option =>
            {
                //use memory cache
                option.UseInMemory("Mongo_Memory_Cache");
            });
            services.AddSingleton<IStaticCacheManager, MemoryCacheManager>();
            services.AddSingleton<ILocker, MemoryCacheManager>();
            services.AddSingleton<ICacheKeyManager, CacheKeyManager>();

        }

        public static void UseRestClient(this IServiceCollection services)
        {

        }
    }
}
