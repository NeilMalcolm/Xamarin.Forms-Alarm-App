using System;
using AlarmApp.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using AlarmApp.iOS.Renderers;
using System.ComponentModel;

[assembly: ExportRenderer(typeof(DayOfWeekButton), typeof(IosDayOfTheWeekButtonRenderer))]
namespace AlarmApp.iOS.Renderers
{
	public class IosDayOfTheWeekButtonRenderer : ButtonRenderer
	{
		DayOfWeekButton _dowButton;
		protected override void OnElementChanged(ElementChangedEventArgs<Button> e)
		{
			base.OnElementChanged(e);

			if(Element != null)
			{
				_dowButton = (DayOfWeekButton)Element;
				_dowButton.Clicked += Button_Clicked;
			}
		}

		protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			base.OnElementPropertyChanged(sender, e);
		}

		void Button_Clicked(object sender, EventArgs e)
		{
			_dowButton.IsSelected = !_dowButton.IsSelected;
		}

		protected override void Dispose(bool disposing)
		{
			_dowButton.Clicked -= Button_Clicked;
			base.Dispose(disposing);
		}
	}
}
