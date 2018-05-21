using System;
using System.Threading.Tasks;
using System.Windows.Input;
using AlarmApp.Models;
using AlarmApp.Services;
using FreshMvvm;
using Xamarin.Forms;

namespace AlarmApp.PageModels
{
	public class NewAlarmPageModel : AlarmBasePageModel
	{
		IAlarmSetter _alarmSetter = DependencyService.Get<IAlarmSetter>();

		public ICommand SaveAlarmCommand
		{
			get {
				return new FreshAwaitCommand((tcs) =>
				{
					SaveAlarm();
					tcs.SetResult(true);
				});
			}
		}

		public NewAlarmPageModel()
		{
			Alarm = new Alarm();
			Alarm.Time = DateTime.Now.TimeOfDay;
		}

		public override void Init(object initData)
		{
			base.Init(initData);
		}

		/// <summary>
		/// Save a new alarm to the list
		/// </summary>
		void SaveAlarm()
		{
			var frequency = GetFrequency();

			//need some UI feedback for user
			if (frequency == null)
				return;
			
			var time = Alarm.Time;
			var now = DateTime.Now;

			var alarmDateTime = new DateTime(now.Year, now.Month, now.Day, time.Hours, time.Minutes, time.Seconds, time.Milliseconds);

			Alarm.Time = alarmDateTime.TimeOfDay;
			Alarm.Frequency = (TimeSpan)frequency;
			Alarm.IsActive = true;

			//Set alarm and add to our list of alarms
			_alarmSetter.SetAlarm(alarmDateTime);
			Defaults.AllAlarms.Add(Alarm);

			//pop the page
			CoreMethods.PopPageModel(false, true);
		}

		TimeSpan? GetFrequency()
		{
			//need some sort of UI feedback for user
			if (FrequencyNumber <= 0 || FrequencyPeriod == null || FrequencyNumber == int.MaxValue)
				return null;

			TimeSpan frequency;

			if (FrequencyPeriod == "Minutes")
				frequency = new TimeSpan(0, FrequencyNumber, 0);

			if (FrequencyPeriod == "Hours")
				frequency = new TimeSpan(FrequencyNumber, 0, 0);

			return frequency;
		}
	}
}
