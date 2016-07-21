using System;
using Fusillade;
using MvvmCross.Core.ViewModels;
using MVVMCrossTemplate.Services;
using Refit;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Identity.Client;
using MVVMCrossTemplate.Model;
using MVVMCrossTemplate.Services.Infrastructure;

namespace MVVMCrossTemplate.ViewModels
{
    public class ListViewModel : MvxViewModel
    {
        private ObservableCollection<DogProfile> _dogs;
        private IDogService dogService;

        public ObservableCollection<DogProfile> Dogs
        {
            get { return _dogs; }
            set { _dogs = value; RaisePropertyChanged(() => Dogs); }
        }

        private MvxCommand<DogProfile> _itemSelectedCommand;
        private B2cAuthorizationService b2c;

        public ICommand ItemSelectedCommand
        {
            get
            {
                _itemSelectedCommand = _itemSelectedCommand ?? new MvxCommand<DogProfile>(DoSelectItem);
                return _itemSelectedCommand;
            }
        }

        private void DoSelectItem(DogProfile dog)
        {
            ShowViewModel<DetailViewModel>(dog);
        }

        // Check out viewmodel life cycle at https://github.com/MvvmCross/MvvmCross/wiki/View-Model-Lifecycle
        // also how to load async at http://stackoverflow.com/questions/28472306/what-is-the-best-async-loading-viewmodels-strategy-when-using-mvvmcross with answer from cheesebaron himself
        public override async void Start()
        {
            dogService = new DogService(new ApiService<IDogApi>("http://10.0.0.35:3000/api/", getToken));
            var dogs = await dogService.GetDogs(Priority.UserInitiated).ConfigureAwait(false);

            Dogs = new ObservableCollection<DogProfile>(dogs);

           
        }

        private async Task<string> getToken()
        {
            try
            {
                AuthenticationResult ar = await App.PCApplication.AcquireTokenSilentAsync(App.Scopes, "", App.Authority, App.SignUpSignInpolicy, false);
                return ar.Token;
            }
            catch (Exception ee)
            {
               // DisplayAlert("An error has occurred", "Exception message: " + ee.Message, "Dismiss");
                App.PCApplication.UserTokenCache.Clear(App.PCApplication.ClientId);
                return string.Empty;
            }
        }


        public ICommand BackCommand
        {
            get
            {
                return new MvxCommand(() => Close(this));
            }
        }

        public IPlatformParameters PlatformParamaters { get; set; }
    }
}