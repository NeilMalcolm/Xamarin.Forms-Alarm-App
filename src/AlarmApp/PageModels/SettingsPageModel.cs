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

		protected async override void ViewIsAppearing(object sender, EventArgs e)
		{
			base.ViewIsAppearing(sender, e);

			Settings = _alarmStorage.GetSettings();
		}

		async Task OnCellTapped(string parameter)
		{
			await Task.Delay(1000);
			System.Diagnostics.Debug.WriteLine(parameter);
			return;
		}
	}
}
