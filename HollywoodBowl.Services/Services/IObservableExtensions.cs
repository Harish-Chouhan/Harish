using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Reactive.Disposables;
using System.Reactive.Concurrency;
using LAPhil.Cache;
using LAPhil.Application;


namespace HollywoodBowl.Services
{
    public static class IObservableExtensions
    {
        public static IObservable<T> WithCache<T>(this IObservable<T> source, string key)
        {
            return Observable.Create<T>(observer => {

                Scheduler.ScheduleAsync(Scheduler.CurrentThread, async (scheduler, cancelToken) =>
                {
                    var cacheService = ServiceContainer.Resolve<CacheService>();
                    var cached = await cacheService.GetAsync<T>(key);

                    if (EqualityComparer<T>.Default.Equals(cached, default(T)) == false)
                        observer.OnNext(cached);

                    source.Do(value =>
                    {
                        #pragma warning disable 4014
                        if (EqualityComparer<T>.Default.Equals(value, default(T)) == false)
                            cacheService.SetAsync(key, value);
                        #pragma warning restore 4014
                        
                    }).Subscribe(value =>
                    {
                        if (EqualityComparer<T>.Default.Equals(value, default(T)) == false)
                            observer.OnNext(value);
                    }, observer.OnError, observer.OnCompleted);
                });

                return Disposable.Empty;
            });
        }
    }
}
