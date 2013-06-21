using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.Content;
using Android.App;

namespace Berry.Core.Android.Navigation
{
    public class Navigator : INavigator<Activity>
    {
		public void NavigateTo<TViewModel>(Activity navigationContext, TViewModel viewModel)
		{
			Intent i = new Intent(navigationContext, viewModel.GetType());
			navigationContext.StartActivity(i);
		}
    }
}
