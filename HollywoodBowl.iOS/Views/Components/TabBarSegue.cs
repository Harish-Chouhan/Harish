using Foundation;
using System;
using System.Linq;
using UIKit;


namespace HollywoodBowl.iOS.Views.Components
{
    [Register("TabBarSegue")]
    public class TabBarSegue: UIStoryboardSegue
    {
        
        public TabBarSegue(IntPtr handle) : base(handle)
        {
        }

        public TabBarSegue(String identifier, UIViewController source, UIViewController destination): 
            base(identifier, source, destination)
        {
            
        }

        public override void Perform()
        {
            var containerView = ((IContainerController)SourceViewController).ContainerView;
            var previous = SourceViewController.ChildViewControllers.Last();

            if(previous != null && previous.GetType() != typeof(TabBarController)){
                previous.RemoveFromParentViewController();
                previous.View.RemoveFromSuperview();
            }

            SourceViewController.AddChildViewController(DestinationViewController);
            containerView.AddSubview(DestinationViewController.View);
            DestinationViewController.DidMoveToParentViewController(SourceViewController);

            DestinationViewController.View.TranslatesAutoresizingMaskIntoConstraints = false;

            DestinationViewController.View.LeadingAnchor.ConstraintEqualTo(containerView.LeadingAnchor).Active = true;
            DestinationViewController.View.TrailingAnchor.ConstraintEqualTo(containerView.TrailingAnchor).Active = true;
            DestinationViewController.View.TopAnchor.ConstraintEqualTo(containerView.TopAnchor).Active = true;
            DestinationViewController.View.BottomAnchor.ConstraintEqualTo(containerView.BottomAnchor).Active = true;


        }


    }
}
