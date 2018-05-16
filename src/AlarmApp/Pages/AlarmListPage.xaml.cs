using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace AlarmApp.Pages
{
	public partial class AlarmListPage : ContentPage
	{
		public AlarmListPage()
		{
			InitializeComponent();
		}

		void Handle_ItemSelected(object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
		{
			((ListView)sender).SelectedItem = null;
		}
	}
}
