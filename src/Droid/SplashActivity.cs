
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;

namespace AlarmApp.Droid.Resources
{
	[Activity(Label = "AlarmApp", Icon = "@drawable/icon", Theme = "@style/splashscreen", MainLauncher = true, NoHistory = true)]
	public class SplashActivity : AppCompatActivity
	{
		protected override void OnResume()
		{
			base.OnResume();
			StartActivity(typeof(MainActivity));
		}
	}
}
