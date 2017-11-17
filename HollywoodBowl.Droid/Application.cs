using System;
using System.IO;
using Android.App;
using Android.Runtime;
using LAPhil.Application;
using LAPhil.Logging;
using LAPhil.Connectivity;
using LAPhil.HTTP;
using LAPhil.Cache;
using LAPhil.Cache.Realm;
using LAPhil.Analytics;
using HollywoodBowl.Services;
using HollywoodBowl.Droid.Services;

namespace HollywoodBowl.Droid
{
    
    [Application]
    public class Application: Android.App.Application
    {
        
        public Application(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
        }

        public override void OnCreate()
        {
            base.OnCreate();

            // Logging service must be initialized first and MUST NOT be in a lambda
            // create the concrete instance.

            var cacheStoragePath = PathService.CachePath;
            var cacheFilename = Path.Combine(cacheStoragePath, "cache.realm");
            PathService.CreatePath(cacheStoragePath);

            ServiceContainer.Register(new LoggingService(new Services.PlatfromLogger()));
            ServiceContainer.Register(() => new ConnectivityService());
            ServiceContainer.Register(() => new HttpService(timeout: 6.05));
            ServiceContainer.Register(() => new EventsService(new MockEventsDriver()));
            ServiceContainer.Register(() => new SeasonsService(new MockSeasonsDriver()));
            ServiceContainer.Register(() => new FavoritesService(new MockFavoritesDriver()));
            ServiceContainer.Register(() => new AnalyticsService(new MockAnalyticsDriver()));
            ServiceContainer.Register(() => new CacheService(new RealmDriver(
                path: cacheFilename,
                serializer: new JsonSerializerService()
            )));
        }
    }
}
