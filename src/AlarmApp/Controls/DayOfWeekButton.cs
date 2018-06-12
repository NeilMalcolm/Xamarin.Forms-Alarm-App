using System;
using System.ComponentModel;
using Xamarin.Forms;

namespace AlarmApp.Controls
{
	public class DayOfWeekButton : Button
	{
		public static readonly BindableProperty IsSelectedProperty = BindableProperty.Create("IsSelected", typeof(bool), typeof(DayOfWeekButton), false, BindingMode.TwoWay);

		public bool IsSelected 
		{ 
			get
			{
				return (bool)GetValue(IsSelectedProperty);
			}
			set
			{
				SetValue(IsSelectedProperty, value);
			}
		}
	}
}
