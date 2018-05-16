using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using AlarmApp.Models;
using FreshMvvm;

namespace AlarmApp.PageModels
{
	public class AlarmListPageModel : FreshBasePageModel
	{
		AlarmListType _alarmListType;

		List<Alarm> _alarms;
		private Alarm _selectedAlarm;

		public List<Alarm> Alarms
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

			if (_alarmListType == AlarmListType.All)
				Alarms = Defaults.AllAlarms.ToList();
			else
				Alarms = Defaults.TodaysAlarms.ToList();

		}

		private async void OpenPage(Alarm selectedAlarm)
		{
			await CoreMethods.PushPageModel<ViewAlarmPageModel>(selectedAlarm, false, true);
		}
	}
}
