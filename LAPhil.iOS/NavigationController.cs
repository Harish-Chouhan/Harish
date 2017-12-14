using System;

using UIKit;

using Onboarding.Forms;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;


namespace LAPhil.iOS
{
    public partial class NavigationController : UINavigationController
    {
        UIViewController _history;

        public NavigationController(IntPtr handle) : base(handle)
        {
            Forms.Init();
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
        }

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);

            if (_history == null)
            {
                // #2 Use it
                _history = new CarouselPage().CreateViewController();
            }

            // And push it onto the navigation stack
            base.PushViewController(_history, true);
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.		
        }
    }
}
