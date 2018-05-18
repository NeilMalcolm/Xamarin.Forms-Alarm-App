using System;
using Xamarin.Forms;
using AlarmApp.Controls;
using AlarmApp.Droid.Renderers;
using Xamarin.Forms.Platform.Android;
using Android.Graphics;
using System.ComponentModel;
using Android.Support.V4.Content.Res;
using Android.Content;
using XFColor = Xamarin.Forms.Color;

[assembly: ExportRenderer(typeof(CircleLabel), typeof(AndroidCircleLabelRenderer))]
namespace AlarmApp.Droid.Renderers
{
	public class AndroidCircleLabelRenderer : LabelRenderer
	{
		public AndroidCircleLabelRenderer(Context context) : base(context)
		{
		}

		protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
		{
			base.OnElementChanged(e);

			if (Control != null)
			{
				SetCircleBackground();
			}
		}

		protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			base.OnElementPropertyChanged(sender, e);

			if(e.PropertyName == CircleLabel.BackgroundColorProperty.PropertyName)
			{
				if(Element.BackgroundColor != XFColor.Transparent)
				{
					SetCircleBackground();
				}
			}
		}


		void SetCircleBackground()
		{
			//Set our control background to round drawable and overlay our background color as long as our background color isn't transparent
			if (Element.BackgroundColor != default(Xamarin.Forms.Color) && Element.BackgroundColor != Xamarin.Forms.Color.Transparent)
			{
				Control.Background = ResourcesCompat.GetDrawable(Resources, Resource.Drawable.circle_background, null);
				Control.Background.SetColorFilter(Element.BackgroundColor.ToAndroid(), PorterDuff.Mode.Src);
				Element.BackgroundColor = XFColor.Transparent;
			}
		}

	}
}
