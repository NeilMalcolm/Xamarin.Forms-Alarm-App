using Xamarin.Forms;
using System;

namespace AlarmApp
{
	public partial class AlarmAppPage : ContentPage
	{
		public AlarmAppPage()
		{
			InitializeComponent();
		}
		public AlarmAppPage(TimeSpan time) : this()
		{
			TimeLabel.Text = time.Hours + ":" + time.Minutes;
		}
	}
}
