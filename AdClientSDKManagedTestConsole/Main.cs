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
			ImageTextureAdService adClient = new ImageTextureAdService (adListenerImplementation, "asdfadfd");
			adClient.LoadAd ();
            Console.ReadLine();
		}
	}

    public class AdListenerImplementation : AdListener
    {
        public void OnAdLoadFailed(AdLoadFailedArgs args)
        {
            String test = "test";
        }
        public void OnAdLoaded(Object context)
        {
            ImageTextureAdService client = (ImageTextureAdService)context;
            String test = "test";
            byte[] data = client.getImageData();
        }
    }
}
