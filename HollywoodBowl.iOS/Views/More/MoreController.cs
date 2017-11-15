using Foundation;
using UIKit;
using System;
using LAPhil.Application;
using LAPhil.Analytics;


namespace HollywoodBowl.iOS.Views.More
{
    [Register("MoreController")]
    public class MoreController : UIViewController
    {
        AnalyticsService AnalyticsService = ServiceContainer.Resolve<AnalyticsService>();

        public MoreController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);
            AnalyticsService.TrackView("More");
        }
    }
}
