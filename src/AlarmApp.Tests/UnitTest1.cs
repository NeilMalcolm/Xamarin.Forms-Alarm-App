using System;
using AlarmApp.Models;
using Xunit;
using Xunit.Abstractions;

namespace AlarmApp.Tests
{
	public class UnitTest1
	{
		
	    private readonly ITestOutputHelper _output;

		public UnitTest1(ITestOutputHelper output)
	    {
			_output = output;
	    }

		//[Theory]
		//[InlineData(2, 0)]
		//[InlineData(0, 20)]
		//[InlineData(0, 15)]
		//[InlineData(0, 15)]
		//[InlineData(36, 1)]
		//[InlineData(1, 1)]
		//[InlineData(1, 0)]
		//[InlineData(25, 0)]
		//[InlineData(0, 0)]
		//[InlineData(1, 150)]
		//[InlineData(0, 2000)]
		//[InlineData(2000, 2000)]
		//[InlineData(int.MaxValue, int.MaxValue)]
		//[InlineData(int.MinValue, int.MinValue)]
		//public void GetFrequencyString_CheckFriendlyStringIsCorrectFormat(int hours, int minutes)
		//{
		//	var frequency = new TimeSpan(hours, minutes, 0);
		//	var alarm = new Alarm()
		//	{
		//		Time = new TimeSpan(10, 30, 0),
		//		Frequency = frequency,
		//		IsActive = true
		//	};

		//	_output.WriteLine("thing: " + alarm.UserFriendlyFrequency);
		//	Assert.True(true);
		//}

	}
}
