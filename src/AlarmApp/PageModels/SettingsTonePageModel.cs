using System;
using AlarmApp.Services;
using FreshMvvm;
using PropertyChanged;
using AlarmApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using AlarmApp.Helpers;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using System.Linq;

namespace AlarmApp.PageModels
{
	[AddINotifyPropertyChangedInterface]
	public class SettingsTonePageModel : FreshBasePageModel
	{
		IFileLocator _fileLocator = DependencyService.Get<IFileLocator>();
		IPlaySoundService _soundService = DependencyService.Get<IPlaySoundService>();

		IAlarmStorageService _alarmStorage;
		AlarmTone _selectedTone;

		public Settings Settings { get; set; }

		public AlarmTone SelectedTone
		{
			get { return _selectedTone; }
			set 
			{
				if(value != null)
					SetSelectedTone(value);
			}
		}

		public ObservableCollection<AlarmTone> AllAlarmTones { get; set; }

		public ICommand ToneSavedCommand 
		{ 
			get
			{
				return new FreshAwaitCommand(async (obj, tcs) =>
				{
					var selectedTone = _selectedTone;
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
			AllAlarmTones = new ObservableCollection<AlarmTone>(_alarmStorage.GetAllTones());
			
		}


		void PlayTone(AlarmTone tone)
		{
			_soundService.PlayAudio(tone);
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
				_fileLocator.OpenFileLocator();
				_fileLocator.FileChosen += ToneFileChosen;
				return;
			}

			//_selectedTone = value;
			PlayTone(value);
			AddConfirmToolbarItem();

			_selectedTone = value;
		}

		void AddConfirmToolbarItem()
		{
			if(CurrentPage.ToolbarItems.Count == 0)
			{
				CurrentPage.ToolbarItems.Add(new ToolbarItem
				{
					Text = "Save",
					Icon = "save",
					Command = ToneSavedCommand
				});
			}
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
				_alarmStorage.GetSettings().AlarmTone = alarmTone;
			});
		}

		void ToneFileChosen(Uri uri)
		{
			System.Diagnostics.Debug.WriteLine("pcl: " + uri.LocalPath);

			var newTone = new AlarmTone
			{
				Name = uri.AbsolutePath,
				Path = uri.LocalPath,
				IsCustomTone = true
			};

			AllAlarmTones.Add(newTone);
			_alarmStorage.AddTone(newTone);

			_fileLocator.FileChosen -= ToneFileChosen;
			_selectedTone = AllAlarmTones.Last();
		}

		protected override void ViewIsDisappearing(object sender, EventArgs e)
		{
			_soundService.StopAudio();
			base.ViewIsDisappearing(sender, e);
		}
	}
}
