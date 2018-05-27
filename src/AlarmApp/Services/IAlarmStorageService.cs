using System;
using AlarmApp.Models;
using System.Collections.Generic;

namespace AlarmApp.Services
{
	public interface IAlarmStorageService
	{
		List<Alarm> GetAllAlarms();
		bool SaveAlarm(Alarm alarm);
		bool DeleteAlarm(Alarm alarm);
	}
}
