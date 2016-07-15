using Fusillade;
using MvvmCross.Core.ViewModels;
using MVVMCrossTemplate.Services;
using Refit;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using MVVMCrossTemplate.Services.Infrastructure;

namespace MVVMCrossTemplate.ViewModels
{
    public class ListViewModel : MvxViewModel
    {
        private ObservableCollection<Dog> _dogs;
        private IDogService dogService;

        public ObservableCollection<Dog> Dogs
        {
            get { return _dogs; }
            set { _dogs = value; RaisePropertyChanged(() => Dogs); }
        }


        public ListViewModel()
        {
            dogService = new DogService(new ApiService<IDogApi>("http://localhost:3000/api/"));
        }
        private MvxCommand<Dog> _itemSelectedCommand;

        public ICommand ItemSelectedCommand
        {
            get
            {
                _itemSelectedCommand = _itemSelectedCommand ?? new MvxCommand<Dog>(DoSelectItem);
                return _itemSelectedCommand;
            }
        }

        private void DoSelectItem(Dog dog)
        {
            ShowViewModel<DetailViewModel>(dog);
        }

        // Check out viewmodel life cycle at https://github.com/MvvmCross/MvvmCross/wiki/View-Model-Lifecycle
        // also how to load async at http://stackoverflow.com/questions/28472306/what-is-the-best-async-loading-viewmodels-strategy-when-using-mvvmcross with answer from cheesebaron himself
        public override async void Start()
        {
            var dogs = await dogService.GetDogs(Priority.UserInitiated);

            Dogs = new ObservableCollection<Dog>(dogs.Select(x => new Dog()
                                                    {
                                                        Name = x.Breed.ToString(),
                                                        Breed = x.Description
                                                    }).ToList());
        }


        public ICommand BackCommand
        {
            get
            {
                return new MvxCommand(() => Close(this));
            }
        }
    }
}