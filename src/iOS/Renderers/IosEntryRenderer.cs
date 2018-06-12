using System;
using AlarmApp.iOS.Renderers;
using AlarmApp.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(Entry), typeof(IosEntryRenderer))]
namespace AlarmApp.iOS.Renderers
{
	public class IosEntryRenderer : EntryRenderer
	{
		protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
		{
			base.OnElementChanged(e);

			if (Control != null)
			{
				Control.BackgroundColor = UIKit.UIColor.Clear;
			}
		}
	}
}
