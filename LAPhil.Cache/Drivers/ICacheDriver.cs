using System;
using System.Threading.Tasks;


namespace LAPhil.Cache
{
    public interface ICacheDriver
    {
        void Shutdown();
        Task DeleteAsync(string key);
        Task<byte[]> GetAsync(string key);
        Task<T> GetAsync<T>(string key);
        Task SetAsync(string key, byte[] value, DateTimeOffset? expiration = null);
        Task SetAsync<T>(string key, T value, DateTimeOffset? expiration = null);
    }
}
