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
		AlarmTone _selectedTone;

		public Settings Settings { get; set; }

		public AlarmTone SelectedTone
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
					var selectedTone = (AlarmTone)obj;
					OnToneSelected(selectedTone);
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
		void SetSelectedTone(AlarmTone value)
		{
			var isSelectedNull = value.Equals(default(AlarmTone)) || value == null;
			if (isSelectedNull) return;

			var wasSelectCustomToneSelected = value.Equals(Defaults.Tones[0]);
			if(wasSelectCustomToneSelected)
			{
				value = GetAlarmToneNotSetTone();
			}

			//_selectedTone = value;
			ToneSelectedCommand.Execute(value);
			System.Diagnostics.Debug.WriteLine("DONE TONE");
		}

		/// <summary>
		/// Allows the user to set a custom tone
		/// </summary>
		AlarmTone GetAlarmToneNotSetTone()
		{
			//temporary until final solution has been implemented
			return new AlarmTone("In future, you will be able to select custom tones", null);
		}

		/// <summary>
		/// Saves the tone to settings when the tone has been selected
		/// </summary>
		/// <param name="alarmTone">The selected alarm tone</param>
		void OnToneSelected(AlarmTone alarmTone)
		{
			_alarmStorage.Realm.Write(() =>
			{
				Settings.AlarmTone = alarmTone;
			});
		}
	}
}
