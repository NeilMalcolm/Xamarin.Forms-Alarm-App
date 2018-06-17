using System;
using AlarmApp.Services;
using AlarmApp.iOS.Services;
using AlarmApp.Models;
using System.Collections.Generic;

[assembly: Xamarin.Forms.Dependency(typeof(AlarmSetterIos))]
namespace AlarmApp.iOS.Services
{
	public class AlarmSetterIos : IAlarmSetter
	{
		public void DeleteAlarm(Alarm alarm)
		{
			
		}

		public void SetAlarm(Alarm alarm)
		{
			
		}

		public void SetRepeatingAlarm(Alarm alarm)
		{
			
		}

		public void DeleteAllAlarms(List<Alarm> alarms)
		{
			
		}

	}
}
