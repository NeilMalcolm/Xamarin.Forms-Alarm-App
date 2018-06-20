using System;
using AlarmApp.Models;
using System.Collections.Generic;
using Realms;
using System.Threading.Tasks;

namespace AlarmApp.Services
{
	public interface IAlarmStorageService
	{
		Realm Realm { get; }

		List<Alarm> GetAllAlarms();
		List<Alarm> GetTodaysAlarms();

		void AddAlarm(Alarm alarm);
		void UpdateAlarm(Alarm alarm);
		void DeleteAlarm(Alarm alarm);
		bool DoesAlarmExist(Alarm alarm);
		void DeleteAllAlarms();

		Settings GetSettings();
		Task<Settings> GetSettingsAsync();
	}
}
