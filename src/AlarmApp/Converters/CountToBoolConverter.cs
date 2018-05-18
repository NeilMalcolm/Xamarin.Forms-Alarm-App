using System;
using System.Globalization;
using Xamarin.Forms;

namespace AlarmApp.Converters
{
	public class CountToBoolConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (!(value is int)) return false;

			var count = (int)value;
			return count > 0;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
