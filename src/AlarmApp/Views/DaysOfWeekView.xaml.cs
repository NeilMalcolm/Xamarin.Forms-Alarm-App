using System;
using System.Collections.Generic;
using AlarmApp.Models;
using Xamarin.Forms;

namespace AlarmApp.Views
{
	public partial class DaysOfWeekView : ContentView
	{
		public DaysOfWeekView()
		{
			InitializeComponent();
		}


		protected override void OnBindingContextChanged()
		{
			base.OnBindingContextChanged();

			if (BindingContext == null) return;
			var alarm = ((Alarm)BindingContext);

			var daysOfWeek = alarm.Days;

			System.Diagnostics.Debug.WriteLine("mon: " + daysOfWeek.Monday);
			System.Diagnostics.Debug.WriteLine("beep: " + alarm.Beep);

			MondayLabel.IsVisible = daysOfWeek.Monday;
			TuesdayLabel.IsVisible = daysOfWeek.Tuesday;
			WednesdayLabel.IsVisible = daysOfWeek.Wednesday;
			ThursdayLabel.IsVisible = daysOfWeek.Thursday;
			FridayLabel.IsVisible = daysOfWeek.Friday;
			SaturdayLabel.IsVisible = daysOfWeek.Saturday;
			SundayLabel.IsVisible = daysOfWeek.Sunday;
		}
	}
}
