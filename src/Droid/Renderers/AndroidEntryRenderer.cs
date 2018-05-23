using AlarmApp.Droid.Renderers;
using Android.Content;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using AlarmApp.Controls;
using AGraphics = Android.Graphics;
using System.ComponentModel;
using Android.Support.V4.Content.Res;

[assembly: ExportRenderer(typeof(Entry), typeof(AndroidEntryRenderer))]
namespace AlarmApp.Droid.Renderers
{
	public class AndroidEntryRenderer : Xamarin.Forms.Platform.Android.EntryRenderer
	{
		AGraphics.Color _backgroundColor = new AGraphics.Color(255, 255, 255, 25);

		public AndroidEntryRenderer(Context context) : base(context)
		{

		}

		protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
		{
			base.OnElementChanged(e);

			if (Control == null) return;
			if(Element is CustomEntry)
			{
				(Element as CustomEntry).IsValidChanged += OnIsValidChanged;
			}
			Control.Gravity = Android.Views.GravityFlags.CenterVertical | Android.Views.GravityFlags.Left;
			Control.SetPaddingRelative(5, 5, 5, 5);
			Control.SetHintTextColor(Color.FromHex("#565C78").ToAndroid());
			Control.Background = ResourcesCompat.GetDrawable(Resources, Resource.Drawable.control_selector, null);
		}

		protected override void Dispose(bool disposing)
		{
			if (Element is CustomEntry)
			{
				(Element as CustomEntry).IsValidChanged -= OnIsValidChanged;
			}
			base.Dispose(disposing);
		}

		void OnIsValidChanged(object sender, System.EventArgs e)
		{
			if (!(bool)(Element as CustomEntry).IsValid)
			{
				Control.Background = ResourcesCompat.GetDrawable(Resources, Resource.Drawable.control_background_invalid, null);
			}
			else
			{
				Control.Background.ClearColorFilter();
			}
		}

		protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			base.OnElementPropertyChanged(sender, e);

			if(Element is CustomEntry)
			{
				if (e.PropertyName == Entry.IsFocusedProperty.PropertyName)
				{
					if (!Element.IsFocused)
					{
						if (!string.IsNullOrWhiteSpace(Element.Text) && int.Parse(Element.Text) > 0)
						{
							(Element as CustomEntry).IsValid = true;
						}
						else
						{
							(Element as CustomEntry).IsValid = false;
						}
					}
				}
			}
		}
	}
}
