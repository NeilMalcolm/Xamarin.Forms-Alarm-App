using AlarmApp.Droid.Renderers;
using Android.Content;
using Android.Support.V4.Content.Res;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using AlarmApp.Controls;
using System.ComponentModel;
using AColor = Android.Graphics.Color;

[assembly: ExportRenderer(typeof(DayOfWeekButton), typeof(AndroidDayOfTheWeekButtonRenderer))]
namespace AlarmApp.Droid.Renderers
{
	public class AndroidDayOfTheWeekButtonRenderer : Xamarin.Forms.Platform.Android.ButtonRenderer
	{
		DayOfWeekButton _dowButton;
		AColor _selectedColor = new AColor(206, 217, 255);
		AColor _selectedTextColor = new AColor(29, 33, 48);
		AColor _deselectedColor =((Color)App.Current.Resources["ContentPageBackgroundColor"]).ToAndroid();

		public AndroidDayOfTheWeekButtonRenderer(Context context) : base(context)
		{

		}

		protected override void OnElementChanged(ElementChangedEventArgs<Button> e)
		{
			base.OnElementChanged(e);

			if (Control == null) return;

			if (Element != null) 
				_dowButton = (DayOfWeekButton)Element;

			if(Control.Width > Control.Height)
				Control.SetHeight(Control.Width);
			else 
				Control.SetWidth(Control.Height);
			
			_dowButton.Clicked += Element_Clicked;

			Control.StateListAnimator = null;
			Control.SetPaddingRelative(0, 0, 0, 0);
			Control.TextSize = 20;
			Control.Background = ResourcesCompat.GetDrawable(Resources, Resource.Drawable.circle_background, null);
			SetPadding(0, 0, 0, 0);
			ButtonDeselected();
			Control.Elevation = 0;
		}

		/// <summary>
		/// When the button is pressed
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="e">E.</param>
		void Element_Clicked(object sender, System.EventArgs e)
		{
			_dowButton.IsSelected = !_dowButton.IsSelected;
		}

		protected override void Dispose(bool disposing)
		{
			_dowButton.Clicked -= Element_Clicked;
			base.Dispose(disposing);
		}

		protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			base.OnElementPropertyChanged(sender, e);

			if (e.PropertyName == DayOfWeekButton.IsSelectedProperty.PropertyName) 
			{
				if(_dowButton.IsSelected)
				{
					ButtonSelected();
				}
				else 
				{
					ButtonDeselected();
				}
			}
		}

		void ButtonSelected()
		{
			Control.SetTextColor(_selectedTextColor);
			Control.Background.SetColorFilter(_selectedColor, Android.Graphics.PorterDuff.Mode.Src);
		}

		void ButtonDeselected()
		{
			Control.SetTextColor(_selectedColor);
			Control.Background.SetColorFilter(_deselectedColor, Android.Graphics.PorterDuff.Mode.Src);
		}
	}
}
