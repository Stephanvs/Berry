using System;
using Android.App;
using Android.Content;
using Berry.Core.Navigation;

namespace Berry.Core.Android.Navigation
{
	public class Navigator : INavigator<Activity>
	{
		public void NavigateTo<TViewModel>(Activity navigationContext, TViewModel viewModel)
		{
			Intent intentToNavigateToActivity = new Intent(navigationContext, viewModel.GetType());
			navigationContext.StartActivity(intentToNavigateToActivity);
		}
	}
}