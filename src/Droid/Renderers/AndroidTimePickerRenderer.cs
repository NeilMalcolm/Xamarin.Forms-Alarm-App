using System;
using System.ComponentModel;
using AlarmApp.Droid.Renderers;
using Android.App;
using Android.Content;
using Android.Support.V4.Content.Res;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using AGraphics = Android.Graphics;

[assembly: ExportRenderer(typeof(TimePicker), typeof(AndroidTimePickerRenderer))]
namespace AlarmApp.Droid.Renderers
{
	public class AndroidTimePickerRenderer : TimePickerRenderer, TimePickerDialog.IOnTimeSetListener, Android.Runtime.IJavaObject, IDisposable
	{
		AGraphics.Color _backgroundColor = new AGraphics.Color(255, 255, 255, 25);
		Android.App.TimePickerDialog _dialog = null;

		public AndroidTimePickerRenderer(Context context) : base(context)
		{
			
		}

		protected override void OnElementChanged(ElementChangedEventArgs<TimePicker> e)
		{
			base.OnElementChanged(e);

			if (Control == null) return;

			Control.SetCompoundDrawablesRelativeWithIntrinsicBounds(ResourcesCompat.GetDrawable(Resources, Resource.Drawable.clock, null), null, null, null);
			Control.CompoundDrawablePadding = 20;
			Control.TextSize = 65;
			Control.Background = ResourcesCompat.GetDrawable(Resources, Resource.Drawable.control_selector, null);
			Control.SetPaddingRelative(10, 10, 10, 10);
			Control.SetCursorVisible(false);
			//Set the time format
			Control.Text = Element.Time.ToString(@"hh\:mm");

			Control.Click += Control_Click;
			Control.FocusChange += Control_FocusChange;
		}


		protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			base.OnElementPropertyChanged(sender, e);

			if(e.PropertyName == "Time")
			{
				Control.Text = Element.Time.ToString(@"hh\:mm");
			}
		}

		/// <summary>
		/// Handle the control click
		/// </summary>
		/// <param name="sender">Object clicked</param>
		/// <param name="e">Click event</param>
		void Control_Click(object sender, EventArgs e)
		{
			ShowTimePicker();
		}

		/// <summary>
		/// If we focus on the element, show the time picker
		/// </summary>
		/// <param name="sender">Object which has had its focus change</param>
		/// <param name="e">Focuschange event</param>
        void Control_FocusChange(object sender, Android.Views.View.FocusChangeEventArgs e)
		{
			if (e.HasFocus)
				ShowTimePicker();
		}

		/// <summary>
		/// Shows our new 24 hour dialog
		/// </summary>
		void ShowTimePicker()
		{
			if (_dialog == null)
			{
				_dialog = new Android.App.TimePickerDialog(Context, this, Element.Time.Hours, Element.Time.Minutes, true);    //this bool is where we set time format, true = 24 hours
			}

			_dialog.Show();
		}

		/// <summary>
		/// Set the time of the TimePicker
		/// </summary>
		/// <param name="view">The Timepicker</param>
		/// <param name="hourOfDay">Hours</param>
		/// <param name="minute">Minutes</param>
		public void OnTimeSet(Android.Widget.TimePicker view, int hourOfDay, int minute)
		{
			var time = new TimeSpan(hourOfDay, minute, 0);
			//set our Xamarin Forms element time to our new time
			this.Element.SetValue(Xamarin.Forms.TimePicker.TimeProperty, time);

			//update our control's text to reflect the time
			this.Control.Text = time.ToString(@"hh\:mm");
		}
	}
}
