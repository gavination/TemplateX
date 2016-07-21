using Android.App;
using Android.Content;
using Android.OS;
using Microsoft.Identity.Client;
using MvvmCross.Droid.Views;
using MVVMCrossTemplate.ViewModels;

namespace MVVMCrossTemplate.Droid.Views
{
    [Activity(Label = "Home")]
    public class HomeView : MvxActivity<HomeViewModel>
    {

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.HomeView);
            this.ViewModel.PlatformParamaters = new PlatformParameters(this.ToActivity());
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            AuthenticationAgentContinuationHelper.SetAuthenticationAgentContinuationEventArgs(requestCode, resultCode, data);
        }
    }
}
