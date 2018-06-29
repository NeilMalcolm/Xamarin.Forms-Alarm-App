using System;
using Android.Content.Res;
using Android.Media;
using Xamarin.Forms;
using System.Linq;
using AlarmApp.Models;
using AlarmApp.Droid.Services;
using AlarmApp.Services;
using System.Threading.Tasks;
using System.Threading;

[assembly: Xamarin.Forms.Dependency(typeof(PlaySoundServiceAndroid))]
namespace AlarmApp.Droid.Services
{
	public class PlaySoundServiceAndroid : IPlaySoundService
	{
		static CancellationTokenSource tokenSource2 = new CancellationTokenSource();
		CancellationToken ct = tokenSource2.Token;

		MediaPlayer _mediaPlayer = new MediaPlayer();
		AssetFileDescriptor _assetFileDescriptor;
		Task playSoundTask;

		public void PlayAudio(AlarmTone alarmTone)
		{
			PlayAudio(alarmTone, false);
		}

		public void PlayAudio(AlarmTone alarmTone, bool isLooping)
		{
			StopAudio();

			var isADefaultTone = Defaults.Tones.FirstOrDefault(x => x.Path == alarmTone.Path) != default(AlarmTone);

			if (isADefaultTone)
			{
				_assetFileDescriptor = Forms.Context.Assets.OpenFd(alarmTone.Path);
				_mediaPlayer.SetDataSource(_assetFileDescriptor.FileDescriptor, _assetFileDescriptor.StartOffset, _assetFileDescriptor.Length);
			}
			else
			{
				string alarmTonePath;
				string[] split = alarmTone.Path.Split(':');
				alarmTonePath = split[1];
				_mediaPlayer.SetDataSource(alarmTonePath);
			}

			_mediaPlayer.Looping = isLooping;
			_mediaPlayer.Prepare();
			_mediaPlayer.Start();
		}

		public void StopAudio()
		{
			if(_mediaPlayer.IsPlaying)
				_mediaPlayer.Stop();
			
			_mediaPlayer.Reset();
		}
	}
}
