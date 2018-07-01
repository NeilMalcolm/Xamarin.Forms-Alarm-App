using System;
using AlarmApp.Models;
using Xamarin.Forms;

namespace AlarmApp.DataTemplateSelectors
{
	public class AlarmToneDatatemplateSelector : DataTemplateSelector
	{
		public DataTemplate DefaultTemplate { get; set; }
		public DataTemplate CustomTemplate { get; set; }

		protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
		{
			var alarmTone = (AlarmTone)item;
			return alarmTone.IsCustomTone ? CustomTemplate : DefaultTemplate; 
		}
	}
}
