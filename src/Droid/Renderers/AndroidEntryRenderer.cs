using AlarmApp.Droid.Renderers;
using Android.Content;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using AGraphics = Android.Graphics;

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

			Control.SetPaddingRelative(5, 5, 5, 5);
			Control.SetHintTextColor(Color.FromHex("#565C78").ToAndroid());
			Control.Background = Resources.GetDrawable(Resource.Drawable.control_selector);
		}
	}
}
