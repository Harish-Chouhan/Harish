using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Polly.Caching;
using Newtonsoft.Json;


namespace LAPhil.Cache
{
    public class LAPhilJsonSerializer<T> : ICacheItemSerializer<T, string>
    {
        public string Serialize(T objectToSerialize)
        {
            return JsonConvert.SerializeObject(objectToSerialize);
        }

        public T Deserialize(string objectToDeserialize)
        {
            return JsonConvert.DeserializeObject<T>(objectToDeserialize);
        }
    }

    public class LAPhilCacheProvider: IAsyncCacheProvider<string>
    {
        public readonly ICacheDriver Driver;

        public LAPhilCacheProvider(ICacheDriver driver)
        {
            Driver = driver;
        }

        public async Task<string> GetAsync(string key, CancellationToken cancellationToken, bool continueOnCapturedContext)
        {
            var bytes = await Driver
                .GetAsync(key)
                .ConfigureAwait(continueOnCapturedContext);

            return Encoding.UTF8.GetString(bytes);
        }

        public Task PutAsync(string key, string value, Ttl ttl, CancellationToken cancellationToken, bool continueOnCapturedContext)
        {
            
            return Driver.SetAsync(key, Encoding.UTF8.GetBytes(value), new DateTimeOffset(DateTime.UtcNow, ttl.Timespan));

        }
    }

}
