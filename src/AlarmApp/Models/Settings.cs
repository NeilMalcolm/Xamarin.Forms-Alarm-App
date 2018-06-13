using System;
using Realms;

namespace AlarmApp.Models
{
	public class Settings : RealmObject
	{
		public int FormatInt { get; set; } = (int)ClockFormat.Hour24;
		public ClockFormat Format 
		{ 
			get { return (ClockFormat)FormatInt; }
		}

		public string AlarmTone { get; set; } = "Buzz";
		public bool IsVibrateOn { get; set; } = true;
	}

	public enum ClockFormat 
	{
		Hour12 = 0, 
		Hour24 = 1
	}
}