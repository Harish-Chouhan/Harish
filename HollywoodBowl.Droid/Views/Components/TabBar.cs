
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using LAPhil.Logging;
using LAPhil.Application;


namespace HollywoodBowl.Droid.Views.Components
{
    [Register("HollywoodBowl.Droid.Views.Components.TabBar")] 
    public class TabBar : LinearLayout
    {

        public sealed class _Rx
        {
            internal Subject<IObservable<TabBarButton>> Buttons = new Subject<IObservable<TabBarButton>>();
            internal IObservable<TabBarButton> ClickMerge;
            public IObservable<TabBarButton> Click => ClickMerge;

            internal _Rx()
            {
                var publisher = Observable
                    .Merge(Buttons)
                    .Where(x => x.IsSelected == false)
                    .Scan(seed: default(TabBarButton), accumulator: (previous, latest) =>
                    {
                        if (previous != null)
                            previous.IsSelected = false;

                        latest.IsSelected = true;
                        return latest;
                    })
                    .Publish();

                // We want this to be Connectable as we want it to
                // immediately be available for Lifecycle reasons.
                // Since we Subscribe in the `MainActivity` in `OnCreated`
                // which happens AFTER `OnFinishInflate`. Which basically
                // means it's a dead observable. Making it `Connectable`
                // and immediately `Connect()`ing to it means it's ready
                // to go for anything.
                //
                // If we wanted to wait
                // we could override `OnAttachedToWindow` then 
                // register the `TabBatButtons` as well.

                publisher.Connect();
                ClickMerge = publisher.AsObservable();

            }
        }

        ILog Log;
        public _Rx Rx = new _Rx();

        public TabBar(Context context) :
            base(context)
        {
            Initialize();
        }

        public TabBar(Context context, IAttributeSet attrs) :
            base(context, attrs)
        {
            Initialize();
        }

        public TabBar(Context context, IAttributeSet attrs, int defStyle) :
            base(context, attrs, defStyle)
        {
            Initialize();
        }

        void Initialize()
        {
            
            #pragma warning disable
            try
            {
                Log = ServiceContainer.Resolve<LoggingService>().GetLogger<TabBar>();

            } catch {
                // Visual Studio runs part of the code to show the preview
                // It doesn't appear to run Application.cs though which registers
                // things into our `ServiceContainer`. If we place this 
                // at the class level, like we do for iOS we can't preview anymore
                // as we get a runtime error since nothing is registered.
            }
            #pragma warning restore

            LayoutInflater inflater = (LayoutInflater)Context
                .GetSystemService(Context.LayoutInflaterService);

            inflater.Inflate(Resource.Layout.HWBTabBarContainer, root: this);
        }

        protected override void OnFinishInflate()
        {
            base.OnFinishInflate();

            Rx.Buttons.OnNext(FindViewById<TabBarButton>(Resource.Id.HWBTabNavigation1).Rx.Click);
            Rx.Buttons.OnNext(FindViewById<TabBarButton>(Resource.Id.HWBTabNavigation2).Rx.Click);
            Rx.Buttons.OnNext(FindViewById<TabBarButton>(Resource.Id.HWBTabNavigation3).Rx.Click);
            Rx.Buttons.OnNext(FindViewById<TabBarButton>(Resource.Id.HWBTabNavigation4).Rx.Click);
            Rx.Buttons.OnNext(FindViewById<TabBarButton>(Resource.Id.HWBTabNavigation5).Rx.Click);
        }
    }
}
