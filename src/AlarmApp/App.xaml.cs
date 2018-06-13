using System;
using AlarmApp.PageModels;
using FreshMvvm;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using AlarmApp.Services;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace AlarmApp
{
	public partial class App : Application
	{
		public App()
		{
			InitializeComponent();

			SetUpIoC();
			/*
			 * TabbedPage page 
			 */
			var tabbedNavigation = new FreshTabbedFONavigationContainer("Alarm App");
			tabbedNavigation.BackgroundColor = (Color)Resources["PrimaryColor"];

			tabbedNavigation.AddTab<AlarmListPageModel>("Today's Alarms", null, AlarmListType.Today);
			tabbedNavigation.AddTab<AlarmListPageModel>("All Alarms", null, AlarmListType.All);
			MainPage = tabbedNavigation;
			/*
			 * Single page
			 */

			//var page = FreshPageModelResolver.ResolvePageModel<AlarmListPageModel>();
			//var nav = new FreshNavigationContainer(page);
			//nav.BackgroundColor = (Color)Resources["PrimaryColor"];
			//page.Title = "My Alarms";

			//MainPage = nav;


			//testing
			//MainPage = new TestPage();
		}

		void SetUpIoC()
		{
			FreshIOC.Container.Register<IAlarmStorageService, AlarmStorageService>();
		}

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
