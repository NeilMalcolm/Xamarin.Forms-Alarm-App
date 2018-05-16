using System;
using System.Windows.Input;
using AlarmApp.Models;
using FreshMvvm;

namespace AlarmApp.PageModels
{
	public class ViewAlarmPageModel : FreshBasePageModel
	{
		string _name;
		Alarm _alarm;
		TimeSpan _time;
		int _frequencyNumber;
		string _frequencyPeriod;
		DaysOfWeek _days;

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
			get
			{
				return _time;
			}
			set
			{
				_time = value;
				RaisePropertyChanged();
			}
		}

		public int FrequencyNumber
		{
			get { return _frequencyNumber; }
			set
			{
				_frequencyNumber = value;
				RaisePropertyChanged();
			}
		}


		public string FrequencyPeriod
		{
			get { return _frequencyPeriod; }
			set
			{
				_frequencyPeriod = value;
				RaisePropertyChanged();
			}
		}

		public DaysOfWeek Days
		{
			get { return _days; }
			set { _days = value; RaisePropertyChanged(); }
		}

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
			Days = Alarm.Days;
		}


		/// <summary>
		/// Updates the alarm with the values edited by the user
		/// </summary>
		void UpdateAlarm()
		{
			var frequencyTimeSpan = Alarm.GetFrequencyFromNumberAndPeriod(FrequencyNumber, FrequencyPeriod);

			Alarm.Frequency = frequencyTimeSpan;
			Alarm.Time = Time;
			Alarm.Days = Days;

			CoreMethods.PopPageModel(false, true);
		}
	}
}
