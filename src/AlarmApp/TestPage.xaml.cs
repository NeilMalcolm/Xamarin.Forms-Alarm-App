using System;
using System.Collections.Generic;
using AlarmApp.Services;
using Xamarin.Forms;

namespace AlarmApp
{
	public partial class TestPage : ContentPage
	{
		IAlarmSetter _alarmSetter = DependencyService.Get<IAlarmSetter>();
		const int _minsInMilis = 60000;

		DateTime _previouslySet;

		public TestPage()
		{
			InitializeComponent();
		}

		void Handle_DateSelected(object sender, Xamarin.Forms.DateChangedEventArgs e)
		{
			//throw new NotImplementedException();

		}

		//void Handle_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		//{
		//	if(e.PropertyName == "Time")
		//	{
		//		var timePicker = (TimePicker)sender;
		//		var time = timePicker.Time;
		//		var now = DateTime.Now;

		//		var newDateTime = new DateTime(now.Year, now.Month, now.Day, time.Hours, time.Minutes, time.Seconds, time.Milliseconds);
		//		_alarmSetter.SetAlarm(newDateTime);
		//	}
		//}

		void Set_Pressed(object sender, EventArgs e)
		{
			var time = StartTimePicker.Time;
			var now = DateTime.Now;

			var newDateTime = new DateTime(now.Year, now.Month, now.Day, time.Hours, time.Minutes, time.Seconds, time.Milliseconds);
			_previouslySet = newDateTime;
			//_alarmSetter.SetAlarm(newDateTime);

			//if(string.IsNullOrWhiteSpace(FrequencyEntry.Text))
			//{
			//	DisplayAlert("Oi", "Put some frequency in, ya dafty", "aiight");
			//	return;
			//}

			//var startTime = StartTimePicker.Time;
			//var endTime = EndTimePicker.Time;


			//if (startTime.CompareTo(endTime) >= 0)
			//{
			//	DisplayAlert("Oi", "The start time needs to be before the end time ya wank", "aiight");
			//	return;
			//}

			//int frequency = -1;
			//int.TryParse(FrequencyEntry.Text, out frequency);

			//if(frequency <= 0)
			//{
			//	DisplayAlert("Oi", "Choose a frequency boyo", "aiight");
			//	return;
			//}

			//var timeDifferenceInMilis = endTime.TotalMilliseconds - startTime.TotalMilliseconds;
			//double frequencyInMilis = _minsInMilis * frequency;


		}

		void Delete_Pressed(object sender, EventArgs e)
		{
			//_alarmSetter.DeleteAlarm(_previouslySet);
		}
	}
}
