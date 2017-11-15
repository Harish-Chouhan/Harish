using Foundation;
using UIKit;
using System;
using LAPhil.Application;
using LAPhil.Analytics;


namespace HollywoodBowl.iOS.Views.MyTickets
{
    [Register("MyTicketsController")]
    public class MyTicketsController : UIViewController
    {
        AnalyticsService AnalyticsService = ServiceContainer.Resolve<AnalyticsService>();

        public MyTicketsController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);
            AnalyticsService.TrackView("MyTickets");
        }
    }
}
