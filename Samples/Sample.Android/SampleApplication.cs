using System;
using Berry.Core;
using System.Threading.Tasks;
using Berry.Core.Android.Navigation;

namespace Sample.Android
{
	public class SampleApplication : IApplication<Navigator>
	{
		private readonly TaskScheduler _scheduler;

		public SampleApplication(Navigator navigator, TaskScheduler scheduler) 
		{
			_scheduler = scheduler;
			CurrentNavigationContext = navigator;
		}

		#region IApplication implementation

		public Navigator CurrentNavigationContext { get; private set; }

		#endregion
	}
}