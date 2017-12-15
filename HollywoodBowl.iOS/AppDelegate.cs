using Foundation;
using UIKit;
using System;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Concurrency;
using System.Threading.Tasks;
using System.IO;
using System.Collections.Generic;
using System.Reactive.Linq;
using LAPhil.Application;
using LAPhil.Logging;
using LAPhil.Connectivity;
using LAPhil.HTTP;
using LAPhil.Cache;
using LAPhil.Cache.Realm;
using LAPhil.Analytics;
using LAPhil.Routing;
using LAPhil.Settings;
using LAPhil.Settings.Realm;
using HollywoodBowl.Services;
using HollywoodBowl.iOS.Services;


namespace HollywoodBowl.iOS
{
    
    [Register("AppDelegate")]
    public class AppDelegate : UIApplicationDelegate
    {
        ILog Log { get; set; }

        UIViewController _onboardingViewController;


        public override UIWindow Window
        {
            get;
            set;
        }

        public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
        {
            // Logging service must be initialized first and MUST NOT be in a lambda
            // create the concrete instance.

            var cacheStoragePath = PathService.CachePath;
            var settingsStoragePath = PathService.SettingsPath;

            var cacheFilename = Path.Combine(cacheStoragePath, "cache.realm");
            var settingsFilename = Path.Combine(settingsStoragePath, "settings.realm");

            PathService.CreatePath(cacheStoragePath);
            PathService.CreatePath(settingsStoragePath);

            ServiceContainer.Register(new LoggingService(new Services.PlatfromLogger()));
            ServiceContainer.Register(() => new ConnectivityService());
            ServiceContainer.Register(() => new HttpService(timeout: 6.05));
            ServiceContainer.Register(() => new EventsService(new MockEventsDriver()));
            ServiceContainer.Register(() => new SeasonsService(new MockSeasonsDriver()));
            ServiceContainer.Register(() => new FavoritesService(new MockFavoritesDriver()));
            ServiceContainer.Register(() => new AnalyticsService(new MockAnalyticsDriver()));

            ServiceContainer.Register(() => new SettingsService(new LAPhil.Settings.Realm.RealmDriver(
                path: settingsFilename
            )));

            ServiceContainer.Register(() => new CacheService(new LAPhil.Cache.Realm.RealmDriver(
                path: cacheFilename, 
                serializer: new JsonSerializerService()
            )));

            ServiceContainer.Register(
                new Router(children: new List<Route> {

                    new Route(path: @"/concerts/:season/:slug/2018-01-05/", action: (request) => {
                        Log.Debug($"Invoked segment 'foo' {request.Params}");
                    })
            }));

            Log = ServiceContainer.Resolve<LoggingService>().GetLogger<AppDelegate>();

            // Testing things beyond this point
            //ServiceContainer.Resolve<Router>().Navigate("/concerts/2017-2018/foo-bar-baz/2018-01-05");

            //Task.Run(async () => {
            //    var settingsService = ServiceContainer.Resolve<SettingsService>();
            //    var settings = await settingsService.AppSettings();
            //    settings.IsFirstRun = false;
            //    await settingsService.Write(settings);

            //});


            //Task.Run(() => {
            //    var eventsService = ServiceContainer.Resolve<EventsService>();

            //    eventsService.InRange(DateTime.Today, DateTime.Today)
            //     .Subscribe((value) =>
            //     {
            //         var foo = 1;
            //     });
            //});

            Forms.Init();

            if (_historyViewController == null)
            {
                // #2 ADD
                _historyViewController = new HistoryPage().CreateViewController();
            }

            _navigation.PushViewController(_historyViewController, true);

            return true;
        }

        public override void OnResignActivation(UIApplication application)
        {
            // Invoked when the application is about to move from active to inactive state.
            // This can occur for certain types of temporary interruptions (such as an incoming phone call or SMS message) 
            // or when the user quits the application and it begins the transition to the background state.
            // Games should use this method to pause the game.
        }

        public override void DidEnterBackground(UIApplication application)
        {
            
            // Use this method to release shared resources, save user data, invalidate timers and store the application state.
            // If your application supports background exection this method is called instead of WillTerminate when the user quits.
        }

        public override void WillEnterForeground(UIApplication application)
        {
            // Called as part of the transiton from background to active state.
            // Here you can undo many of the changes made on entering the background.
        }

        public override void OnActivated(UIApplication application)
        {
            // Restart any tasks that were paused (or not yet started) while the application was inactive. 
            // If the application was previously in the background, optionally refresh the user interface.
        }

        public override void WillTerminate(UIApplication application)
        {
            ServiceContainer.Resolve<LoggingService>().Shutdown();
            ServiceContainer.Resolve<CacheService>().Shutdown();
            // Called when the application is about to terminate. Save data, if needed. See also DidEnterBackground.
        }
    }
}

