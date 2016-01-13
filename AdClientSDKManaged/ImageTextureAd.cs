using System;
using System.Collections;
using System.Net;
using Newtonsoft.Json;
using co.chimeralabs.ads.managed.Models;
using co.chimeralabs.ads.managed.Utils;

namespace co.chimeralabs.ads.managed
{
	public class ImageTextureAd
	{
        private String adRequestURIString = "http://chimeralabs.cloudapp.net/adserver/publisher/api/loadad";
        private Uri adRequestURI;
        private AdListener adListener;
		private String adId;
        private AdResponse adResponse;
        private byte[] imageData;
		private Hashtable instances = new Hashtable();

		public ImageTextureAd (AdListener adListener, String adId)
		{
			this.adId = adId;
            this.adListener = adListener;
            adRequestURI = new Uri(adRequestURIString);
		}

		public AdInstance CreateInstance(String instanceId){
            AdInstance instance = new AdInstance(instanceId, this.adId);
			instances.Add (instance.GetInstanceId(), instance);
            return instance;
		}

		public Hashtable GetInstancesMap(){
			return instances;
		}

		public AdInstance GetInstance(String instanceId){
			return (AdInstance) instances [instanceId];
		}

		public void LoadAd(){
			Logger.Log (this, "LoadAd: Entered");

            WebClient webClient = new WebClient();
            webClient.DownloadStringCompleted += new DownloadStringCompletedEventHandler (OnAdResponseReceived);
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
                AnalyticsWebManager.Push(new AdEventDTO(this.adId, AdEventDTO.EventType.AdMetadataLoadFailed, errorMesage), AnalyticsWebWrapperDTO.Action.AdEvent, AnalyticsWebManager.PRIORITY.HIGH);
                return;
            }
            if (e.Error != null)
            {
                String errorMessage = e.Error.Message;
                AdLoadFailedArgs args = new AdLoadFailedArgs(errorMessage);
                adListener.OnAdLoadFailed(args);
                AnalyticsWebManager.Push(new AdEventDTO(this.adId, AdEventDTO.EventType.AdMetadataLoadFailed, errorMessage), AnalyticsWebWrapperDTO.Action.AdEvent, AnalyticsWebManager.PRIORITY.HIGH);
                return;
            }

            adResponse = JsonConvert.DeserializeObject<AdResponse>(e.Result);
            if (adResponse.resourceErrorCode != 0)
            {
                String errorMessage = "Ad can't be loaded";
                AdLoadFailedArgs args = new AdLoadFailedArgs(errorMessage);
                adListener.OnAdLoadFailed(args);
                AnalyticsWebManager.Push(new AdEventDTO(this.adId, AdEventDTO.EventType.AdMetadataLoadFailed, errorMessage), AnalyticsWebWrapperDTO.Action.AdEvent, AnalyticsWebManager.PRIORITY.HIGH);
                return;
            }
            AnalyticsWebManager.Push(new AdEventDTO(this.adId, AdEventDTO.EventType.AdMetadataLoadSuccess, ""), AnalyticsWebWrapperDTO.Action.AdEvent, AnalyticsWebManager.PRIORITY.HIGH);

            WebClient imageLoadWebClient = new WebClient();
            imageLoadWebClient.DownloadDataCompleted += OnImageDownloaded;
            //Uri imageUri = new Uri("https://fbcdn-sphotos-e-a.akamaihd.net/hphotos-ak-xaf1/v/t1.0-9/10245558_984698391554833_6578971494841583883_n.jpg?oh=822156489cc9e0efffac14f276ccd70a&oe=56B4DD1F&__gda__=1458580398_d9c77ffc5578a056933e7f13ca4b1d0e");
			//Uri imageUri = new Uri ("http://images.earthcam.com/ec_metros/ourcams/fridays.jpg");
			Uri imageUri = new Uri (adResponse.resourceURL);
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
                AnalyticsWebManager.Push(new AdEventDTO(this.adId, AdEventDTO.EventType.AdResourceDownloadFailed, errorMesage), AnalyticsWebWrapperDTO.Action.AdEvent, AnalyticsWebManager.PRIORITY.HIGH);
                return;
            }
            if (e.Error != null)
            {
                String errorMessage = e.Error.Message;
                AdLoadFailedArgs args = new AdLoadFailedArgs(errorMessage);
                adListener.OnAdLoadFailed(args);
                AnalyticsWebManager.Push(new AdEventDTO(this.adId, AdEventDTO.EventType.AdResourceDownloadFailed, errorMessage), AnalyticsWebWrapperDTO.Action.AdEvent, AnalyticsWebManager.PRIORITY.HIGH);
                return;
            }
            imageData = e.Result;
            adListener.OnAdLoaded(this);
            AnalyticsWebManager.Push(new AdEventDTO(this.adId, AdEventDTO.EventType.AdResourceDownloadSuccess, ""), AnalyticsWebWrapperDTO.Action.AdEvent, AnalyticsWebManager.PRIORITY.HIGH);
        }

        public byte[] getImageData()
        {
            return imageData;
        }

	}
}
