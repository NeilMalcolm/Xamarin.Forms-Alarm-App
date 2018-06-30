using System;
using System.Collections.Generic;
using Xamarin.Forms;
using AlarmApp.Models;

namespace AlarmApp.Views
{
	public partial class ToneViewCell : ViewCell
	{
		public ToneViewCell()
		{
			InitializeComponent();
		}

		void Handle_BindingContextChanged(object sender, System.EventArgs e)
		{
			base.OnBindingContextChanged();

		}
	}
}
