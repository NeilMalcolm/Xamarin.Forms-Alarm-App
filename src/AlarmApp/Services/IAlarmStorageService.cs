using System;
using AlarmApp.Models;
using System.Collections.Generic;

namespace AlarmApp.Services
{
	public interface IAlarmStorageService
	{
		List<Alarm> GetAllAlarms();
		List<Alarm> GetTodaysAlarms();

		void AddAlarm(Alarm alarm);
		void UpdateAlarm(Alarm alarm);
		void DeleteAlarm(Alarm alarm);
		bool DoesAlarmExist(Alarm alarm);

		Settings GetSettings();
	}
}
