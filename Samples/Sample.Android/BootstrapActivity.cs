using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Threading.Tasks;
using Berry.Core.Android.Navigation;

namespace Sample.Android
{
	[Activity (Label = "Sample.Android", MainLauncher = true)]
	public class BootstrapActivity : Activity
	{
		protected SampleApplication App { get; set; }

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			var taskScheduler = TaskScheduler.FromCurrentSynchronizationContext();
			App = new SampleApplication (new Navigator(), taskScheduler);

			App.CurrentNavigationContext.NavigateTo(this, new MainScreenViewModel());
		}
	}
}