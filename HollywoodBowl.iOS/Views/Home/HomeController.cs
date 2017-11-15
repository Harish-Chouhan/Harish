using UIKit;
using System;
using LAPhil.Application;
using LAPhil.Analytics;


namespace HollywoodBowl.iOS.Views.Home
{
    public partial class HomeController : UIViewController
    {
        AnalyticsService AnalyticsService = ServiceContainer.Resolve<AnalyticsService>();

        public HomeController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);
            AnalyticsService.TrackView("Home");
        }
    }
}

