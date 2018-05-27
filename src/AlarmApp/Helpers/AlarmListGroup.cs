using System;
using System.Collections.Generic;
using AlarmApp.Models;

namespace AlarmApp.Helpers
{
	public class AlarmListGroup : List<Alarm>
	{
		public string Title { get; set; }
		public string ShortName { get; set; }

		public AlarmListGroup(string title, string shortName)
		{
			Title = title;
			ShortName = shortName;
		}

		public AlarmListGroup(List<Alarm> alarms)
		{
			this.AddRange(alarms);
		}
	}
}
