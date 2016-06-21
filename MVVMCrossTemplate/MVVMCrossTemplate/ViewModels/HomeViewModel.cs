using MvvmCross.Core.ViewModels;

namespace MVVMCrossTemplate.ViewModels
{
    public class HomeViewModel
        : MvxViewModel
    {

        public IMvxCommand GoToList
        {
            get
            {

                {
                    return new MvxCommand(() => ShowViewModel<ListViewModel>());
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
