using System;
using AlarmApp.Controls;
using AlarmApp.Droid.Renderers;
using Android.Content;
using Android.Support.V4.Content.Res;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms.Platform.Android.AppCompat;

[assembly: ExportRenderer(typeof(MusicSelectionButton), typeof(MusicSelectionRenderer))]
namespace AlarmApp.Droid.Renderers
{
	public class MusicSelectionRenderer : Xamarin.Forms.Platform.Android.AppCompat.ButtonRenderer
	{
		public MusicSelectionRenderer(Context context) : base(context)
		{
		}

		protected override void OnElementChanged(ElementChangedEventArgs<Button> e)
		{
			base.OnElementChanged(e);

			if(Control != null)
			{
				// if the text color has not been overridden, display this default
				if(Element.TextColor == (Color)Button.TextColorProperty.DefaultValue)
				{
					Control.SetTextColor(Xamarin.Forms.Color.FromHex("#B1C3FF").ToAndroid());
				}
				Control.SetAllCaps(false);

				// set background and drawables
				Control.SetBackground(ResourcesCompat.GetDrawable(Resources, Resource.Drawable.music_selection_background, null));
				Control.SetCompoundDrawablesRelativeWithIntrinsicBounds(ResourcesCompat.GetDrawable(Resources, Resource.Drawable.music, null), null, null, null);
				Control.SetPaddingRelative(PaddingStart + 80, PaddingTop, PaddingEnd, PaddingBottom);

				// remove state list shadow
				Control.StateListAnimator = null;
			}
		}
	}
}
