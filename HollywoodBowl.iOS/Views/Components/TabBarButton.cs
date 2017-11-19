using Foundation;
using System;
using UIKit;
using System.Reactive.Subjects;


namespace HollywoodBowl.iOS.Views.Components
{
    [Register("TabBarButton")]
    public class TabBarButton : UIViewController
    {
        public sealed class _Rx
        {
            internal Subject<TabBarButton> ClickSubject = new Subject<TabBarButton>();
            public IObservable<TabBarButton> Click => ClickSubject;
        }


        [Export("Label")]
        public string Label { get; internal set; }
        public bool IsSelected {get; set;}
		public _Rx Rx = new _Rx();
		
        UITapGestureRecognizer Click;

        public TabBarButton(IntPtr handle) : base(handle)
        {
            
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            Click = new UITapGestureRecognizer(OnClick);
            View.AddGestureRecognizer(Click);
        }

        void OnClick(UITapGestureRecognizer sender)
        {
            if(sender.State == UIGestureRecognizerState.Ended)
            {
                Rx.ClickSubject.OnNext(this);
            }
        }
    }
}

