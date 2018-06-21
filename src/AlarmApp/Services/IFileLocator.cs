using System;
namespace AlarmApp.Services
{
	public interface IFileLocator
	{
		void OpenFileLocator();

		event Action<Uri> FileChosen;
	}
}
