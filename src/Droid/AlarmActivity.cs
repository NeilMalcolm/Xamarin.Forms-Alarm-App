
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
using AlarmApp.Services;
using Android.Provider;
using AlarmApp.Models;

namespace AlarmApp.Droid
{
	[Activity(Label = "AlarmActivity", NoHistory = true, Theme = "@style/MyTheme", ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class AlarmActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
	{
		AlarmStorageService _alarmStorageService = new AlarmStorageService();
		Alarm _alarm;
		AlarmApp.Models.Settings _settings;

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

			// add flags to turn screen on and appear over lock screen
			Window.AddFlags(WindowManagerFlags.ShowWhenLocked);
			Window.AddFlags(WindowManagerFlags.DismissKeyguard);
			Window.AddFlags(WindowManagerFlags.KeepScreenOn);
			Window.AddFlags(WindowManagerFlags.TurnScreenOn);

			Intent intent = Intent;
			Bundle bundle = intent.Extras;

			if (bundle != null)
			{
				var id = (string)bundle.Get("id");
				var textView = FindViewById<TextView>(Resource.Id.timeTextView);
				_alarm = _alarmStorageService.GetAlarm(id);
				textView.Text = _alarm.TimeOffset.ToLocalTime().ToString(@"hh\:mm");
			}
			_settings = _alarmStorageService.GetSettings();
			string alarmTonePath = "alarm_tone.m4a";
			var alarmTone = _settings.AlarmTone;
			if(alarmTone != null)
			{
				if(alarmTone.IsCustomTone)
				{
					
					string[] split = alarmTone.Path.Split(':');
					string type = split[0];

					if (type.Contains("primary"))
					{
						alarmTonePath = Android.OS.Environment.ExternalStorageDirectory + "/" + split[1];
						_mediaPlayer.SetDataSource(alarmTonePath);
					}
				}
				else 
				{
					
					alarmTonePath = _settings.AlarmTone.Path;
					AssetFileDescriptor assetFileDescriptor = Assets.OpenFd(alarmTonePath);
					_mediaPlayer.SetDataSource(assetFileDescriptor.FileDescriptor, assetFileDescriptor.StartOffset, assetFileDescriptor.Length);
				}
			}

			_mediaPlayer.Looping = true;
			_mediaPlayer.Prepare();
			_mediaPlayer.Start();

			if (!_settings.IsVibrateOn) return;


			_vibrator = Vibrator.FromContext(this);
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

			if(_settings.IsVibrateOn)
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
