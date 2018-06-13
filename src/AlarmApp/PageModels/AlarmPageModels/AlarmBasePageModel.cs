using System;
using System.Windows.Input;
using AlarmApp.Models;
using AlarmApp.Services;
using FreshMvvm;
using PropertyChanged;

namespace AlarmApp.PageModels
{
	[AddINotifyPropertyChangedInterface]
	public class AlarmBasePageModel : FreshBasePageModel
	{
		protected IAlarmStorageService _alarmStorage;

		public string Name { get; set; }
		public Alarm Alarm { get; set; }
		public TimeSpan Time { get; set; }
		public int FrequencyNumber { get; set; }
		public string FrequencyPeriod { get; set; }
		public int DurationNumber { get; set; }
		public string DurationPeriod { get; set; }
		public DaysOfWeek Days { get; set; }

		// Validation
		public bool HasDayBeenSelected { get; set; } = true;
		public bool IsFrequencyNumberValid { get; set; } = true;
		public bool IsFrequencyPeriodValid { get; set; } = true;
		public bool IsDurationNumberValid { get; set; } = true;
		public bool IsDurationPeriodValid { get; set; } = true;


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
		protected virtual bool ValidateFields()
		{
			bool validation = true;
		
			if(FrequencyNumber <= 0)
			{
				//Set which one to false
				IsFrequencyNumberValid = false;
				validation = false;
			}
			else
			{
				IsFrequencyNumberValid = true;
			}

			if(string.IsNullOrWhiteSpace(FrequencyPeriod))
			{
				IsFrequencyPeriodValid = false;
				validation = false;
			}
			else
			{
				IsFrequencyPeriodValid = true;
			}

			if (DurationNumber <= 0)
			{
				//Set which one to false
				IsDurationNumberValid = false;
				validation = false;
			}
			else
			{
				IsDurationNumberValid = true;
			}

			if (string.IsNullOrWhiteSpace(DurationPeriod))
			{
				IsDurationPeriodValid = false;
				validation = false;
			}
			else
			{
				IsDurationPeriodValid = true;
			}

			return validation;
		}

		public AlarmBasePageModel()
		{
			
		}

		public AlarmBasePageModel(IAlarmStorageService alarmStorage)
		{
			_alarmStorage = alarmStorage;
		}
	}
}
