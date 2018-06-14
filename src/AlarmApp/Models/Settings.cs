using System;
using System.Globalization;
using Realms;
using PropertyChanged;

namespace AlarmApp.Models
{
	[AddINotifyPropertyChangedInterface]
	public class Settings : RealmObject
	{
		public int FormatInt { get; private set; }

		[Ignored]
		public ClockFormat Format 
		{ 
			get { return (ClockFormat)FormatInt; }
			set { FormatInt = (int)value; }
		}

		[Ignored]
		public string TimeFormat => GetTimeFormatAsString();
		public string AlarmTone { get; set; } = "Buzz";
		public bool IsVibrateOn { get; set; } = true;

		/// <summary>
		/// Gets the Format in string format
		/// </summary>
		/// <returns>The time format as string</returns>
		string GetTimeFormatAsString()
		{
			var stringBuilder = new System.Text.StringBuilder();
			if(Format == ClockFormat.Hour24)
			{
				stringBuilder.Append("24 Hour - ");
				stringBuilder.Append(DateTime.Now.TimeOfDay.ToString(@"hh\:mm"));
			}
			else {
				stringBuilder.Append("12 Hour - ");
				stringBuilder.Append(DateTime.Now.ToString("h:mm tt"));
			}

			return stringBuilder.ToString();
		}

		/// <summary>
		/// Switches the format between 24 Hour and 12 Hour
		/// </summary>
		public void SwitchFormat()
		{
			if (Format == ClockFormat.Hour12)
				Format = ClockFormat.Hour24;
			else
				Format = ClockFormat.Hour12;
		}
	}

	public enum ClockFormat 
	{
		Hour12 = 0, 
		Hour24 = 1
	}
}