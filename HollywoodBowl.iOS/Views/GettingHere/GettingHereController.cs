using Foundation;
using UIKit;
using System;
using LAPhil.Application;
using LAPhil.Analytics;

namespace HollywoodBowl.iOS.Views.GettingHere
{
    [Register("GettingHereController")]
    public class GettingHereController: UIViewController
    {
        AnalyticsService AnalyticsService = ServiceContainer.Resolve<AnalyticsService>();

        public GettingHereController(IntPtr handle): base(handle)
        {
        }

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);
            AnalyticsService.TrackView("GettingHere");
        }
    }
}
