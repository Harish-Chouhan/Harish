using Foundation;
using UIKit;
using System;
using LAPhil.Application;
using LAPhil.Analytics;


namespace HollywoodBowl.iOS.Views.WhenHere
{
    [Register("WhenHereController")]
    public class WhenHereController : UIViewController
    {
        AnalyticsService AnalyticsService = ServiceContainer.Resolve<AnalyticsService>();

        public WhenHereController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);
            AnalyticsService.TrackView("WhenHere");
        }
    }
}
