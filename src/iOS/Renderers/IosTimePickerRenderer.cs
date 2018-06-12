using System;
using AlarmApp.iOS.Renderers;
using AlarmApp.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(TimePicker), typeof(IosTimePickerRenderer))]
namespace AlarmApp.iOS.Renderers
{
	public class IosTimePickerRenderer : TimePickerRenderer
	{
		protected override void OnElementChanged(ElementChangedEventArgs<TimePicker> e)
		{
			base.OnElementChanged(e);

			if (Control != null)
			{
				Control.BackgroundColor = UIKit.UIColor.Clear;
				Control.Text = Element.Time.ToString(@"hh\:mm");
			}
		}
	}
}
