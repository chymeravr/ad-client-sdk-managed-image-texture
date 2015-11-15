using System;
using NUnit.Framework;
using co.chimeralabs.ads.managed.Utils;

namespace co.chimeralabs.ads.managed.test.Utils
{
	[TestFixture]
	public class LoggerTest
	{
		[Test]
		public void LogTest(){
			Logger.Open ("test.txt");
			Logger.Log (this, "hey there");
			Logger.Close ();
		}
	}
}

