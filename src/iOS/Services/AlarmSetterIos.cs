using System;
using AlarmApp.Services;
using AlarmApp.iOS.Services;

[assembly: Xamarin.Forms.Dependency(typeof(AlarmSetterIos))]
namespace AlarmApp.iOS.Services
{
	public class AlarmSetterIos : IAlarmSetter
	{
		public void DeleteAlarm(DateTime time)
		{
			
		}

		public void SetAlarm(DateTime time)
		{
			
		}

		public void SetRepeatingAlarm(DateTime start, DateTime end, TimeSpan interval)
		{
			
		}
	}
}
