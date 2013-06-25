using System;
using Berry.Core.ViewModels;

namespace Sample.Android
{
	public class MessageContainingViewModel : ViewModelBase
	{
		public string Message = "Hello {0}";
		public string Name = "Berry";
	}
}

