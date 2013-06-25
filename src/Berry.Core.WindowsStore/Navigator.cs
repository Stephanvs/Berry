using Berry.Core.Navigation;
using Windows.UI.Xaml.Controls;

namespace Berry.Core.WindowsStore
{
    public class Navigator : INavigator<Frame>
    {
        public void NavigateTo<TViewModel>(Frame navigationContext, TViewModel viewModel)
        {
            navigationContext.Navigate(typeof (TViewModel));
        }
    }
}
