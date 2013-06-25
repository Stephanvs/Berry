using System;
using System.ComponentModel;
using Android.App;
using Berry.Core.Views;
using Berry.Core.ViewModels;

namespace Berry.Core.Android
{
	public abstract class Activity<TViewModel> : Activity, IView<TViewModel>
		where TViewModel : IViewModel, new()
	{
		public Activity ()
		{
			ViewModel = new TViewModel();
		}

		public TViewModel ViewModel { get; set; }
	}
}

