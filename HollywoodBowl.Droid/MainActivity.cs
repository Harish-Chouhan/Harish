using System;
using Android.App;
using Android.Widget;
using Android.OS;
using Android.Support.V7.App;
using System.Reactive;
using System.Reactive.Linq;
using LAPhil.Logging;
using LAPhil.Application;
using HollywoodBowl.Droid.Views.Components;



namespace HollywoodBowl.Droid
{
    
    [Activity(Label = "HollywoodBowl", MainLauncher = true, Icon = "@mipmap/icon", Theme = "@style/AppTheme")]
    public class MainActivity : AppCompatActivity
    {
        TabBar TabBarView { get; set; }
        ILog Log = ServiceContainer.Resolve<LoggingService>().GetLogger<MainActivity>();


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            TabBarView = FindViewById<TabBar>(Resource.Id.HWBTabBar);

            TabBarView.Rx.Click.Subscribe((button) =>
            {
                Log.Debug($"Button Clicked: '{button.Text}'");
            });
        }
    }
}

