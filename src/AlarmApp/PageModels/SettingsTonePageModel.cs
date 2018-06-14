using System;
using AlarmApp.Services;
using FreshMvvm;
using PropertyChanged;
using AlarmApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using AlarmApp.Helpers;

namespace AlarmApp.PageModels
{
	[AddINotifyPropertyChangedInterface]
	public class SettingsTonePageModel : FreshBasePageModel
	{
		IAlarmStorageService _alarmStorage;
		KeyValuePair<string, string> _selectedTone;

		public Settings Settings { get; set; }

		public KeyValuePair<string, string> SelectedTone
		{
			get { return _selectedTone; }
			set 
			{
				SetSelectedTone(value);
			}
		}

		public ICommand ToneSelectedCommand 
		{ 
			get
			{
				return new FreshAwaitCommand(async (obj, tcs) =>
				{
					var kvp = (KeyValuePair<string, string>)obj;
					OnToneSelected(kvp.Key);
					await CoreMethods.PopPageModel(false, true);
					tcs.SetResult(true);
				});
			}
		}


		public SettingsTonePageModel(IAlarmStorageService alarmStorage)
		{
			_alarmStorage = alarmStorage;
		}

		public override void Init(object initData)
		{
			base.Init(initData);

			Settings = _alarmStorage.GetSettings();
		}

		/// <summary>
		/// Handles setting the current tone value
		/// </summary>
		/// <param name="value">Value.</param>
		void SetSelectedTone(object value)
		{
			var isSelectedNull = value.Equals(default(KeyValuePair<string, string>));
			if (isSelectedNull) return;

			var wasSelectCustomToneSelected = value.Equals(Defaults.Tones[0]);
			if(wasSelectCustomToneSelected)
			{
				DoSetCustomTone();
			}
			else 
			{
				_selectedTone = (KeyValuePair<string, string>)value;
				ToneSelectedCommand.Execute(value);
			}
		}

		/// <summary>
		/// Allows the user to set a custom tone
		/// </summary>
		void DoSetCustomTone()
		{
			//temporary until final solution has been implemented
			ToneSelectedCommand.Execute(
				new KeyValuePair<string, string>("In future, you will be able to select custom tones",
				                                "value"));
		}

		/// <summary>
		/// Saves the tone to settings when the tone has been selected
		/// </summary>
		/// <param name="alarmTone">The selected alarm tone</param>
		void OnToneSelected(string alarmTone)
		{
			_alarmStorage.Realm.Write(() =>
			{
				Settings.AlarmTone = alarmTone;
			});
		}
	}
}
