using System;
using System.Threading.Tasks;
using Akavache;
using System.Reactive.Linq;
using Akavache.Sqlite3;
using LAPhil.Application;
using LAPhil.Logging;


namespace LAPhil.Cache.Akavache
{
    /*
     * Akavache 5.0 has a dependency on ReactiveUI which itself depends on RX.NET 2.5
     * If you wanted to use RX.NET 3.1.1 you couldn't. We do make use of RX.NET 3.1.1
     * So we have an incompatibility with Akavache 5.0. This seems to be resolve with
     * Akavache 6.0 (alpha 0038). That ran into some runtime issues in Xamarin.iOS not 
     * being able to load DLLs.
     * 
     * SO...
     * 
     * We are keeping this around in the event that maybe it's useful later? 
     * To handle Caching we are instead using Realm. 
     * 
     * Now you might be saying, doesn't that add to the overall appliction size 
     * since SQLite (Akavache) is already part of both iOS and Android. The answer
     * is yes, about 5-8mb:
     * https://realm.io/docs/xamarin/latest/#file-size--tracking-of-intermediate-versions
     * 
     * So there you have it. That's how we landed on using Realm vs Akavache. If they 
     * fix Akavache we can just switch back.
     * 
     * */
    public class AkavacheDriver: ICacheDriver
    {
        Logger<AkavacheDriver> Log = ServiceContainer.Resolve<LoggingService>().GetLogger<AkavacheDriver>();
        readonly ICacheSerializer Serializer;

        readonly SQLitePersistentBlobCache Cache;

        public AkavacheDriver(string path, ICacheSerializer serializer)
        {

            Cache = new SQLitePersistentBlobCache(path);
            Serializer = serializer;
            Log.Debug("Akavache Cache Driver Initialized at '{Path}'", path);
        }

        public void Shutdown()
        {
            Cache.Shutdown.Wait();
        }

        public async Task DeleteAsync(string key)
        {
            await Cache.Invalidate(key);
        }

        public async Task<byte[]> GetAsync(string key)
        {
            return await Cache.Get(key);
        }

        public async Task<T> GetAsync<T>(string key)
        {
            var result = await GetAsync(key);
            return Serializer.Deserialize<T>(result);
        }

        public async Task SetAsync(string key, byte[] value, DateTimeOffset? expiration = null)
        {
            await Cache.Insert(key, value, expiration);
        }

        public async Task SetAsync<T>(string key, T value, DateTimeOffset? expiration = null)
        {
            var bytes = Serializer.Serialize(value);
            await Cache.Insert(key, bytes, expiration);
        }
    }
}
