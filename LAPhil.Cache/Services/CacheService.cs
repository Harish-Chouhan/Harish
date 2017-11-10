using System;
using System.Threading.Tasks;
using System.Reactive.Linq;
using Akavache;


namespace LAPhil.Cache
{
    public class CacheService
    {
        public readonly ICacheDriver Driver;

        public CacheService(ICacheDriver driver)
        {
            Driver = driver;
        }

        public void Shutdown()
        {
            Driver.Shutdown();
        }

        public async Task DeleteAsync(string key)
        {
            await Driver.DeleteAsync(key);
        }

        public async Task<byte[]> GetAsync(string key)
        {
            return await Driver.GetAsync(key);
        }

        public async Task<T> GetAsync<T>(string key)
        {
            return await Driver.GetAsync<T>(key);
        }

        public async Task SetAsync(string key, byte[] value, DateTimeOffset? expiration = null)
        {
            await Driver.SetAsync(key, value, expiration);
        }

        public async Task SetAsync<T>(string key, T value, DateTimeOffset? expiration = null)
        {
            await Driver.SetAsync(key, value, expiration);
        }
    }
}
