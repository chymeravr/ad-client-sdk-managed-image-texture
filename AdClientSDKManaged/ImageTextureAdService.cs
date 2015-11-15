using System;
using System.Net;
using Newtonsoft.Json;
using co.chimeralabs.ads.managed.Models;
using co.chimeralabs.ads.managed.Utils;

namespace co.chimeralabs.ads.managed
{
	public class ImageTextureAdService
	{
        private String adRequestURIString = "http://localhost:8080/virtualadserver";
        private Uri adRequestURI;
        private AdListener adListener;
		private String adId;
        private AdResponse adResponse;
        private byte[] imageData;
		public ImageTextureAdService (AdListener adListener, String adId)
		{
			this.adId = adId;
            this.adListener = adListener;
            adRequestURI = new Uri(adRequestURIString);
		}

		public void LoadAd(){
			Logger.Log (this, "LoadAd: Entered");
			//AdRequest adRequest = new AdRequest ();
			//adRequest.setAdType (AdType.ImageTexture);

            WebClient webClient = new WebClient();
            webClient.DownloadStringCompleted += new DownloadStringCompletedEventHandler (OnAdResponseReceived);
            //String response = webHandler.DownloadString(uri);
            //Console.WriteLine("response: {0}", response);
            try
            {
                webClient.DownloadStringAsync(adRequestURI);
            }
            catch(System.Net.WebException e){
                Console.WriteLine("Exception caught: {0}", e);
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine("Exception caught: {0}", e);
            }
		}

		private void OnAdResponseReceived(System.Object sender, DownloadStringCompletedEventArgs e){
			Logger.Log (this, "OnAdResponseReceived: Entered");
            if (e.Cancelled)
            {
                String errorMesage = "Request Cancelled.";
                AdLoadFailedArgs args = new AdLoadFailedArgs(errorMesage);
                adListener.OnAdLoadFailed(args);
                return;
            }
            if (e.Error != null)
            {
                String errorMessage = e.Error.Message;
                AdLoadFailedArgs args = new AdLoadFailedArgs(errorMessage);
                adListener.OnAdLoadFailed(args);
                return;
            }

            adResponse = JsonConvert.DeserializeObject<AdResponse>(e.Result);
            if (adResponse.resourceErrorCode != 0)
            {
                String errorMessage = "Ad can't be loaded";
                AdLoadFailedArgs args = new AdLoadFailedArgs(errorMessage);
                adListener.OnAdLoadFailed(args);
                return;
            }

            WebClient imageLoadWebClient = new WebClient();
            imageLoadWebClient.DownloadDataCompleted += OnImageDownloaded;
            //Uri imageUri = new Uri("https://fbcdn-sphotos-e-a.akamaihd.net/hphotos-ak-xaf1/v/t1.0-9/10245558_984698391554833_6578971494841583883_n.jpg?oh=822156489cc9e0efffac14f276ccd70a&oe=56B4DD1F&__gda__=1458580398_d9c77ffc5578a056933e7f13ca4b1d0e");
			Uri imageUri = new Uri ("http://images.earthcam.com/ec_metros/ourcams/fridays.jpg");
            try
            {
                imageLoadWebClient.DownloadDataAsync(imageUri);
            }
            catch (System.Net.WebException excp)
            {
                Console.WriteLine("Exception caught: {0}", excp);
            }
            catch (ArgumentNullException excp)
            {
                Console.WriteLine("Exception caught: {0}", excp);
            }
		}

        private void OnImageDownloaded(System.Object sender, DownloadDataCompletedEventArgs e)
        {
            Logger.Log(this, "OnImageDownloaded: Entered");
            if (e.Cancelled)
            {
                String errorMesage = "Image Load. Request Cancelled.";
                AdLoadFailedArgs args = new AdLoadFailedArgs(errorMesage);
                adListener.OnAdLoadFailed(args);
                return;
            }
            if (e.Error != null)
            {
                String errorMessage = e.Error.Message;
                AdLoadFailedArgs args = new AdLoadFailedArgs(errorMessage);
                adListener.OnAdLoadFailed(args);
                return;
            }
            imageData = e.Result;
            adListener.OnAdLoaded(this);
        }

        public byte[] getImageData()
        {
            return imageData;
        }

	}
}

