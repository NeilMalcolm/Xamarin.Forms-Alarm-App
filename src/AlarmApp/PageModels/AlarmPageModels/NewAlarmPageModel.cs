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
			if (!ValidateFields()) return;

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

		protected override bool ValidateFields()
		{
			var s = base.ValidateFields();
			var validation = true;

			if (!DaysOfWeek.GetHasADayBeenSelected(Alarm.Days))
			{
				HasDayBeenSelected = false;
				validation = false;
			}

			return s & validation;
		}
	}
}
