using System;
using System.Collections.ObjectModel;
using AlarmApp.Models;

namespace AlarmApp
{
	/// <summary>
	/// Default values to be used by the app for various reasons
	/// </summary>
	public static class Defaults
	{
		//Frequency of once per day
		public readonly static TimeSpan DEFAULT_FREQUENCY = new TimeSpan(1, 0, 0, 0);
		public readonly static TimeSpan TEST_FREQUENCY = new TimeSpan(0, 0, 0, 0);
		public readonly static TimeSpan ONE_HOUR = new TimeSpan(0, 1, 0, 0);
		public readonly static TimeSpan THREE_HOURS = new TimeSpan(0, 3, 0, 0);
		public readonly static TimeSpan FIVE_HOURS = new TimeSpan(0, 5, 0, 0);

		public static ObservableCollection<Alarm> TodaysAlarms = new ObservableCollection<Alarm>()
		{
			new Alarm
			{
				Name = "Name of alarm",
				Time = DateTime.Now.TimeOfDay.Add(ONE_HOUR),
				Frequency = new TimeSpan(0, 5, 0),
				IsActive = true,
				Days = new DaysOfWeek(new bool[]{true, true, true, false, false, false, false})
			}
		};

		public static ObservableCollection<Alarm> AllAlarms = new ObservableCollection<Alarm>();
	}

	public enum AlarmListType
	{
		All, Today
	}
}
