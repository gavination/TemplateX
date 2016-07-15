using MvvmCross.Core.ViewModels;
using MVVMCrossTemplate.Model;

namespace MVVMCrossTemplate.ViewModels
{
    public class DetailViewModel : MvxViewModel
    {
        public string DogName;

        public void Init(DogProfile item)
        {
            Dog = item;

        }
        private DogProfile _dog;
        public DogProfile Dog
        {
            get { return _dog; }
            set { _dog = value; RaisePropertyChanged(() => Dog); }

        }
    }
}