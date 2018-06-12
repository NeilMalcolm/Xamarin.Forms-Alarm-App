using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using AlarmApp.Models;
using FreshMvvm;
using AlarmApp.Helpers;
using AlarmApp.Services;
using PropertyChanged;

namespace AlarmApp.PageModels
{
	[AddINotifyPropertyChangedInterface]
	public class AlarmListPageModel : FreshBasePageModel
	{
		AlarmListType _alarmListType;
		AlarmStorageService _alarmStorage = new AlarmStorageService();
		Alarm _selectedAlarm;

		public ObservableCollection<Alarm> Alarms { get; set; } = new ObservableCollection<Alarm>();


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
					//Defaults.AllAlarms.Remove((Alarm)param);
					//Alarms.Clear();
					//CreateLists();
					var alarm = (Alarm)param;
					Alarms.Remove(alarm);
					_alarmStorage.DeleteAlarm(alarm);
				});
			}

		}

		public AlarmListPageModel()
		{
		}

		public override void Init(object initData)
		{
			base.Init(initData);

			if (initData != null)
				_alarmListType = (AlarmListType)initData;
		}

		protected override void ViewIsAppearing(object sender, EventArgs e)
		{
			base.ViewIsAppearing(sender, e);

			CreateLists();
		}

		async void OpenPage(Alarm selectedAlarm)
		{
			await CoreMethods.PushPageModel<ViewAlarmPageModel>(selectedAlarm, false, true);
		}

		public override void ReverseInit(object returnedData)
		{
			base.ReverseInit(returnedData);

			if((bool)returnedData)
			{
				Alarms.Clear();
				CreateLists();
			}
		}

		void CreateLists()
		{
			if (_alarmListType == AlarmListType.Today)
				//Alarms = new ObservableCollection<Alarm>(Defaults.AllAlarms.Where(x => x.OccursToday == true).ToList());
				Alarms = new ObservableCollection<Alarm>(_alarmStorage.GetTodaysAlarms());
			else
				//Alarms = new ObservableCollection<Alarm>(Defaults.AllAlarms);
				Alarms = new ObservableCollection<Alarm>(_alarmStorage.GetAllAlarms());

			//var todayGroup = new AlarmListGroup(Defaults.TodaysAlarms.Where(x => x.OccursToday == true).ToList());
			//todayGroup.Title = "Today";

			//var allGroup = new AlarmListGroup(Defaults.TodaysAlarms.Where(x => x.OccursToday == false).ToList());
			//allGroup.Title = "All";
			//System.Diagnostics.Debug.WriteLine("TODAY: " + todayGroup.Count + ", ALL: " + allGroup.Count);
			//System.Diagnostics.Debug.WriteLine(Defaults.TodaysAlarms.Where(x => x.OccursToday == true).ToList().Count);
			//Alarms = new List<AlarmListGroup>()
			//{
			//	todayGroup, allGroup
			//};

		}
	}
}
