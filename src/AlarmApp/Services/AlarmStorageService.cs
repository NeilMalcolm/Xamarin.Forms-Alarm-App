using System;
using AlarmApp.Models;
using System.Collections.Generic;
using SQLite;
using System.IO;
using Xamarin.Forms;

namespace AlarmApp.Services
{
	public class AlarmStorageService : IAlarmStorageService
	{
		const string _filePath = "Alarms.db";
		readonly IDeviceStorageService _storageService = DependencyService.Get<IDeviceStorageService>();
		string _databasePath;

		public AlarmStorageService()
		{
			_databasePath = _storageService.GetFilePath(_filePath);
		}

		public bool DeleteAlarm(Alarm alarm)
		{
			
		}

		public List<Alarm> GetAllAlarms()
		{
			
		}

		public bool SaveAlarm(Alarm alarm)
		{
			
		}
	}
}
