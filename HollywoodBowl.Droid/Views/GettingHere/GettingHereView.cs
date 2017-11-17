using System;
using Android.App;
using Android.OS;
using Android.Views;


namespace HollywoodBowl.Droid.Views.GettingHere
{
    public class GettingHereView : Fragment
    {
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            return inflater.Inflate(Resource.Layout.GettingHereView, container, false);
        }
    }
}
