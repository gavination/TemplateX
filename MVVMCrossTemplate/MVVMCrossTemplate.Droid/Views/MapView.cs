using Android.App;
using Android.OS;
using MvvmCross.Droid.FullFragging.Caching;
using MvvmCross.Droid.FullFragging.Fragments;
using MvvmCross.Droid.Views;
using MVVMCrossTemplate.ViewModels;

namespace MVVMCrossTemplate.Droid.Views
{
    [Activity(Label = "Map")]
    public class MapView : MvxCachingFragmentActivity<MapViewModel>
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.MapView);

           
        }
    }
}
