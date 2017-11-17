using System;
using Android.App;
using Android.OS;
using Android.Views;


namespace HollywoodBowl.Droid.Views.WhenHere
{
    public class WhenHereView : Fragment
    {
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            return inflater.Inflate(Resource.Layout.WhenHereView, container, false);
        }
    }
}
