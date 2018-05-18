using System;
namespace AlarmApp.Models
{
	public class DaysOfWeek
	{
		public bool Monday { get; set; }
		public bool Tuesday { get; set; }
		public bool Wednesday { get; set; }
		public bool Thursday { get; set; }
		public bool Friday { get; set; }
		public bool Saturday { get; set; }
		public bool Sunday { get; set; }

		public bool[] AllDays => new bool[] { Monday, Tuesday, Wednesday, Thursday, Friday, Saturday, Sunday };

		public DaysOfWeek() {}

		public DaysOfWeek(bool[] allDays)
		{
			if (allDays.Length != 7) return;

			Monday = allDays[0];
			Tuesday = allDays[1];
			Wednesday = allDays[2];
			Thursday = allDays[3];
			Friday = allDays[4];
			Saturday = allDays[5];
			Sunday = allDays[6];
		}
	}
}
