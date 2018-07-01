using System;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace AlarmApp.Droid
{
	[Activity(Label = "AlarmApp", Icon = "@drawable/icon", Theme = "@style/splashscreen", ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation, LaunchMode = LaunchMode.SingleTop)]
	public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
	{
		protected override void OnCreate(Bundle bundle)
		{
			TabLayoutResource = Resource.Layout.Tabbar;
			ToolbarResource = Resource.Layout.Toolbar;
        
			base.Window.RequestFeature(WindowFeatures.ActionBar);
			base.SetTheme(Resource.Style.MyTheme);

			Rg.Plugins.Popup.Popup.Init(this, bundle);
			base.OnCreate(bundle);
			Xamarin.Forms.Forms.SetFlags("FastRenderers_Experimental");
			global::Xamarin.Forms.Forms.Init(this, bundle);

			LoadApplication(new App());
		}

		public event Action<Uri> FileChosen;

		protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
		{
			base.OnActivityResult(requestCode, resultCode, data);

			if(requestCode == 42 && resultCode == Result.Ok)
			{
				if (data == null) return;

				Uri uri = null;
				var stringUri = data.ToUri(IntentUriType.None);
				uri = new Uri(stringUri);

				FileChosen?.Invoke(uri);
			}
		}

		public override void OnBackPressed()
		{
			var thereArePopupsOnScreen = Rg.Plugins.Popup.Services.PopupNavigation.Instance.PopupStack.Count > 0;
			if (thereArePopupsOnScreen)
			{
				Rg.Plugins.Popup.Services.PopupNavigation.Instance.PopAsync();
			}
			else
			{
				base.OnBackPressed();
			}
		}
	}
}
