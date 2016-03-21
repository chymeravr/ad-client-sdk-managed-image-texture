using System;
using co.chimeralabs.ads.managed;
namespace AdClientSDKManagedTestConsole
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			Console.WriteLine ("Hello World!");
            /*AdListenerImplementation adListenerImplementation = new AdListenerImplementation();
			ImageTextureAd adClient = new ImageTextureAd (adListenerImplementation, "asdfadfd");
			adClient.LoadAds (2);
            //AnalyticsWebManagerTest.Test1();*/
            PlatformObjectsFactoryHolder.SetFactory(new PlatformObjectsFactoryUnity());
            AdListenerImplementation adListener = new AdListenerImplementation();
            ImageTextureAdUnit adUnit = new ImageTextureAdUnit(adListener, "adsfd");
            adUnit.LoadAds(2);
            //AnalyticsWebManagerTest.Test1();
            Console.ReadLine();
		}
	}

    public class AdListenerImplementation : AdUnitListener
    {
        public void OnAdUnitLoadFailed(AdUnitFailedArgs args)
        {
			Console.WriteLine ("Ad Load Failed " + args.errorMessage);
        }
        public void OnAdUnitLoaded(Object context)
        {
            ImageTextureAdUnit adUnit = (ImageTextureAdUnit)context;
            ImageTextureAdInstance instance1 = adUnit.CreateInstance("adds", new AdObjectUnity());
            ImageTextureAdInstance instance2 = adUnit.CreateInstance("sdfdf", new AdObjectUnity());
            TextureUnity texture1 = (TextureUnity) instance1.GetDiffuseTexture();
            TextureUnity texture2 = (TextureUnity) instance2.GetDiffuseTexture();
            byte[] data1 = texture1.GetTextureData();
            byte[] data2 = texture2.GetTextureData();
            if(texture1!=null)
                Console.WriteLine("Non null first instance");
            if (texture2 != null)
                Console.WriteLine("Non null second instanc");
			Console.WriteLine ("adLoadFinished");
        }
    }
    public class PlatformObjectsFactoryUnity : IPlatformObjectsFactory
    {
        public ITexture GetTextureObject()
        {
            return new TextureUnity();
        }
    }

    public class TextureUnity : ITexture
    {
        private byte[] testArray;
        public Boolean CreateTextureObject(byte[] textureData)
        {
            testArray = textureData;
            return true;
        }
        public byte[] GetTextureData()
        {
            return testArray;
        }
    }
    public class AdObjectUnity : IAdObject
    {

        public void Update()
        {

        }
        public float AdObjectX()
        {
            return 0.0f;
        }
        public float AdObjectY()
        {
            return 0.0f;
        }
        public float AdObjectZ()
        {
            return 0.0f;
        }
        public float CameraX()
        {
            return 0.0f;
        }
        public float CameraY()
        {
            return 0.0f;
        }
        public float CameraZ()
        {
            return 0.0f;
        }
        public float CameraLA_X()
        {
            return 0.0f;
        }
        public float CameraLA_Y()
        {
            return 0.0f;
        }
        public float CameraLA_Z()
        {
            return 0.0f;
        }
    }
}
