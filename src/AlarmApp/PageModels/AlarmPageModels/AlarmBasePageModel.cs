using System;
using System.Windows.Input;
using AlarmApp.Models;
using FreshMvvm;

namespace AlarmApp.PageModels
{
	public class AlarmBasePageModel : FreshBasePageModel
	{
		string _name;
		Alarm _alarm;
		TimeSpan _time;
		int _frequencyNumber;
		string _frequencyPeriod;
		int _durationNumber;
		string _durationPeriod;
		DaysOfWeek _days;

		//True by default, so we can be notified if false
		bool _hasDayBeenSelected = true;
		bool _isRepetitionsSet = true;
		bool _isIntervalSset = true;
		bool _isTotalDurationSet = true;

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

		// repeat every

		public int FrequencyNumber
		{
			get { return _frequencyNumber; }
			set { _frequencyNumber = value; RaisePropertyChanged(); }
		}

		public string FrequencyPeriod
		{
			get { return _frequencyPeriod; }
			set { _frequencyPeriod = value; RaisePropertyChanged(); }
		}

		// for

		public int DurationNumber
		{
		    get{ return _durationNumber;  }
			set{ _durationNumber = value; RaisePropertyChanged(); }
		}

		public string DurationPeriod
		{
			get{ return _durationPeriod;  }
			set{ _durationPeriod = value; RaisePropertyChanged(); }
		}

		public DaysOfWeek Days
		{
			get { return _days; }
			set { _days = value; RaisePropertyChanged(); }
		}

		public bool HasDayBeenSelected
		{
			get { return _hasDayBeenSelected; }
			set { _hasDayBeenSelected = value; RaisePropertyChanged(); }
		}

		public ICommand DayPressedCommand
		{
			get
			{
				return new Xamarin.Forms.Command((param) =>
				{
					var isSelected = (bool)param;
					if (HasDayBeenSelected == false)
					{
						if (!isSelected)
						{
							HasDayBeenSelected = true;
							return;
						}

						if (DaysOfWeek.GetHasADayBeenSelected(Days))
						{
							HasDayBeenSelected = true;
						}
					}
				});
			}
		}

		/// <summary>
		/// Validates the fields to see if the data is valid
		/// </summary>
		/// <returns><c>true</c>true, if fields have been validated, <c>false</c> otherwise.</returns>
		protected bool ValidateFields()
		{
			bool validation = true;
		
			if(FrequencyNumber == 0)
			{
				//Set which one to false
				validation = false;
			}

			if(string.IsNullOrWhiteSpace(FrequencyPeriod))
			{
				validation = false;
			}

			if (DurationNumber == 0)
			{
				//Set which one to false
				validation = false;
			}

			if (string.IsNullOrWhiteSpace(DurationPeriod))
			{
				validation = false;
			}

			if (!DaysOfWeek.GetHasADayBeenSelected(Days))
			{
				HasDayBeenSelected = false;
				validation = false;
			}

			return validation;
		}
	}
}
