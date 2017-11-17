using System;
using Android.Content;
using Android.Runtime;
using Android.Util;
using Android.Widget;
using System.Reactive.Subjects;



namespace HollywoodBowl.Droid.Views.Components
{
    [Register("HollywoodBowl.Droid.Views.Components.HollywoodBowl")] 
    public class TabBarButton : TextView
    {
        public sealed class _Rx
        {
            internal Subject<TabBarButton> ClickSubject = new Subject<TabBarButton>();
            public IObservable<TabBarButton> Click => ClickSubject;
        }

        public bool IsSelected { get; set; }
        public _Rx Rx = new _Rx();


        public TabBarButton(Context context) :
            base(context)
        {
            Initialize();
        }

        public TabBarButton(Context context, IAttributeSet attrs) :
            base(context, attrs)
        {
            Initialize();
        }

        public TabBarButton(Context context, IAttributeSet attrs, int defStyle) :
            base(context, attrs, defStyle)
        {
            Initialize();
        }

        void Initialize()
        {
            Clickable = true;
            IsSelected = false;
            Click += OnClick;

        }

        void OnClick(object sender, EventArgs e)
        {
            Rx.ClickSubject.OnNext(this);
        }
    }
}
