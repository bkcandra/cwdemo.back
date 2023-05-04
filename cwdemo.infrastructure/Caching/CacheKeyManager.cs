namespace cwdemo.infrastructure.Caching
{
    public class CacheKeyManager : ICacheKeyManager
    {
        private static List<string> _cacheKeys;

        public List<string> CacheKeys => _cacheKeys ??= new List<string>();

        public void Add(string key)
        {
            if (!CacheKeys.Exists(x => x.Equals(key)))
                CacheKeys.Add(key);
        }

        public void Flush()
        {
            CacheKeys.Clear();
        }

        public void Remove(string key)
        {
            CacheKeys.Remove(key);
        }

        public void RemoveByPrefix(string prefix)
        {
            var keysToRemove = CacheKeys.Where(x => x.StartsWith(prefix, StringComparison.OrdinalIgnoreCase)).ToList();
            keysToRemove.ForEach(key => this.CacheKeys.Remove(key));
        }
    }

    public interface ICacheKeyManager
    {
        List<string> CacheKeys { get; }

        void Add(string key);

        void Remove(string key);

        void RemoveByPrefix(string prefix);
        void Flush();
    }
}