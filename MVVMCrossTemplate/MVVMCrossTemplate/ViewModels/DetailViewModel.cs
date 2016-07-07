using MvvmCross.Core.ViewModels;
namespace MVVMCrossTemplate.ViewModels
{
    public class DetailViewModel : MvxViewModel
    {
        public string DogName;

        public void Init(Services.Dog item)
        {
            Dog = item;

        }
        private Services.Dog _dog;
        public Services.Dog Dog
        {
            get { return _dog; }
            set { _dog = value; RaisePropertyChanged(() => Dog); }

        }
    }
}