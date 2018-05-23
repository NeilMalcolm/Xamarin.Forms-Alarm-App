using System;
using Xamarin.Forms;
namespace AlarmApp.Controls
{
	public class CustomEntry : Entry
	{
		public static readonly BindableProperty IsValidProperty = BindableProperty.Create("IsValid", typeof(bool?), typeof(CustomEntry), null, propertyChanged: OnIsValidChanged);

		public bool? IsValid
		{
			get { return (bool?)GetValue(IsValidProperty); }
			set { SetValue(IsValidProperty, value); }
		}

		public event EventHandler IsValidChanged;

		static void OnIsValidChanged(BindableObject bindable, object oldValue, object newValue)
		{
			// Property changed implementation goes here
			var entry = (CustomEntry)bindable;
			entry.IsValidChanged?.Invoke(entry, null);
		}
	}
}
