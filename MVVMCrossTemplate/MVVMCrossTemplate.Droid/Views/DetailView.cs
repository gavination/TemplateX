using Android.App;
using Android.OS;
using MvvmCross.Binding.Droid.Views;
using MvvmCross.Droid.Views;

namespace MVVMCrossTemplate.Droid.Views
{
    [Activity(Label = "List")]
    public class DetailView : MvxActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.DetailView);
        }



    }
}
