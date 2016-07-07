using MvvmCross.Core.ViewModels;
using MVVMCrossTemplate.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace MVVMCrossTemplate.ViewModels
{
    public class ListViewModel : MvxViewModel
    {
        private ObservableCollection<Dog> _dogs;

        public ObservableCollection<Dog> Dogs
        {
            get { return _dogs; }
            set { _dogs = value; RaisePropertyChanged(() => Dogs); }
        }


        public ListViewModel(IDogCreatorService service)
        {
            var newList = new List<Dog>();
            for (var i=0; i < 100; i++)
            {
                var newDog = service.CreateNewDog();
                newList.Add(newDog);
            }
            Dogs = new ObservableCollection<Dog>(newList);
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

        public ICommand BackCommand
        {
            get
            {
                return new MvxCommand(() => Close(this));
            }
        }

        
    }
}