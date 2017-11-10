using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Reactive.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Disposables;
using LAPhil.HTTP;
using LAPhil.Cache;
using LAPhil.Application;


namespace HollywoodBowl.Services
{

    public class ObservableCachedHttpService
    {
            
    }

    public class CachedHttpService
    {
        static HttpService HttpService = ServiceContainer.Resolve<HttpService>();
        static CacheService CacheService = ServiceContainer.Resolve<CacheService>();
        string Key;

        public CachedHttpService(string key)
        {
            key = Key;
        }


        public IObservable<T> GetObservableAsync<T>(string url, Dictionary<string, string> parameters = null, Dictionary<string, string> headers = null)
        {
            return Observable.FromAsync(async () =>
            {
                var result = await HttpService.GetResultAsync<T>(url: url, parameters: parameters, headers: headers);

                if (result.IsSuccess)
                    return result.Value;

                return default(T);
            })
            .WithCache(Key);
        }

        public IObservable<T> PostObservableAsync<T>(string url, Dictionary<string, string> data = null, IDictionary json = null, Dictionary<string, string> headers = null)
        {
            return Observable.FromAsync(async () =>
            {
                var result = await HttpService.PostResultAsync<T>(url: url, data: data, json: json, headers: headers);

                if (result.IsSuccess)
                    return result.Value;

                return default(T);
            })
            .WithCache(Key);
        }

        public IObservable<T> PutObservableAsync<T>(string url, Dictionary<string, string> data = null, IDictionary json = null, Dictionary<string, string> headers = null)
        {
            return Observable.FromAsync(async () =>
            {
                var result = await HttpService.PutResultAsync<T>(url: url, data: data, json: json, headers: headers);

                if (result.IsSuccess)
                    return result.Value;

                return default(T);
            })
            .WithCache(Key);
        }

        public Task<T> GetAsync<T>(string url, Dictionary<string, string> parameters = null, Dictionary<string, string> headers = null)
            where T: new()
        {
            var cache = Observable
                .FromAsync(async () => await CacheService.GetAsync<T>(key: Key));
            
            var request = Observable
                .FromAsync(async () => await HttpService.GetResultAsync<T>(url: url, parameters: parameters, headers: headers));

            return SendRequestAsync(cache, request);
        }

        public Task<T> PostAsync<T>(string url, Dictionary<string, string> data = null, IDictionary json = null, Dictionary<string, string> headers = null)
            where T : new()
        {
            var cache = Observable
                .FromAsync(async () => await CacheService.GetAsync<T>(key: Key));

            var request = Observable
                .FromAsync(async () => await HttpService.PostResultAsync<T>(url: url, data: data, json: json, headers: headers));

            return SendRequestAsync(cache, request);
        }

        public Task<T> PutAsync<T>(string url, Dictionary<string, string> data = null, IDictionary json = null, Dictionary<string, string> headers = null)
            where T : new()
        {
            var cache = Observable
                .FromAsync(async () => await CacheService.GetAsync<T>(key: Key));

            var request = Observable
                .FromAsync(async () => await HttpService.PutResultAsync<T>(url: url, data: data, json: json, headers: headers));

            return SendRequestAsync(cache, request);
        }

        public async Task<T> SendRequestAsync<T>(IObservable<T> cache, IObservable<Result<T>> request, int timeoutMilliseconds = 2000)
            where T: new()
        {
            request = request
                .Timeout(TimeSpan.FromMilliseconds(timeoutMilliseconds))
                .Catch<Result<T>, System.TimeoutException>(ex => Observable.Return(new Result<T>(ex)));

            var result = await cache

                // wait till we have both results from the cache and the request.
                // if no Connectivity is available the request will fail immediately
                // and `requestResult.IsSuccess` will be `false`. Along with any other
                // exceptions that may take place during the request.
                .CombineLatest(request, (cacheResult, requestResult) =>
                {
                    if (requestResult.IsSuccess)
                    {
                        // Cache it later, we specifically don't await this as we don't care 
                        // when in the immediate future this happens.
                        #pragma warning disable 4014
                        CacheService.SetAsync(Key, requestResult.Value);
                        return requestResult.Value;
                    }

                    return cacheResult;
                })

                // If both the requestResult.Value was null and the cacheResult was null
                // fallback to a `new` empty object. 
                .Select(x => EqualityComparer<T>.Default.Equals(x, default(T)) ? new T() : x);

            return result;
        }
    }

    public static class HttpServiceExtensions
    {
        public static CachedHttpService WithCache(this HttpService source, string key)
        {
            return new CachedHttpService(key: key);    
        }
    }
}
