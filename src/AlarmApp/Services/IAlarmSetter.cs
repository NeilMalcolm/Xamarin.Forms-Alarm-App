using System;
using AlarmApp.Models;

namespace AlarmApp.Services
{
	public interface IAlarmSetter
	{
		void SetAlarm(Alarm alarm);

		void SetRepeatingAlarm(Alarm alarm);

		void DeleteAlarm(Alarm alarm);
	}
}
