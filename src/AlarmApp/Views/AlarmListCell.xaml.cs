using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using AlarmApp.Models;
using Xamarin.Forms;

namespace AlarmApp.Views
{
	public partial class AlarmListCell : ViewCell
	{
		Alarm _alarm;
		public AlarmListCell()
		{
			InitializeComponent();
			App.Current.Resources["NameHeading"] = App.Current.Resources["AlarmNameHeading"];
		}

		protected override void OnBindingContextChanged()
		{
			base.OnBindingContextChanged();

			if (BindingContext == null) return;
			_alarm = (Alarm)BindingContext;

			if (string.IsNullOrWhiteSpace(_alarm.Name))
				NameLabel.IsVisible = false;
			else
			{
				NameLabel.Text = _alarm.Name;
				NameLabel.IsVisible = true;
			}
			
			StartSpan.Text = _alarm.Time.ToString(@"hh\:mm");
			EndSpan.Text = _alarm.EndTime.ToString(@"hh\:mm");
			var freq = _alarm.UserFriendlyFrequency;
			FrequencyLabel.Text = string.IsNullOrWhiteSpace(freq) ? null : $"Every {freq}";
			SetDynamicResources(_alarm.IsActive);
			//IsActiveSwitch.IsToggled = _alarm.IsActive;
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();
			ActiveSwitch.Toggled += ActiveSwitch_Toggled;
		}

		protected override void OnDisappearing()
		{
			ActiveSwitch.Toggled -= ActiveSwitch_Toggled;
			base.OnDisappearing();
		}

		void ActiveSwitch_Toggled(object sender, ToggledEventArgs e)
		{
			//switch resource
			DaysOfWeekView.IsEnabled = e.Value;
			SetDynamicResources(e.Value);
		}

		void SetDynamicResources(bool state)
		{
			if (state)
			{
				//if is active
				NameLabel.Style = (Style)App.Current.Resources["AlarmNameHeading"];
				TimeLabel.Style = (Style)App.Current.Resources["AlarmTimeHeading"];
				FrequencyLabel.Style = (Style)App.Current.Resources["AlarmExtrasHeading"];
				OnLabel.Style = (Style)App.Current.Resources["AlarmExtrasHeading"];
			}
			else
			{
				//if is not active
				NameLabel.Style = (Style)App.Current.Resources["AlarmNameDisabledHeading"];
				TimeLabel.Style = (Style)App.Current.Resources["AlarmTimeDisabledHeading"];
				FrequencyLabel.Style = (Style)App.Current.Resources["AlarmExtrasDisabledHeading"];
				OnLabel.Style = (Style)App.Current.Resources["AlarmExtrasDisabledHeading"];
			}
		}
	}
}
