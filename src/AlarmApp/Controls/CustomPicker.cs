using System;
using Xamarin.Forms;

namespace AlarmApp.Controls
{
	public class CustomPicker : Picker
	{
		public static readonly BindableProperty HintProperty = BindableProperty.Create("Hint", typeof(string), typeof(CustomPicker), null);

		public string Hint 
		{ 
			get { return (string)GetValue(HintProperty); }
			set { SetValue(HintProperty, value); }
		}

		public static readonly BindableProperty FontSizeProperty = BindableProperty.Create("FontSize", typeof(double), typeof(CustomPicker), Entry.FontSizeProperty.DefaultValue);

		public double FontSize
		{
			get { return (double)GetValue(FontSizeProperty); }
			set { SetValue(FontSizeProperty, value); }
		}

	}
}
