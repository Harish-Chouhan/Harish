using System;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
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
using Android.OS;

namespace HollywoodBowl.Droid
{
    
    [Application]
    public class Application: Android.App.Application, Android.App.Application.IActivityLifecycleCallbacks
    {
        ILog Log { get; set; }

        public Application(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
        }

        public override void OnCreate()
        {
            base.OnCreate();
            RegisterActivityLifecycleCallbacks(this);

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
            ServiceContainer.Register(() => new CurrentActivityService());
            ServiceContainer.Register(() => new CacheService(new RealmDriver(
                path: cacheFilename,
                serializer: new JsonSerializerService()
            )));

            ServiceContainer.Register(
                new Router(children: new List<Route> {

                    new Route(path: @"/concerts/:season/:slug/2018-01-05/", action: (request) => {
                        Log.Debug($"Invoked segment 'foo' {request.Params}");
                        
                        var bundle  = new Bundle();
                        bundle.PutString("season", request.Params["season"]);
                        bundle.PutString("slug", request.Params["slug"]);

                        var activity = (MainActivity) ServiceContainer.Resolve<CurrentActivityService>().Activity;
                        activity.ShowSection(Resource.Id.TabNavigation3, bundle);
                    })
            }));

            // Testing things beyond this point
            Log = ServiceContainer.Resolve<LoggingService>().GetLogger<Application>();

            Task.Run(() => {
                //await Task.Delay(2000);
                ServiceContainer.Resolve<Router>().Navigate("/concerts/2017-2018/foo-bar-baz/2018-01-05");    
            });

        }

        public void OnActivityDestroyed(Activity activity){}
        public void OnActivityPaused(Activity activity){}
        public void OnActivitySaveInstanceState(Activity activity, Bundle outState) {}
        public void OnActivityStopped(Activity activity) {}


        public void OnActivityResumed(Activity activity)
        {
            ServiceContainer.Resolve<CurrentActivityService>().Activity = activity;
        }

        public void OnActivityCreated(Activity activity, Bundle savedInstanceState)
        {
            ServiceContainer.Resolve<CurrentActivityService>().Activity = activity;
        }

        public void OnActivityStarted(Activity activity)
        {
            ServiceContainer.Resolve<CurrentActivityService>().Activity = activity;
        }


    }
}
