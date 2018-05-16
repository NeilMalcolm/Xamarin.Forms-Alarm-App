using System;
using AlarmApp.PageModels;
using FreshMvvm;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace AlarmApp
{
	public partial class App : Application
	{
		public App()
		{
			InitializeComponent();

			var tabbedNavigation = new FreshTabbedFONavigationContainer("Alarm App");
			tabbedNavigation.BackgroundColor = (Color)Resources["PrimaryColor"];

			tabbedNavigation.AddTab<AlarmListPageModel>("Today's Alarms", null, AlarmListType.Today);
			tabbedNavigation.AddTab<AlarmListPageModel>("All Alarms", null, AlarmListType.All);

			MainPage = tabbedNavigation;


			//testing
			//MainPage = new TestPage();
		}

		//public App(TimeSpan time) : this()
		//{
		//	InitializeComponent();
		//	MainPage.Navigation.PushAsync(new AlarmAppPage(time));
		//}

		public void DoThing()
		{
			
		}

		protected override void OnStart()
		{
			// Handle when your app starts
		}

		protected override void OnSleep()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume()
		{
			// Handle when your app resumes
		}
	}
}
