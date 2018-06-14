using System;
using FreshMvvm;
using PropertyChanged;
using AlarmApp.Models;
using System.Threading.Tasks;
using AlarmApp.Services;
using System.Windows.Input;

namespace AlarmApp.PageModels
{
	[AddINotifyPropertyChangedInterface]
	public class SettingsPageModel : FreshBasePageModel
	{
		IAlarmStorageService _alarmStorage;

		public Settings Settings { get; set; }

		public ICommand CellTappedCommand 
		{ 
			get
			{
				return new FreshAwaitCommand(async (param, tcs) =>
				{
					var parameter = (string)param;
					await OnCellTapped(parameter);

					tcs.SetResult(true);
				});
			}
		}

		public SettingsPageModel(IAlarmStorageService alarmStorage)
		{
			_alarmStorage = alarmStorage;
		}

		protected override void ViewIsAppearing(object sender, EventArgs e)
		{
			base.ViewIsAppearing(sender, e);

			Settings = _alarmStorage.GetSettings();
		}

		async Task OnCellTapped(string parameter)
		{
			switch (parameter)
			{
				case "Clock Format":
					SwitchFormat();
					break;
				case "Alarm Tone":
					ChangeAlarmTone();
					break;
				case "Vibrate":
					ToggleVibrate();
					break;
				case "Delete":
					await DoDeleteAlert();
					break;
			}
			return;
		}

		async Task DoDeleteAlert()
		{
			var shouldDeleteAlarms = await CoreMethods.DisplayAlert("Are you sure?", 
			                               "You are about to delete all your alarms, " +
			                               "this action is permanent and cannot be undone.", 
			                               "DELETE", "CANCEL");

			if(shouldDeleteAlarms)
			{
				DeleteAlarms();
			}
		}

		void DeleteAlarms()
		{
			_alarmStorage.DeleteAllAlarms();
		}

		void ChangeAlarmTone()
		{

			//_alarmStorage.UpdateSettings(Settings);
		}

		void ToggleVibrate()
		{
			_alarmStorage.Realm.Write(() =>
			{
				Settings.IsVibrateOn = !Settings.IsVibrateOn;
			});
		}

		void SwitchFormat()
		{
			_alarmStorage.Realm.Write(() =>
			{
				Settings.SwitchFormat();
			});
		}
	}
}
