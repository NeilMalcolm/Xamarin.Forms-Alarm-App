using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace AlarmApp.Pages
{
	public partial class SettingsTonePage : ContentPage
	{
		public SettingsTonePage()
		{
			InitializeComponent();
		}

		protected override void OnAppearing()
		{
			ToneListView.ItemSelected += ToneListView_ItemSelected;
			base.OnAppearing();
		}

		protected override void OnDisappearing()
		{
			ToneListView.ItemSelected -= ToneListView_ItemSelected;
			base.OnDisappearing();
		}

		void ToneListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
		{
			ToneListView.SelectedItem = null;
		}
	}
}
