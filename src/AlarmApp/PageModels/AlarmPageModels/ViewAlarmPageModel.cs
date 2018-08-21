using System;
using System.Windows.Input;
using AlarmApp.Models;
using AlarmApp.Services;
using FreshMvvm;

namespace AlarmApp.PageModels
{
	public class ViewAlarmPageModel : AlarmBasePageModel
	{
		public ICommand UpdateAlarmCommand
		{
			get
			{
				return new FreshAwaitCommand((tcs) =>
				{
					UpdateAlarm();
					tcs.SetResult(true);
				});
			}
		}

		public ViewAlarmPageModel(IAlarmStorageService alarmStorage) : base(alarmStorage)
		{
			
		}

		public override void Init(object initData)
		{
			base.Init(initData);
			Alarm = (Alarm)initData;
			AlarmTone = AlarmStorage.GetTone(Alarm.Tone);

			if(AlarmTone == null)
			{
				AlarmTone = AlarmStorage.GetAllTones()[0];
			}
		}

		protected override void ViewIsAppearing(object sender, EventArgs e)
		{
			base.ViewIsAppearing(sender, e);

			//set the properties of this PageModel to the alarm passed through
			var freq = Alarm.GetNumberAndPeriodFromTimeSpan(Alarm.Frequency);
			FrequencyNumber = freq.Key;
			FrequencyPeriod = freq.Value;
			var duration = Alarm.GetNumberAndPeriodFromTimeSpan(Alarm.Duration);
			DurationNumber = duration.Key;
			DurationPeriod= duration.Value;
			IsVibrateOn = Alarm.IsVibrateOn;

			Time = Alarm.Time;
			Days = new DaysOfWeek(Alarm.Days.AllDays);
		}

		/// <summary>
		/// Updates the alarm with the values edited by the user
		/// </summary>
		void UpdateAlarm()
		{
			//need UI feedback
			if (!ValidateFields()) return;

			var frequency = Alarm.GetFrequencyDurationFromNumberAndPeriod(FrequencyNumber, FrequencyPeriod);
			var duration = Alarm.GetFrequencyDurationFromNumberAndPeriod(DurationNumber, DurationPeriod);

			var realm = Realms.Realm.GetInstance();
			realm.Write(() =>
		   {
			   Alarm.Frequency = frequency;
			   Alarm.Time = Time;
			   Alarm.Days = Days;
			   Alarm.Duration = duration;
			   Alarm.Tone = AlarmTone.Id;
		   });
			CoreMethods.PopPageModel(true, false,true);
		}

		protected override bool ValidateFields()
		{
			var s = base.ValidateFields();
			var validation = true;

			if (!DaysOfWeek.GetHasADayBeenSelected(Days))
			{
				HasDayBeenSelected = false;
				validation = false;
			}

			return s & validation;
		}
	}
}
