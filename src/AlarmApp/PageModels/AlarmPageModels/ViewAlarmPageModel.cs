using System;
using System.Windows.Input;
using AlarmApp.Models;
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

		public override void Init(object initData)
		{
			base.Init(initData);
			Alarm = (Alarm)initData;
		}


		protected override void ViewIsAppearing(object sender, EventArgs e)
		{
			base.ViewIsAppearing(sender, e);

			//set the properties of this PageModel to the alarm passed through
			var freq = Alarm.GetNumberAndPeriodFromFrequency();
			FrequencyNumber = freq.Key;
			FrequencyPeriod = freq.Value;

			Time = Alarm.Time;
			Days = new DaysOfWeek(Alarm.Days.AllDays);
		}

		protected override void ViewIsDisappearing(object sender, EventArgs e)
		{
			base.ViewIsDisappearing(sender, e);

			System.Diagnostics.Debug.WriteLine("ALARM.DAYS");
			foreach(bool b in Alarm.Days.AllDays)
			{
				System.Diagnostics.Debug.WriteLine(b.ToString());
			}
			System.Diagnostics.Debug.WriteLine("DAYS");
			foreach (bool b in Days.AllDays)
			{
				System.Diagnostics.Debug.WriteLine(b.ToString());
			}
		}

		/// <summary>
		/// Updates the alarm with the values edited by the user
		/// </summary>
		void UpdateAlarm()
		{
			//need UI feedback
			if (!ValidateFields()) return;

			var frequencyTimeSpan = Alarm.GetFrequencyFromNumberAndPeriod(FrequencyNumber, FrequencyPeriod);

			Alarm.Frequency = frequencyTimeSpan;
			Alarm.Time = Time;
			Alarm.Days = Days;

			CoreMethods.PopPageModel(false, true);
		}

	}
}
