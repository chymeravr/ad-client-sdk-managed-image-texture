using System;
using co.chimeralabs.ads.managed;
namespace AdClientSDKManagedTestConsole
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			Console.WriteLine ("Hello World!");
            AdListenerImplementation adListenerImplementation = new AdListenerImplementation();
			ImageTextureAd adClient = new ImageTextureAd (adListenerImplementation, "asdfadfd");
			adClient.LoadAd ();
            AnalyticsWebManagerTest.Test1();
            Console.ReadLine();
		}
	}

    public class AdListenerImplementation : AdListener
    {
        public void OnAdLoadFailed(AdLoadFailedArgs args)
        {
			Console.WriteLine ("Ad Load Failed %s", args.getErrorMessage ());
        }
        public void OnAdLoaded(Object context)
        {
            ImageTextureAd client = (ImageTextureAd)context;
            String test = "test";
            byte[] data = client.getImageData();
			Console.WriteLine ("adLoadFinished");
        }
    }
}
