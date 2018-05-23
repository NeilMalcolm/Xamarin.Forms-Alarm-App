using System;
using System.Collections.Generic;
using AlarmApp.Models;
using Xamarin.Forms;

namespace AlarmApp.Views
{
	public partial class AlarmListCell : ViewCell
	{
		public AlarmListCell()
		{
			InitializeComponent();
		}

		protected override void OnBindingContextChanged()
		{
			base.OnBindingContextChanged();

			if (BindingContext == null) return;
			var alarm = (Alarm)BindingContext;

			NameLabel.Text = alarm.Name;
			StartSpan.Text = alarm.Time.ToString(@"hh\:mm");
			EndSpan.Text = alarm.EndTime.ToString(@"hh\:mm");
			var freq = alarm.UserFriendlyFrequency;
			FrequencyLabel.Text = string.IsNullOrWhiteSpace(freq) ? null : $"Every {freq}";
		}
	}
}
