using System;
using System.Text;
using AlarmApp.Droid.Services;
using AlarmApp.Services;
using Android.App;
using Android.Content;
using Android.Icu.Util;
using Android.Support.V4.Content;
using Android.Util;
using Android.Widget;
using Plugin.CurrentActivity;
using Xamarin.Forms;
using AlarmApp.Models;

[assembly: Xamarin.Forms.Dependency(typeof(AlarmSetterAndroid))]
namespace AlarmApp.Droid.Services
{
	public class AlarmSetterAndroid : IAlarmSetter
	{
		public static string AlarmTag = "Al4rm";
		Calendar _now = Calendar.GetInstance(Android.Icu.Util.TimeZone.Default, Java.Util.Locale.Default);

		string _previous;
		public AlarmSetterAndroid()
		{
			
		}

		public void SetAlarm(DateTime time)
		{
			//time = DateTime.Now;
			//time.AddSeconds(10);
			Log.Debug(AlarmTag, "SET THE THING");
			var alarmIntent = new Intent(Forms.Context, typeof(AlarmReceiver));
			alarmIntent.SetFlags(ActivityFlags.IncludeStoppedPackages);
			alarmIntent.PutExtra("message", "bing bong");
			alarmIntent.PutExtra("hours", time.Hour);
			alarmIntent.PutExtra("mins", time.Minute);
			PendingIntent pendingIntent = PendingIntent.GetBroadcast(Forms.Context, GetAlarmId(), alarmIntent, PendingIntentFlags.UpdateCurrent);
			AlarmManager alarmManager = (AlarmManager)Forms.Context.GetSystemService(Context.AlarmService);

			//var openAlarmIntent = new Intent(Android.Provider.AlarmClock.ActionSetAlarm);
			//openAlarmIntent.SetFlags(ActivityFlags.NewTask);
			//openAlarmIntent.PutExtra(Android.Provider.AlarmClock.ExtraSkipUi, true);
			//openAlarmIntent.PutExtra(Android.Provider.AlarmClock.ExtraDays, time.Day);
			//openAlarmIntent.PutExtra(Android.Provider.AlarmClock.ExtraHour, time.Hour);
			//openAlarmIntent.PutExtra(Android.Provider.AlarmClock.ExtraMinutes, time.Minute);


			//var guid = Guid.NewGuid();
			//_previous = guid.ToString();
			//openAlarmIntent.PutExtra(Android.Provider.AlarmClock.ExtraMessage, "AlarmApp: " + _previous);

			var difference = time.Subtract(DateTime.Now);
			var differenceAsMillis = difference.TotalMilliseconds;

			//Forms.Context.StartActivity(openAlarmIntent);
			time.AddHours(1);
			alarmManager.SetExact(AlarmType.RtcWakeup, Java.Lang.JavaSystem.CurrentTimeMillis() + (long)differenceAsMillis, pendingIntent);
			//alarmManager.Set(AlarmType.RtcWakeup, )
			Log.Debug(AlarmTag, "SET ALARM ANDROID: " + time.ToString());
			//Log.Debug(AlarmTag, "milliseconds: " + (long)time.TimeOfDay.Milliseconds + ", current time in millis: " + (long)DateTime.Now.TimeOfDay.TotalMilliseconds);
			Log.Debug(AlarmTag, "time diff: " + (long)differenceAsMillis);
			Log.Debug(AlarmTag, "System current time in millis: " + Java.Lang.JavaSystem.CurrentTimeMillis() + " alarm set at: " + Java.Lang.JavaSystem.CurrentTimeMillis() + (long)differenceAsMillis);

		}

		public void SetRepeatingAlarm(DateTime start, DateTime end, TimeSpan interval)
		{
			// if our end time is before our star ttime
			if (start.CompareTo(end) > 0) return;


			// if our two times
			if(end.Subtract(start).TotalHours < 24)
			{
				
			}
			else
			{
				
			}
		}

		public void DeleteAlarm(DateTime time)
		{
			var openAlarmIntent = new Intent(Android.Provider.AlarmClock.ActionDismissAlarm);
			openAlarmIntent.SetFlags(ActivityFlags.NewTask);
			//openAlarmIntent.PutExtra(Android.Provider.AlarmClock.ExtraSkipUi, true);
			//openAlarmIntent.PutExtra(Android.Provider.AlarmClock.ExtraDays, time.Day);
			//openAlarmIntent.PutExtra(Android.Provider.AlarmClock.ExtraHour, time.Hour);
			//openAlarmIntent.PutExtra(Android.Provider.AlarmClock.ExtraMinutes, time.Minute);
			//openAlarmIntent.PutExtra(Android.Provider.AlarmClock.ExtraMessage, _previous);

			openAlarmIntent.PutExtra(Android.Provider.AlarmClock.AlarmSearchModeLabel, _previous);

			Forms.Context.StartActivity(openAlarmIntent);
		}

		int GetAlarmId()
		{
			return (int)DateTime.Now.TimeOfDay.TotalMilliseconds;
		}

		string GetTimeDifferenceAsString(DateTime alarmTime)
		{
			var timeFromNow = GetTimeDifferenceInHours(alarmTime);

			StringBuilder sb = new StringBuilder();

			if(timeFromNow.Days > 0)
			{
				Log.Debug(AlarmTag, "total days: " + timeFromNow.Days);
				sb.Append(timeFromNow.Days);
				sb.Append(" days");
			}

			if(timeFromNow.Hours > 0)
			{
				Log.Debug(AlarmTag, "total hours: " + timeFromNow.Hours);
				if(sb.Length > 0)
				{
					sb.Append(" ");
				}
				sb.Append(timeFromNow.Hours);
				sb.Append(" hours");
			}

			if(timeFromNow.Minutes > 0)
			{
				Log.Debug(AlarmTag, "total minutes: " + timeFromNow.Minutes);
				if (sb.Length > 0)
				{
					sb.Append(" ");
				}
				sb.Append(timeFromNow.Minutes);
				sb.Append(" minutes");
			}

			if (timeFromNow.Seconds > 0)
			{
				Log.Debug(AlarmTag, "total seconds: " + timeFromNow.Seconds);
				if (sb.Length > 0)
				{
					sb.Append(" ");
				}
				sb.Append(timeFromNow.Seconds);
				sb.Append(" seconds");
			}

			return sb.ToString();
		}

		TimeSpan GetTimeDifferenceInHours(DateTime alarmTime)
		{
			var now = DateTime.Now.TimeOfDay;
			var alarm = alarmTime.TimeOfDay;
			var diff = alarm.Subtract(now);

			return diff;
		}
	}

	[BroadcastReceiver]
	public class AlarmReceiver : BroadcastReceiver
	{
		public override void OnReceive(Context context, Intent intent)
		{

			//App.Current.MainPage.Navigation.PushAsync(new AlarmAppPage());
			Log.Debug(AlarmSetterAndroid.AlarmTag, "OPEN THE THING");
			var hours = intent.GetIntExtra("hours", 0);
			var mins = intent.GetIntExtra("mins", 0);
			Log.Debug(AlarmSetterAndroid.AlarmTag, "DO ALARM: " + hours + " - " + mins);
			var disIntent = new Intent(context, typeof(AlarmActivity));
			disIntent.PutExtra("hours", hours);
			disIntent.PutExtra("mins", mins);
			disIntent.SetFlags(ActivityFlags.NewTask);
			context.StartActivity(disIntent);
			Log.Debug(AlarmSetterAndroid.AlarmTag, "current time in millis: " + (long)DateTime.Now.TimeOfDay.TotalMilliseconds);
		}
	} 
}
