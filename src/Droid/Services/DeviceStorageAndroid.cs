using System;
using System.IO;
using AlarmApp.Droid.Services;
using AlarmApp.Services;

[assembly: Xamarin.Forms.Dependency(typeof(DeviceStorageAndroid))]
namespace AlarmApp.Droid.Services
{
	public class DeviceStorageAndroid : IDeviceStorageService
	{
		public string GetFilePath(string fileName)
		{
			return Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments), fileName);
		}
	}
}
