using Android.App;
using Android.OS;
using MvvmCross.Droid.Views;

namespace MVVMCrossTemplate.Droid.Views
{
    [Activity(Label = "List")]
    public class ListView : MvxActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.ListView);
        }
    }
}
