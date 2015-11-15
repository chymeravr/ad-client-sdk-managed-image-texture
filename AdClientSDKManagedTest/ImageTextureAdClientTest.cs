using System;
using NUnit.Framework;
using co.chimeralabs.ads.managed;
using co.chimeralabs.ads.managed.Utils;
namespace co.chimeralabs.ads.managed.test
{
	[TestFixture]
	public class ImageTextureAdClientTest
	{
		[Test]
		public void LoadAdTest ()
		{
			Logger.Open ("test.txt");
			ImageTextureAdClient adClient = new ImageTextureAdClient ("asdfadfd");
			adClient.LoadAd ();
			System.Timers.Timer timer = new System.Timers.Timer (3000);
			timer.Start ();
			Logger.Close ();
		}
	}
}
