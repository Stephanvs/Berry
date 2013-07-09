using System;
using System.ComponentModel;

namespace Berry.Core.ViewModels
{
	public abstract class ViewModelBase : IViewModel
	{
		public event PropertyChangedEventHandler PropertyChanged;
	}
}