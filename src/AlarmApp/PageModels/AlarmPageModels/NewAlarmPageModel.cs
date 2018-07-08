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

		public NewAlarmPageModel(IAlarmStorageService alarmStorage) : base(alarmStorage)
		{
			Alarm = new Alarm();
			AlarmTone = alarmStorage.GetTone(Alarm.Tone);
			Alarm.Time = DateTime.Now.TimeOfDay;
		}

		/// <summary>
		/// Save a new alarm to the list
		/// </summary>
		void SaveAlarm()
		{
			if (!ValidateFields()) return;

			var frequency = GetDurationOrFrequency(FrequencyNumber, FrequencyPeriod);
			var duration = GetDurationOrFrequency(DurationNumber, DurationPeriod);
			
			var time = Alarm.Time;
			var now = DateTime.Now;

			var alarmDateTime = new DateTime(now.Year, now.Month, now.Day, time.Hours, time.Minutes, time.Seconds, time.Milliseconds);

			Alarm.IsActive = true;
			Alarm.Frequency = (TimeSpan)frequency;
			Alarm.Duration = (TimeSpan)duration;

			//Set alarm and add to our list of alarms
			_alarmSetter.SetAlarm(Alarm);

			var realm = Realms.Realm.GetInstance();

			using (var transaction = realm.BeginWrite())
			{
				realm.Add(Alarm, true);
				Alarm.Tone = AlarmTone.Id;

				transaction.Commit();
			}

			//pop the page
			CoreMethods.PopPageModel(true, false, true);
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

		/// <summary>
		/// Get the duration or frequency as a TimeSpan object, number represents either the hour or minute
		/// value, depending on the period value. i.e. if period is Minutes and
		/// number is 5, we get a nullable TimeSpan of 0, 0, 5, 0 (dd, hh, mm, ss)
		/// </summary>
		/// <returns>The frequency as a TimeSpan object, null if either are not set</returns>
		protected TimeSpan? GetDurationOrFrequency(int number, string period)
		{
			//need some sort of UI feedback for user
			if (number <= 0 || period == null || number == int.MaxValue)
				return null;

			TimeSpan time;

			if (period == "Minutes")
				time = new TimeSpan(0, number, 0);

			if (period == "Hours")
				time = new TimeSpan(number, 0, 0);

			return time;
		}
	}
}
