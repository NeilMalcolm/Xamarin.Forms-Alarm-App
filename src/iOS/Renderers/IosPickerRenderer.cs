using System;
using AlarmApp.iOS.Renderers;
using AlarmApp.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CustomPicker), typeof(IosPickerRenderer))]
namespace AlarmApp.iOS.Renderers
{
	public class IosPickerRenderer : PickerRenderer
	{
		CustomPicker _customPicker;

		protected override void OnElementChanged(ElementChangedEventArgs<Picker> e)
		{
			base.OnElementChanged(e);

			if (Control != null)
			{
				Control.BackgroundColor = UIKit.UIColor.Clear;
			}
			if(Element != null)
			{
				_customPicker = (CustomPicker)Element;
				_customPicker.IsValidChanged += OnIsValidChanged;
			}
		}

		void OnIsValidChanged(object sender, EventArgs e)
		{
			if ((bool)_customPicker.IsValid)
				Control.BackgroundColor = UIKit.UIColor.Clear;
			else
				Control.BackgroundColor = UIKit.UIColor.Red;
		}

	}
}
