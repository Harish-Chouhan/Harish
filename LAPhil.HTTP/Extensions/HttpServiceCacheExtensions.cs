using System;
using System.Collections;
using System.Collections.Generic;
using System.Reactive.Linq;
using LAPhil.Application;


namespace LAPhil.HTTP
{
    public class CachedHttpService
    {
        static HttpService HttpService = ServiceContainer.Resolve<HttpService>();
        string Key;

        public CachedHttpService(string key)
        {
            Key = key;
        }


        public IObservable<T> Get<T>(string url, Dictionary<string, string> parameters = null, Dictionary<string, string> headers = null)
        {
            var request = Observable.FromAsync(async () =>
            {
                var result = await HttpService.GetResultAsync<T>(url: url, parameters: parameters, headers: headers);

                if (result.IsSuccess)
                    return result.Value;

                return default(T);
            });

            return SendRequestObservable(request);
        }

        public IObservable<T> Post<T>(string url, Dictionary<string, string> data = null, IDictionary json = null, Dictionary<string, string> headers = null)
        {
            var request = Observable.FromAsync(async () =>
            {
                var result = await HttpService.PostResultAsync<T>(url: url, data: data, json: json, headers: headers);

                if (result.IsSuccess)
                    return result.Value;

                return default(T);
            });
            return SendRequestObservable(request);
        }

        public IObservable<T> Put<T>(string url, Dictionary<string, string> data = null, IDictionary json = null, Dictionary<string, string> headers = null)
        {
            var request = Observable.FromAsync(async () =>
            {
                var result = await HttpService.PutResultAsync<T>(url: url, data: data, json: json, headers: headers);

                if (result.IsSuccess)
                    return result.Value;

                return default(T);
            });

            return SendRequestObservable(request);
        }

        public IObservable<T> SendRequestObservable<T>(IObservable<T> request, int timeoutMilliseconds = 2000)
        {
            return request
            .Timeout(TimeSpan.FromMilliseconds(timeoutMilliseconds))
            .Catch<T, System.TimeoutException>(_ => Observable.Return(default(T)))
            .WithCache(Key);
        }
    }

    public static class HttpServiceCacheExtensions
    {
        public static CachedHttpService WithCache(this HttpService source, string key)
        {
            return new CachedHttpService(key: key);
        }
    }
}
