using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using AlarmApp.Models;
using FreshMvvm;

namespace AlarmApp.PageModels
{
	public class AlarmListPageModel : FreshBasePageModel
	{
		AlarmListType _alarmListType;

		ObservableCollection<Alarm> _alarms = null;
		Alarm _selectedAlarm;

		public ObservableCollection<Alarm> Alarms
		{
			get { return _alarms; }
			set { _alarms = value; RaisePropertyChanged(); }
		}

		public Alarm SelectedAlarm
		{
			get { return _selectedAlarm; }
			set
			{
				_selectedAlarm = value;
				if(value != null)
					OpenPage(value);
			}
		}

		public ICommand NewAlarmCommand 
		{ 
			get{
				return new FreshAwaitCommand(async (o, tcs) => 
				{
					await CoreMethods.PushPageModel<NewAlarmPageModel>(null, false, true);
					tcs.SetResult(true);
				});
			} 
		}

		public ICommand DeleteAlarmCommand
		{
			get
			{
				return new Xamarin.Forms.Command((param) =>
				{
					Alarms.Remove((Alarm)param);
				});
			}

		}

		public AlarmListPageModel()
		{
		}

		public override void Init(object initData)
		{
			base.Init(initData);

			_alarmListType = (AlarmListType)initData;
		}

		protected override void ViewIsAppearing(object sender, EventArgs e)
		{
			base.ViewIsAppearing(sender, e);

			if (Alarms == null)
			{
				if (_alarmListType == AlarmListType.All)
					Alarms = new ObservableCollection<Alarm>(Defaults.AllAlarms.ToList());
				else
					Alarms = new ObservableCollection<Alarm>(Defaults.TodaysAlarms.ToList());
			}
		}

		private async void OpenPage(Alarm selectedAlarm)
		{
			await CoreMethods.PushPageModel<ViewAlarmPageModel>(selectedAlarm, false, true);
		}

		public override void ReverseInit(object returnedData)
		{
			base.ReverseInit(returnedData);

			if((bool)returnedData)
			{
				Alarms.Clear();

				if (_alarmListType == AlarmListType.All)
					Alarms = new ObservableCollection<Alarm>(Defaults.AllAlarms.ToList());
				else
					Alarms = new ObservableCollection<Alarm>(Defaults.TodaysAlarms.ToList());
			}
		}
	}
}
