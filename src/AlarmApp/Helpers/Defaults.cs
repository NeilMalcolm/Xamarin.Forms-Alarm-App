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
		////Frequency of once per day
		//public readonly static TimeSpan DEFAULT_FrequencyOffset = new DateTimeOffset(DateTime.Now, new TimeSpan(1, 0, 0, 0));
		//public readonly static TimeSpan TEST_FrequencyOffset = new DateTimeOffset(DateTime.Now, new TimeSpan(0, 0, 0, 0));
		//public readonly static TimeSpan ONE_HOUR = new TimeSpan(0, 1, 0, 0);
		//public readonly static TimeSpan THREE_HOURS = new TimeSpan(0, 3, 0, 0);
		//public readonly static TimeSpan FIVE_HOURS = new TimeSpan(0, 5, 0, 0);

		//public static ObservableCollection<Alarm> AllAlarms = new ObservableCollection<Alarm>()
		//{
		//	new Alarm
		//	{
		//		Name = "Name of alarm",
		//		TimeOffset = new DateTimeOffset(DateTime.Now, default(DateTime).TimeOfDay.Add(ONE_HOUR)),
  //              FrequencyOffset = new DateTimeOffset(DateTime.Now, new TimeSpan(0, 5, 0)),
		//		IsActive = false,
		//		DurationOffset = new DateTimeOffset(DateTime.Now, new TimeSpan(1, 0, 0)),
		//		Days = new DaysOfWeek(new bool[]{true, true, true, false, false, false, true})
		//	}, 
		//	new Alarm
		//	{
		//		Name = "Second Alarm",
		//		TimeOffset = new DateTimeOffset(DateTime.Now, default(DateTime).TimeOfDay.Add(FIVE_HOURS)),
		//		FrequencyOffset = new DateTimeOffset(DateTime.Now, new TimeSpan(0, 10, 0)),
		//		IsActive = true,
  //              DurationOffset = new DateTimeOffset(DateTime.Now, new TimeSpan(5, 0, 0)),
		//		Days = new DaysOfWeek(new bool[]{true, true, true, false, false, false, false})
		//	}, 
		//	new Alarm
		//	{
		//		Name = "Third alarm",
		//		TimeOffset = new DateTimeOffset(DateTime.Now, default(DateTime).TimeOfDay.Add(THREE_HOURS)),
		//		FrequencyOffset = new DateTimeOffset(DateTime.Now, new TimeSpan(0, 5, 0)),
		//		IsActive = false,
		//		DurationOffset = new DateTimeOffset(DateTime.Now, new TimeSpan(0, 30, 0)),
		//		Days = new DaysOfWeek(new bool[]{true, true, true, false, false, false, true})
		//	}, 
		//	new Alarm
		//	{
		//		Name = "Name of alarm",
		//		TimeOffset = new DateTimeOffset(DateTime.Now, default(DateTime).TimeOfDay.Add(ONE_HOUR)),
		//		FrequencyOffset = new DateTimeOffset(DateTime.Now, new TimeSpan(0, 5, 0)),
		//		IsActive = false,
		//		DurationOffset = new DateTimeOffset(DateTime.Now, new TimeSpan(1, 0, 0)),
		//		Days = new DaysOfWeek(new bool[]{true, true, true, false, false, false, false})
		//	}, 
		//	new Alarm
		//	{
		//		Name = "Name of alarm",
		//		TimeOffset = new DateTimeOffset(DateTime.Now, default(DateTime).TimeOfDay.Add(ONE_HOUR)),
		//		FrequencyOffset = new DateTimeOffset(DateTime.Now, new TimeSpan(0, 5, 0)),
		//		IsActive = true,
		//		DurationOffset = new DateTimeOffset(DateTime.Now, new TimeSpan(1, 0, 0)),
		//		Days = new DaysOfWeek(new bool[]{true, true, true, false, false, false, false})
		//	}, 
		//};
	}

	public enum AlarmListType
	{
		All, Today
	}
}
