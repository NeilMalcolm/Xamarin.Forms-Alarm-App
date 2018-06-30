using System;
using System.Collections.Generic;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;

using Xamarin.Forms;

namespace AlarmApp.Popups
{
	public partial class AlarmToneNamingPopupPage : PopupPage
	{
		public event Action<string> ToneNameSet;

		public AlarmToneNamingPopupPage()
		{
			InitializeComponent();
		}

		void SetNameButtonPressed(object sender, System.EventArgs e)
		{
			ToneNameSet?.Invoke(ToneNameEntry.Text);

			PopupNavigation.Instance.PopAsync();
		}

		protected override bool OnBackButtonPressed()
		{
			ToneNameSet?.Invoke("New Tone: " + DateTime.Now.ToString(@"dd MMM yy - hh\:mm"));
			PopupNavigation.Instance.PopAsync();
			return true;
		}

		protected override bool OnBackgroundClicked()
		{
			ToneNameSet?.Invoke("New Tone: " + DateTime.Now.ToString(@"dd MMM yy - hh\:mm"));
			return base.OnBackgroundClicked();
		}
	}
}
