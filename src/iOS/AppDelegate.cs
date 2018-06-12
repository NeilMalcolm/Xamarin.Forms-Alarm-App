using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;
using Xamarin.Forms.Platform.iOS;

namespace AlarmApp.iOS
{
	[Register("AppDelegate")]
	public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
	{
		UIColor _primary = Xamarin.Forms.Color.FromHex("#1F2230").ToUIColor();

		public override bool FinishedLaunching(UIApplication app, NSDictionary options)
		{
			global::Xamarin.Forms.Forms.Init();

			LoadApplication(new App());
			UITabBar.Appearance.BarTintColor = _primary;
			UITabBar.Appearance.TintColor = UIColor.White;
			UINavigationBar.Appearance.BarTintColor = _primary;
			UINavigationBar.Appearance.TintColor = UIColor.White;
			UINavigationBar.Appearance.TitleTextAttributes = new UIStringAttributes
			{
				ForegroundColor = UIColor.White,

			};

			UIApplication.SharedApplication.SetStatusBarStyle(UIStatusBarStyle.LightContent, true);

			return base.FinishedLaunching(app, options);
		}
	}
}
