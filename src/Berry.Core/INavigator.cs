using System;

namespace Berry.Core
{
    public interface INavigator<in TNavigationContext>
    {
        void NavigateTo<TViewModel>(TNavigationContext navigationContext, TViewModel viewModel);
    }
}
