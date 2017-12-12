using System;
using System.IO;
using System.Collections.Generic;
using Android.App;
using Android.Runtime;
using LAPhil.Application;
using LAPhil.Logging;
using LAPhil.Connectivity;
using LAPhil.HTTP;
using LAPhil.Cache;
using LAPhil.Cache.Realm;
using LAPhil.Analytics;
using LAPhil.Routing;
using HollywoodBowl.Services;
using HollywoodBowl.Droid.Services;

namespace HollywoodBowl.Droid
{
    
    [Application]
    public class Application: Android.App.Application
    {
        ILog Log { get; set; }

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

            ServiceContainer.Register(
                new Router(children: new List<Route> {

                    new Route(path: @"/concerts/:season/:slug/2018-01-05/", action: (request) => {
                        Log.Debug($"Invoked segment 'foo' {request.Params}");
                    })
            }));

            // Testing things beyond this point
            Log = ServiceContainer.Resolve<LoggingService>().GetLogger<Application>();
            ServiceContainer.Resolve<Router>().Navigate("/concerts/2017-2018/foo-bar-baz/2018-01-05");
        }
    }
}
