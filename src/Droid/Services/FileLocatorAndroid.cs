using System;
using System.Threading.Tasks;
using AlarmApp.Droid.Services;
using AlarmApp.Services;
using Android.App;
using Android.Content;
using Xamarin.Forms;

[assembly: Xamarin.Forms.Dependency(typeof(FileLocatorAndroid))]
namespace AlarmApp.Droid.Services
{
	public class FileLocatorAndroid : IFileLocator
	{
		MainActivity _mainActivity;
		static readonly int READ_REQUEST_CODE = 42;

		public void OpenFileLocator()
		{
			Intent intent = new Intent(Intent.ActionOpenDocument);

    		intent.AddCategory(Intent.CategoryOpenable);
   			intent.SetType("audio/*");

			var activity = (Activity)Forms.Context;
				activity.StartActivityForResult(intent, READ_REQUEST_CODE);

			_mainActivity = (MainActivity)activity;

			_mainActivity.FileChosen += OnFileChosen;
		}

		public event Action<Uri> FileChosen;

		void OnFileChosen(Uri uri)
		{
			FileChosen?.Invoke(uri);
			_mainActivity.FileChosen -= OnFileChosen;
		}
	}
}
