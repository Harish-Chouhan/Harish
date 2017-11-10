using System;
using System.Threading.Tasks;
using Realms;
using LAPhil.Application;
using LAPhil.Logging;

namespace LAPhil.Cache.Realm
{
    /// <summary>
    /// We want to enable the usert to be able to call these methods from
    /// any thread. Realm, by design, only wants you to interact with it
    /// from the same thread that created it. So below, at points where
    /// we persist a change to realm, you will see a new Realm being
    /// initialized with the config created at construction time.
    /// 
    /// **NOTE** 
    /// 
    /// I currently do not know the cost penalty for creating and destroying
    /// access to realms like this. For that reason, we also wrap that
    /// in a new task.
    /// </summary>
    public class RealmDriver: ICacheDriver
    {

        Logger<RealmDriver> Log = ServiceContainer.Resolve<LoggingService>().GetLogger<RealmDriver>();
        readonly ICacheSerializer Serializer;
        readonly RealmConfiguration Config;

        public RealmDriver(string path, ICacheSerializer serializer)
        {
            // limit the types of objects we will store:
            Config = new RealmConfiguration(path);
            Config.ObjectClasses = new[] { typeof(RealmCache) };
            Serializer = serializer;

            Log.Debug("Realm Cache Driver Initialized at '{Path}'", path);
        }

        public Task DeleteAsync(string key)
        {
            return Task.Run(() =>
            {
                using (var realm = Realms.Realm.GetInstance(Config))
                using (var trans = realm.BeginWrite())
                {
                    var obj = new RealmCache { Key = key };
                    realm.Remove(obj);
                    trans.Commit();
                }
            });
        }

        public Task<byte[]> GetAsync(string key)
        {
            return Task.Run(() =>
            {
                RealmCache obj = default(RealmCache);

                using(var realm = Realms.Realm.GetInstance(Config))
                using (var trans = realm.BeginWrite())
                {
                    try
                    {
                        obj = realm.Find<RealmCache>(key);
                    }
                    catch (Exception e)
                    {
                        Log.Warn("Failed to load cache key '{Key}', {Message}", e.Message);
                    }

                    if(obj != null){
                        obj.LastAccess = DateTimeOffset.UtcNow;
                        realm.Add(obj, update: true);
                        trans.Commit();

                        return obj.Value;
                    }

                    return null;
                }
            });
        }

        public Task<T> GetAsync<T>(string key)
        {
            return Task.Run(async () =>
            {
                var value = await GetAsync(key);

                if(value == null){
                    return default(T);
                }

                var result = Serializer.Deserialize<T>(value);
                return result;
            });
        }

        public Task SetAsync(string key, byte[] value, DateTimeOffset? expiration = null)
        {
            return Task.Run(() =>
            {
                using (var realm = Realms.Realm.GetInstance(Config))
                {
                    realm.Write(() =>
                    {
                        realm.Add(new RealmCache
                        {
                            Key = key,
                            Value = value,
                            LastAccess = DateTimeOffset.UtcNow
                        }, update: true);
                    });    
                }
            });
        }

        public Task SetAsync<T>(string key, T value, DateTimeOffset? expiration = null)
        {
            return Task.Run(async () =>
            {
                var bytes = Serializer.Serialize(value);
                await SetAsync(key: key, value: bytes);
            });
        }

        public void Shutdown()
        {
            
        }
    }
}
