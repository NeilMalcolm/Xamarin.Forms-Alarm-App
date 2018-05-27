using System;
namespace AlarmApp.Services
{
	public interface IDeviceStorageService
	{
		string GetFilePath(string fileName);
	}
}