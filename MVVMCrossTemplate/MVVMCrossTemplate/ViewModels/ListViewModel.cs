using MvvmCross.Core.ViewModels;
using System.Windows.Input;

namespace MVVMCrossTemplate.ViewModels
{
    public class ListViewModel : MvxViewModel
    {
        public ICommand BackCommand
        {
            get
            {
                return new MvxCommand(() => Close(this));
            }
        }
    }
}