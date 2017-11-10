using System;
using System.Threading.Tasks;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Disposables;
using LAPhil.Application;

namespace LAPhil.Cache
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

                    if (Equals(cached, default(T)) == false)
                        observer.OnNext(cached);

                    source.Do(value =>
                    {
                        if (value != null)
                            cacheService.SetAsync(key, value);
                    }).Subscribe(value =>
                    {
                        if (value != null)
                            observer.OnNext(value);
                    }, observer.OnError, observer.OnCompleted);

                    //source
                    //.SelectMany(async (value) => {
                    //    await cacheService.SetAsync<T>(key, value); 
                    //    return value;
                    //}).Subscribe(user => {
                    //    observer.OnNext(user);
                    //}, observer.OnError, observer.OnCompleted);
                });

                return Disposable.Empty;
            });
        }
    }
}
