using System;
using System.Diagnostics;
using Microsoft.Identity.Client;
using MvvmCross.Core.ViewModels;

namespace MVVMCrossTemplate.ViewModels
{
    public class HomeViewModel
        : MvxViewModel
    {
        public IPlatformParameters PlatformParamaters { get; set; }

        public override async void Start()
        {
            try
            {
                AuthenticationResult ar = await App.PCApplication.AcquireTokenSilentAsync(App.Scopes, "", App.Authority, App.SignUpSignInpolicy, false);
                Debug.WriteLine("logged in already");
            }
            catch
            {
                Debug.WriteLine("");
                // doesn't matter, we go in interactive more
            }


        }

        public IMvxCommand GoToList
        {
            get
            {

                {
                    return new MvxCommand(() => ShowViewModel<ListViewModel>());
                }
            }
        }

        public IMvxAsyncCommand SignIn
        {
            get
            {

                {
                    return new MvxAsyncCommand(async () =>
                    {
                        try
                        {
                            App.PCApplication.PlatformParameters = PlatformParamaters;
                            AuthenticationResult ar = await App.PCApplication.AcquireTokenAsync(App.Scopes, "", UiOptions.SelectAccount, string.Empty, null, App.Authority, App.SignUpSignInpolicy);
                        }
                        catch (Exception ee)
                        {
                            Debug.WriteLine("error");
                        }       
                    });
                }
            }
        }

        public IMvxCommand GoToMap
        {
            get
            {

                {
                    return new MvxCommand(() => ShowViewModel<MapViewModel>());
                }
            }
        }
    }
}
