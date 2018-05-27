using System;
using AlarmApp.Models;
using System.Collections.Generic;
using System.IO;
using Xamarin.Forms;
using Realms;
using System.Linq;

namespace AlarmApp.Services
{
	public class AlarmStorageService : IAlarmStorageService
	{
		static Realm _realm = Realm.GetInstance();

		public AlarmStorageService()
		{
			
		}

		/// <summary>
		/// Gets all alarms
		/// </summary>
		/// <returns>All alarms</returns>
		public List<Alarm> GetAllAlarms()
		{
			return _realm.All<Alarm>().ToList();
		}

		/// <summary>
		/// Gets the alarms for today
		/// </summary>
		/// <returns>Today's alarms</returns>
		public List<Alarm> GetTodaysAlarms()
		{
			var all = _realm.All<Alarm>();
			return all.ToList().Where(x => x.OccursToday == true).ToList();
		}

		/// <summary>
		/// Adds the alarm
		/// </summary>
		/// <param name="alarm">Alarm to add</param>
		public void AddAlarm(Alarm alarm)
		{
			_realm.Write(() =>
			{
				_realm.Add<Alarm>(alarm);
			});
		}

		/// <summary>
		/// Updates the alarm
		/// </summary>
		/// <param name="alarm">Alarm to update</param>
		public void UpdateAlarm(Alarm alarm)
		{
			_realm.Write(() =>
			{
				_realm.Add<Alarm>(alarm, true);
			});
		}

		/// <summary>
		/// Deletes the alarm
		/// </summary>
		/// <param name="alarm">Alarm we want to delete</param>
		public void DeleteAlarm(Alarm alarm)
		{
			_realm.Write(() =>
			{
				_realm.Remove(alarm);
			});
		}

		/// <summary>
		/// Checks if the given alarm exists
		/// </summary>
		/// <returns><c>true</c>, if alarm was found, <c>false</c> otherwise</returns>
		/// <param name="alarm">The Alarm we want to know already exists</param>
		public bool DoesAlarmExist(Alarm alarm)
		{
			var containsAlarm = _realm.All<Alarm>().Contains(alarm);
			if (containsAlarm)
				return true;

			return false;
		}
	}
}
