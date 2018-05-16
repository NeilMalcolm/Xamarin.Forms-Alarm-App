using System;
using System.Threading.Tasks;
using System.Windows.Input;
using AlarmApp.Models;
using AlarmApp.Services;
using FreshMvvm;
using Xamarin.Forms;

namespace AlarmApp.PageModels
{
	public class NewAlarmPageModel : FreshBasePageModel
	{
		IAlarmSetter _alarmSetter = DependencyService.Get<IAlarmSetter>();

		string _name;
		Alarm _alarm = new Alarm();
		TimeSpan _time;

		public string Name
		{
			get { return _name; }
			set { _name = value; RaisePropertyChanged(); }
		}

		public Alarm Alarm
		{
			get { return _alarm; }
			set { _alarm = value; RaisePropertyChanged(); }
		}

		public TimeSpan Time
		{
			get { return _time; }
			set { _time = value; RaisePropertyChanged(); }
		}

		public int FrequencyNumber { get; set; }
		public string FrequencyPeriod { get; set; }

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
			_alarm.Time = DateTime.Now.TimeOfDay;
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

			_alarmSetter.SetAlarm(alarmDateTime);
			var alarm = new Alarm()
			{
				Time = alarmDateTime.TimeOfDay,
				Frequency = (TimeSpan)frequency,
				IsActive = true
				IsActive = true,
				Name = Name
			};

			Defaults.AllAlarms.Add(alarm);

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
