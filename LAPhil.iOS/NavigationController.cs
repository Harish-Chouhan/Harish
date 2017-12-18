using System;

using UIKit;

using Login.Forms;
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

            // Hide the navigation bar on the this view controller
            base.SetNavigationBarHidden(true, true);
            /* harish
            if (_history == null)
            {
                // #2 Use it
                //_history = new Onboarding.Forms.Onboarding().CreateViewController();
                _history = new Login.Forms.LoginPage().CreateViewController();
                //_history.View.Frame = base.View.Frame;
            }

            // And push it onto the navigation stack
            base.PushViewController(_history, true);
            */
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.		
        }
    }
}
