using System;
using System.IO;
using AlarmApp.iOS.Services;
using AlarmApp.Services;

[assembly: Xamarin.Forms.Dependency(typeof(DeviceStorageIos))]
namespace AlarmApp.iOS.Services
{
	public class DeviceStorageIos : IDeviceStorageService
	{
		public string GetFilePath(string fileName)
		{
			return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "..", "Library", fileName);
		}
	}
}
