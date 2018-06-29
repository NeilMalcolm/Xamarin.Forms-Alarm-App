using System;
using AlarmApp.Models;

namespace AlarmApp.Services
{
	public interface IPlaySoundService
	{
		void PlayAudio(AlarmTone alarmTone);
		void PlayAudio(AlarmTone alarmTone, bool isLooping);
		void StopAudio();
	}
}
