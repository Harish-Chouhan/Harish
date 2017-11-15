using Foundation;
using System;
using UIKit;
using LAPhil.Logging;
using LAPhil.Application;
using System.Reactive.Linq;
using System.Reactive.Subjects;


namespace HollywoodBowl.iOS.Views.Components
{
    
    [Register("TabBarController")]
    public class TabBarController : UIViewController
    {
        public sealed class _Rx{
            internal Subject<IObservable<TabBarButton>> Buttons = new Subject<IObservable<TabBarButton>>();
            internal IObservable<TabBarButton> ClickMerge;
            public IObservable<TabBarButton> Click => ClickMerge;

            internal _Rx(){
                ClickMerge = Observable
                    .Merge(Buttons)
                    .Where(x => x.IsSelected == false)
                    .Scan(seed: default(TabBarButton), accumulator: (previous, latest) =>
                    {
                        if (previous != null) 
                            previous.IsSelected = false;
                        
                        latest.IsSelected = true;
                        return latest;
                    });
            }
        }

        ILog Log = ServiceContainer.Resolve<LoggingService>().GetLogger<TabBarController>();
        public _Rx Rx = new _Rx();

        public TabBarController(IntPtr handle) : base(handle)
        {

        }

        public override void PrepareForSegue(UIStoryboardSegue segue, NSObject sender)
        {
            base.PrepareForSegue(segue, sender);
            var button = (TabBarButton)segue.DestinationViewController;
            Rx.Buttons.OnNext(button.Rx.Click);
        }

    }
}

