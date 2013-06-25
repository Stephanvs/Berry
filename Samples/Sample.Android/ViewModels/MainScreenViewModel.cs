using System;
using Android.App;
using Android.OS;
using Berry.Core;
using Berry.Core.Views;
using Android.Widget;
using Berry.Core.Android;

namespace Sample.Android
{
	[Activity (Label = "Main Screen View Model")]
	public class MainScreenViewModel : Activity<MessageContainingViewModel>
	{
		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);

			// Create your application here
			SetContentView(Resource.Layout.MainScreenView);

			var textView = FindViewById<TextView>(Resource.Id.helloText);

			textView.Text = String.Format (ViewModel.Message, ViewModel.Name);
		}
	}
}