using System.ComponentModel;
using AlarmApp.Controls;
using AlarmApp.Droid.Renderers;
using Android.Content;
using Android.Runtime;
using Android.Support.V4.Content.Res;
using Android.Views;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using AGraphics = Android.Graphics;

[assembly: ExportRenderer(typeof(CustomPicker), typeof(AndroidPickerRenderer))]
namespace AlarmApp.Droid.Renderers
{
	public class AndroidPickerRenderer : Xamarin.Forms.Platform.Android.AppCompat.PickerRenderer
	{
		AGraphics.Color _backgroundColor = new AGraphics.Color(255, 255, 255, 25);
		AGraphics.Color _invalidBackgroundColor = new AGraphics.Color(255, 0, 0, 25);
		CustomPicker _customPicker;

		public AndroidPickerRenderer(Context context) : base(context)
		{

		}

		protected override void OnElementChanged(ElementChangedEventArgs<Picker> e)
		{
			base.OnElementChanged(e);

			if (Control == null) return;

			_customPicker = (CustomPicker)Element;
			if(_customPicker.Hint != null)
				Control.Hint = _customPicker.Hint;

			_customPicker.IsValidChanged += OnIsValidChanged;
			Control.TextSize = (float) _customPicker.FontSize;

			Control.SetPaddingRelative(5, 5, 5, 5);
			Control.SetHintTextColor(Color.FromHex("#565C78").ToAndroid());
			Control.Background = ResourcesCompat.GetDrawable(Resources, Resource.Drawable.control_selector, null);
		}

		protected override void Dispose(bool disposing)
		{
			_customPicker.IsValidChanged -= OnIsValidChanged;
			base.Dispose(disposing);
		}

		void OnIsValidChanged(object sender, System.EventArgs e)
		{
			if(!(bool)_customPicker.IsValid)
			{
				Control.Background = ResourcesCompat.GetDrawable(Resources, Resource.Drawable.control_background_invalid, null);
			}
			else
			{
				Control.Background = ResourcesCompat.GetDrawable(Resources, Resource.Drawable.control_selector, null);
			}
		}

		protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			base.OnElementPropertyChanged(sender, e);

			//if (e.PropertyName == Picker.SelectedIndexProperty.PropertyName)
			//{
			//	if(_customPicker.SelectedIndex >= 0)
			//	{
			//		OnIsValidChanged = true;
			//	}
			//}

			if(e.PropertyName == Picker.IsFocusedProperty.PropertyName)
			{
				if(!_customPicker.IsFocused)
				{
					if(_customPicker.SelectedIndex >= 0)
					{
						_customPicker.IsValid = true;
					}
					else
					{
						_customPicker.IsValid = false;	
					}
				}
			}
		}
	}
}
