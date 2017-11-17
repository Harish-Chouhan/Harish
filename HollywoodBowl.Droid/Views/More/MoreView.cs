using System;
using Android.App;
using Android.OS;
using Android.Views;


namespace HollywoodBowl.Droid.Views.More
{
    public class MoreView : Fragment
    {
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            return inflater.Inflate(Resource.Layout.MoreView, container, false);
        }
    }
}
