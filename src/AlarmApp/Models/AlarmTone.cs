using System;
using Realms;
using PropertyChanged;

namespace AlarmApp.Models
{
	[AddINotifyPropertyChangedInterface]
	public class AlarmTone : RealmObject
	{
		public string Name { get; set; }
		public string Path { get; set; }

		public AlarmTone(){}

		public AlarmTone(string name, string path)
		{
			Name = name;
			Path = path;
		}
	}
}
