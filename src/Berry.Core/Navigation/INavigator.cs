namespace Berry.Core.Navigation
{
    public interface INavigator<in TNavigationContext>
    {
		void NavigateTo<TViewModel>(TNavigationContext navigationContext, TViewModel viewModel);
    }
}
