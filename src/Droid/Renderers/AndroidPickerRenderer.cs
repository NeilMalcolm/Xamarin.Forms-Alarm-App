using AlarmApp.Controls;
using AlarmApp.Droid.Renderers;
using Android.Content;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using AGraphics = Android.Graphics;

[assembly: ExportRenderer(typeof(Picker), typeof(AndroidPickerRenderer))]
namespace AlarmApp.Droid.Renderers
{
	public class AndroidPickerRenderer : Xamarin.Forms.Platform.Android.AppCompat.PickerRenderer
	{
		AGraphics.Color _backgroundColor = new AGraphics.Color(255, 255, 255, 25);

		public AndroidPickerRenderer(Context context) : base(context)
		{

		}

		protected override void OnElementChanged(ElementChangedEventArgs<Picker> e)
		{
			base.OnElementChanged(e);

			if (Control == null) return;

			if(Element is CustomPicker)
			{
				var picker = (CustomPicker)Element;
				if(picker.Hint != null)
					Control.Hint = picker.Hint;

				Control.TextSize = (float) picker.FontSize;
			}

			Control.SetPaddingRelative(5, 5, 5, 5);
			Control.SetHintTextColor(Color.FromHex("#565C78").ToAndroid());
			Control.Background = Resources.GetDrawable(Resource.Drawable.control_selector);
		}
	}
}
