using Foundation;
using System;
using UIKit;
using System.Reactive.Linq;
using LAPhil.Application;
using LAPhil.Logging;
using HollywoodBowl.iOS.Views.Components;

namespace HollywoodBowl.iOS.Views.Root
{
    public partial class RootController : UIViewController, IContainerController
    {
        ILog Log = ServiceContainer.Resolve<LoggingService>().GetLogger<RootController>();
        new TabBarController TabBarController;

        public RootController(IntPtr handle) : base(handle)
        {
        
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            PerformSegue("Home", this);
        }

        public override void PrepareForSegue(UIStoryboardSegue segue, NSObject sender)
        {
            base.PrepareForSegue(segue, sender);

            if(segue.Identifier == "TabBarController"){
                TabBarController = (TabBarController)segue.DestinationViewController;

                TabBarController.Rx.Click.Subscribe((button) =>
                {
                    Log.Debug($"Button Clicked: '{button.Label}'");
                    PerformSegue(button.Label, this);
                });
            }
        }
    }
}

