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

		public AlarmSetterAndroid()
		{
			
		}

		public void SetAlarm(Alarm alarm)
		{
			var alarmIntent = new Intent(Forms.Context, typeof(AlarmReceiver));
			alarmIntent.SetFlags(ActivityFlags.IncludeStoppedPackages);
			alarmIntent.PutExtra("message", "bing bong");
			alarmIntent.PutExtra("hours", alarm.Time.Hours);
			alarmIntent.PutExtra("mins", alarm.Time.Minutes);
			PendingIntent pendingIntent = PendingIntent.GetBroadcast(Forms.Context, GetAlarmId(), alarmIntent, PendingIntentFlags.UpdateCurrent);
			AlarmManager alarmManager = (AlarmManager)Forms.Context.GetSystemService(Context.AlarmService);

			var difference = alarm.Time.Subtract(DateTime.Now.ToLocalTime().TimeOfDay);
			var differenceAsMillis = difference.TotalMilliseconds;

			alarm.Time.Add(new TimeSpan(1, 0, 0));
			alarmManager.SetExact(AlarmType.RtcWakeup, Java.Lang.JavaSystem.CurrentTimeMillis() + (long)differenceAsMillis, pendingIntent);
		}

		public void SetRepeatingAlarm(Alarm alarm)
		{
			// if our end time is before our star ttime
			//if (start.CompareTo(end) > 0) return;


			//// if our two times
			//if(end.Subtract(start).TotalHours < 24)
			//{
				
			//}
			//else
			//{
				
			//}
		}

		public void DeleteAlarm(Alarm alarm)
		{
			var deleteAlarmIntent = new Intent(Android.Provider.AlarmClock.ActionDismissAlarm);
			deleteAlarmIntent.SetFlags(ActivityFlags.NewTask);
			deleteAlarmIntent.PutExtra(Android.Provider.AlarmClock.ExtraSkipUi, true);

			var alarmHasName = string.IsNullOrWhiteSpace(alarm.Name);
			if(alarmHasName)
			{
				deleteAlarmIntent.PutExtra(Android.Provider.AlarmClock.AlarmSearchModeLabel, alarm.Name);
			}
			else {
				deleteAlarmIntent.PutExtra(Android.Provider.AlarmClock.ExtraDays, DateTime.Now.Day);
				deleteAlarmIntent.PutExtra(Android.Provider.AlarmClock.ExtraHour, alarm.Time.Hours);
				deleteAlarmIntent.PutExtra(Android.Provider.AlarmClock.ExtraMinutes, alarm.Time.Minutes);
			}

			Forms.Context.StartActivity(deleteAlarmIntent);
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
