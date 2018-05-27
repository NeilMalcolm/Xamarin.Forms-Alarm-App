using System;
using System.Collections.Generic;

namespace AlarmApp.Models
{
	public class Alarm
	{
		public string Name { get; set; }
		public TimeSpan Time { get; set; }
		public TimeSpan Frequency { get; set; }

		public string UserFriendlyFrequency
		{
			get
			{
				return GetFrequencyAsReadableString(Frequency);
			}
		}

		public TimeSpan Duration { get; set; }

		public string UserFriendlyDuration
		{
			get
			{
				return GetFrequencyAsReadableString(Duration);
			}
		}

		public TimeSpan EndTime { get { return Time.Add(Duration); } }
		public bool IsActive { get; set; }
		public DaysOfWeek Days { get; set; }
		public bool OccursToday { get { return Days.Equals(DateTime.Now.DayOfWeek); } }

		public Alarm()
		{
			Days = new DaysOfWeek();
		}


		/// <summary>
		/// Gets the frequency as readable string.
		/// </summary>
		/// <returns>The frequency as readable string. Example: TimeSpan of 5 hours, 10 minutes will return a string as such</returns>
		/// <param name="frequency">Frequency.</param>
		string GetFrequencyAsReadableString(TimeSpan frequency)
		{
			var sb = new System.Text.StringBuilder();

			if (frequency.Days > 0)
				sb.Append(frequency.Days > 1 ? frequency.Days + " days" : frequency.Days + " day");

			if (frequency.Hours > 0)
			{
				if (frequency.Days > 0)
					sb.Append(", ");

				sb.Append(frequency.Hours > 1 ? frequency.Hours + " hours" : frequency.Hours + " hour");
			}

			if (frequency.Minutes > 0)
			{
				if(frequency.Days > 0 || frequency.Hours > 0)
					sb.Append(", ");

				sb.Append(frequency.Minutes > 1 ? frequency.Minutes + " minutes" : frequency.Minutes + " minute");
			}

			return sb.ToString();
		}


		/// <summary>
		/// Gets a TimeSpan from the given number and unit of time
		/// </summary>
		/// <returns>The frequency from the given values. Example: 5 and "minutes" will return a TimeSpan of 00:05:00</returns>
		/// <param name="number">Number.</param>
		/// <param name="period">Period.</param>
		public static TimeSpan GetFrequencyDurationFromNumberAndPeriod(int number, string period)
		{
			if(period == "Minutes")
			{
				return new TimeSpan(0, number, 0);
			}

			return new TimeSpan(number, 0, 0);
		}


		/// <summary>
		/// Get the Frequency property of the Alarm as a unit of time and the number 
		/// </summary>
		/// <returns>The number and period from frequency. Example: if the frequency is 00:05:00 then Key = 5, Value = "Minutes"</returns>
		public KeyValuePair<int, string> GetNumberAndPeriodFromTimeSpan(TimeSpan time)
		{
			if(time.Hours > 0)
			{
				return new KeyValuePair<int, string>(time.Hours, "Hours");
			}

			return new KeyValuePair<int, string>(time.Minutes, "Minutes");
		}



		public override string ToString()
		{
			return base.ToString();
			return $"Alarm set for: {Time.ToString("hh/mm/ss")}, occuring every {Frequency.ToString("dd days, hh hours, mm minutes, ss seconds")}";
		}
	}
}
