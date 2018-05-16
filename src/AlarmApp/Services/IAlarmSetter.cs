using System;

namespace AlarmApp.Services
{
	public interface IAlarmSetter
	{
		void SetAlarm(DateTime time);

		void SetRepeatingAlarm(DateTime start, DateTime end, TimeSpan interval);

		void DeleteAlarm(DateTime time);
	}
}
