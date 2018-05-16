
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Content.Res;
using Android.Media;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace AlarmApp.Droid
{
	[Activity(Label = "AlarmActivity", NoHistory = true, Theme = "@style/MyTheme", ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class AlarmActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
	{
		MediaPlayer _mediaPlayer = new MediaPlayer();
		Vibrator _vibrator;
		readonly long[] _pattern =
		{
			0, 500, 500
		};

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			SetContentView(Resource.Layout.AlarmActivity);
			var closeButton = FindViewById<Button>(Resource.Id.closeButton);
			closeButton.Click += CloseButton_Click;

			Intent intent = Intent;
			Bundle bundle = intent.Extras;

			if (bundle != null)
			{
				int hours = (int)bundle.Get("hours");
				int mins = (int)bundle.Get("mins");
				var textView = FindViewById<TextView>(Resource.Id.timeTextView);
				textView.Text = hours + ":" + mins;
			}

			_vibrator = Vibrator.FromContext(this);
			AssetFileDescriptor assetFileDescriptor = Assets.OpenFd("alarm_tone.m4a");
			_mediaPlayer.Looping = true;
			_mediaPlayer.SetDataSource(assetFileDescriptor.FileDescriptor, assetFileDescriptor.StartOffset, assetFileDescriptor.Length);
			_mediaPlayer.Prepare();
			_mediaPlayer.Start();

			_vibrator.Vibrate(_pattern, 0);
		}

		void CloseButton_Click(object sender, EventArgs e)
		{
			//removes our app from the scree and from 'recent apps' section
			if(Android.OS.Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop)
			{
				FinishAndRemoveTask();	
			}
			else
			{
				Finish();
			}
			_vibrator.Cancel();
			Java.Lang.JavaSystem.Exit(0);
		}

		protected override void Dispose(bool disposing)
		{

			//close mediaplayer? and vibrator?
			base.Dispose(disposing);
		}
	}
}
