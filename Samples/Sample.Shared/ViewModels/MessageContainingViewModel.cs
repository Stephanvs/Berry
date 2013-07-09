using System;
using Berry.Core.ViewModels;

namespace Sample.Shared.ViewModels
{
	public class MessageContainingViewModel : ViewModelBase
	{
		public string Message = "Hello {0}";
		public string Name = "Berry";
	}
}

