using System;
using System.Collections.Generic;
using Xamarin.Forms;
using AlarmApp.Models;
using System.Runtime.CompilerServices;

namespace AlarmApp.Views
{
	public partial class DaysOfWeekSelectionView : ContentView
	{
		public static readonly BindableProperty DaysProperty = BindableProperty.Create("Days", typeof(DaysOfWeek), typeof(DaysOfWeekSelectionView), new DaysOfWeek());

		public DaysOfWeek Days
		{
			get { return (DaysOfWeek)GetValue(DaysProperty); }
			set { SetValue(DaysProperty, value); }
		}

		public DaysOfWeekSelectionView()
		{
			InitializeComponent();
		}

		protected override void OnBindingContextChanged()
		{
			base.OnBindingContextChanged();
		}

	}
}
