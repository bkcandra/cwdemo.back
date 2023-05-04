using EasyCaching.Core;

namespace cwdemo.infrastructure.Caching
{
    /// <summary>
    /// Represents a memory cache manager
    /// </summary>
    public partial class MemoryCacheManager : ILocker, IStaticCacheManager
    {
        #region Fields

        private readonly IEasyCachingProvider _provider;

        private readonly ICacheKeyManager _cacheKeys;

        #endregion Fields

        #region Ctor

        public MemoryCacheManager(IEasyCachingProvider provider, ICacheKeyManager cacheKeys)
        {
            this._provider = provider;
            this._cacheKeys = cacheKeys;
        }

        #endregion Ctor

        #region Methods

        /// <summary>
        /// Get a cached item. If it's not in the cache yet, then load and cache it
        /// </summary>
        /// <typeparam name="T">Type of cached item</typeparam>
        /// <param name="key">Cache key</param>
        /// <param name="acquire">Function to load item if it's not in the cache yet</param>
        /// <param name="cacheTime">Cache time in minutes; pass 0 to do not cache; pass null to use the default time</param>
        /// <returns>The cached value associated with the specified key</returns>
        public T Get<T>(string key, Func<T> acquire, int? cacheTime = null)
        {
            if (cacheTime <= 0)
            {
                return acquire();
            }

            this._cacheKeys.Add(key);
            return this._provider.Get(key, acquire, TimeSpan.FromMinutes(cacheTime ?? CachingDefaults.CacheTime))
                .Value;
        }

        public T Get<T>(string key)
        {
            return this._provider.Get<T>(key).Value;
        }

        /// <summary>
        /// Returns all items by key prefix
        /// </summary>
        /// <param name="prefix">String key prefix</param>
        public IList<T> GetByPrefix<T>(string prefix)
        {
            var cacheValues = this._provider.GetByPrefix<T>(prefix).Select(x => x.Value);
            return cacheValues.Select(x => x.Value).ToList();
        }

        /// <summary>
        /// Get a cached item. If it's not in the cache yet, then load and cache it
        /// </summary>
        /// <typeparam name="T">Type of cached item</typeparam>
        /// <param name="key">Cache key</param>
        /// <param name="acquire">Function to load item if it's not in the cache yet</param>
        /// <param name="cacheTime">Cache time in minutes; pass 0 to do not cache; pass null to use the default time</param>
        /// <returns>The cached value associated with the specified key</returns>
        public async Task<T> GetAsync<T>(string key, Func<Task<T>> acquire, int? cacheTime = null)
        {
            if (cacheTime <= 0)
            {
                return await acquire();
            }

            var t = await this._provider.GetAsync(key, acquire, TimeSpan.FromMinutes(cacheTime ?? CachingDefaults.CacheTime));
            this._cacheKeys.Add(key);
            return t.Value;
        }

        /// <summary>
        /// Adds the specified key and object to the cache
        /// </summary>
        /// <param name="key">Key of cached item</param>
        /// <param name="data">Value for caching</param>
        /// <param name="cacheTime">Cache time in minutes</param>
        public void Set(string key, object data, int cacheTime)
        {
            if (cacheTime <= 0)
            {
                return;
            }

            this._provider.Set(key, data, TimeSpan.FromMinutes(cacheTime));
            this._cacheKeys.Add(key);
        }

        /// <summary>
        /// Gets a value indicating whether the value associated with the specified key is cached
        /// </summary>
        /// <param name="key">Key of cached item</param>
        /// <returns>True if item already is in cache; otherwise false</returns>
        public bool IsSet(string key)
        {
            return this._provider.Exists(key);
        }

        /// <summary>
        /// Perform some action with exclusive in-memory lock
        /// </summary>
        /// <param name="key">The key we are locking on</param>
        /// <param name="expirationTime">The time after which the lock will automatically be expired</param>
        /// <param name="action">Action to be performed with locking</param>
        /// <returns>True if lock was acquired and action was performed; otherwise false</returns>
        public bool PerformActionWithLock(string key, TimeSpan expirationTime, Action action)
        {
            //ensure that lock is acquired
            if (this._provider.Exists(key))
            {
                return false;
            }

            try
            {
                this._provider.Set(key, key, expirationTime);

                //perform action
                action();

                return true;
            }
            finally
            {
                //release lock even if action fails
                this.Remove(key);
            }
        }

        /// <summary>
        /// Removes the value with the specified key from the cache
        /// </summary>
        /// <param name="key">Key of cached item</param>
        public void Remove(string key)
        {
            this._provider.Remove(key);
            this._cacheKeys.RemoveByPrefix(key);
        }

        /// <summary>
        /// Removes items by key prefix
        /// </summary>
        /// <param name="prefix">String key prefix</param>
        public void RemoveByPrefix(string prefix)
        {
            this._provider.RemoveByPrefix(prefix);
            this._cacheKeys.RemoveByPrefix(prefix);
        }

        /// <summary>
        /// Clear all cache data
        /// </summary>
        public void Clear()
        {
            this._provider.Flush();
            this._cacheKeys.Flush();
        }

        /// <summary>
        /// Dispose cache manager
        /// </summary>
        public virtual void Dispose()
        {
            //nothing special
        }

        public List<string> CacheKeys => this._cacheKeys.CacheKeys;

        #endregion Methods
    }
}