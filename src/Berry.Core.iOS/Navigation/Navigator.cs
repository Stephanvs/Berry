using System;
using MonoTouch.UIKit;

namespace Berry.Core.iOS
{
	public class Navigator : INavigator<UIViewController>
	{
		void NavigateTo<TViewModel>(UIViewController navigationContext, TViewModel viewModel)
		{
			var viewController = viewModel as UIViewController;

			navigationContext.NavigationController.PushViewController(viewController, animated: false);
		}
	}
}

