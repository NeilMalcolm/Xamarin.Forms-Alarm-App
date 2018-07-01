using System;
using System.Collections.Generic;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using AlarmApp.Models;

using Xamarin.Forms;

namespace AlarmApp.Popups
{
	public partial class AlarmToneNamingPopupPage : PopupPage
	{
		AlarmTone _toneToEdit;
		bool _isEditMode;
		public event Action<string> ToneNameSet;

		public AlarmToneNamingPopupPage()
		{
			InitializeComponent();
		}

		public AlarmToneNamingPopupPage(AlarmTone tone)
		{
			InitializeComponent();
			_toneToEdit = tone;
			ToneNameEntry.Text = tone.Name;
			_isEditMode = true;
		}

		void SetNameButtonPressed(object sender, System.EventArgs e)
		{
			if(_isEditMode)
			{
				var realm = Realms.Realm.GetInstance();
				realm.Write(() =>
				{
					_toneToEdit.Name = ToneNameEntry.Text;
				});
			}
			else {
				ToneNameSet?.Invoke(ToneNameEntry.Text);
			}

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
