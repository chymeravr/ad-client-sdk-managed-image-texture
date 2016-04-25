using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using Newtonsoft.Json;
using co.chimeralabs.ads.managed.Models;
using co.chimeralabs.ads.managed.Utils;
using co.chimeralabs.ads.managed.Internal;
using co.chimeralabs.analytics.managed;

namespace co.chimeralabs.ads.managed
{
    public class ImageTextureAdUnit : InternalAdListener
    {
        //private String adRequestURIString = "http://chimeralabs.cloudapp.net/adserver/publisher/api/loadad";
        //private String adRequestURIString = "http://localhost:8080/adserver/publisher/api/loadad";
        private Uri adRequestURI;
        private AdUnitListener adUnitListener;
        private String adUnitId;
        private Hashtable instances = new Hashtable();

        private int nDistinctAds;
        private List<ImageTextureAd> ads;
        private List<AdErrorData> adErrors;

        private static Mutex adsMutex = new Mutex();
        private int adIndex = 0;

        public ImageTextureAdUnit(AdUnitListener adUnitListener, String adUnitId)
        {
            this.adUnitId = adUnitId;
            this.adUnitListener = adUnitListener;
            //adRequestURI = new Uri(adRequestURIString);
            adErrors = new List<AdErrorData>();
        }
        public String GetAdUnitId()
        {
            return this.adUnitId;
        }

        public void LoadAds(int nDistinctAds)
        {
            Logger.Log(this, "LoadAd: Entered");
            this.nDistinctAds = nDistinctAds;

            AdRequest adRequest = new AdRequest(AdType.IMAGE_TEXTURE, this.adUnitId, this.nDistinctAds, AdConfigurer.GetAppParams());

            WebClient webClient = new WebClient();
            webClient.Headers.Add("Content-Type", "application/json");
            webClient.UploadStringCompleted += new UploadStringCompletedEventHandler(OnAdResponseReceived);
            try
            {
                webClient.UploadStringAsync(new Uri(AdConfigurer.GetServerConfigedParams().imageTextureAdUnitUrl), "POST", JsonConvert.SerializeObject(adRequest));
            }
            catch (System.Net.WebException e)
            {
                Console.WriteLine("Exception caught: {0}", e);
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine("Exception caught: {0}", e);
            }
        }

        private void OnAdResponseReceived(System.Object sender, UploadStringCompletedEventArgs e)
        {
            Logger.Log(this, "OnAdResponseReceived: Entered");
            /**
             * Error Handling for Ad Response 
             **/
            if (e.Cancelled)
            {
                String errorMesage = "Request Cancelled.";
                AdUnitFailedArgs args = new AdUnitFailedArgs(AdUnitFailedArgs.ErrorCode.NO_AD_LOADED, errorMesage, this.adUnitId);
                adUnitListener.OnAdUnitLoadFailed(args);
                //AnalyticsWebManager.Push(new AdEventDTO(this.adUnitId, AdEventDTO.EventType.AdMetadataLoadFailed, errorMesage), AnalyticsWebWrapperDTO.Action.AdEvent, AnalyticsWebManager.PRIORITY.HIGH);
                return;
            }
            if (e.Error != null)
            {
                String errorMessage = e.Error.Message;
                AdUnitFailedArgs args = new AdUnitFailedArgs(AdUnitFailedArgs.ErrorCode.NO_AD_LOADED, errorMessage, this.adUnitId);
                adUnitListener.OnAdUnitLoadFailed(args);
                //AnalyticsWebManager.Push(new AdEventDTO(this.adUnitId, AdEventDTO.EventType.AdMetadataLoadFailed, errorMessage), AnalyticsWebWrapperDTO.Action.AdEvent, AnalyticsWebManager.PRIORITY.HIGH);
                return;
            }
            AdResponse adResponse = JsonConvert.DeserializeObject<AdResponse>(e.Result);
            if (adResponse.errorCode != 0)
            {
                String errorMessage = adResponse.errorMsg;
                AdUnitFailedArgs args = new AdUnitFailedArgs(AdUnitFailedArgs.ErrorCode.NO_AD_LOADED, errorMessage, this.adUnitId);
                adUnitListener.OnAdUnitLoadFailed(args);
                //AnalyticsWebManager.Push(new AdEventDTO(this.adUnitId, AdEventDTO.EventType.AdMetadataLoadFailed, errorMessage), AnalyticsWebWrapperDTO.Action.AdEvent, AnalyticsWebManager.PRIORITY.HIGH);
                return;
            }
            //AnalyticsWebManager.Push(new AdEventDTO(this.adUnitId, AdEventDTO.EventType.AdMetadataLoadSuccess, ""), AnalyticsWebWrapperDTO.Action.AdEvent, AnalyticsWebManager.PRIORITY.HIGH);

            /**
             * Analyzing individual ads in the response
             */
            ads = new List<ImageTextureAd>();
            for (int i = 0; i < adResponse.adResources.Count; i++)
            {
                AdResourceMetadata adMetadata = adResponse.adResources[i];
                if (adMetadata.errorCode != 0) //Some error occured in this particualr ad
                {
                    adsMutex.WaitOne();
                    adErrors.Add(new AdErrorData(adMetadata.errorCode, adMetadata.errorMsg));
                    adsMutex.ReleaseMutex();
                }
                else if (adMetadata.errorCode == 0) //No error for this ad
                {
                    ImageTextureAd ad = new ImageTextureAd(adMetadata.adServingId, this.adUnitId, adMetadata.diffuseTextureImageUrl, this);
                    ad.DownloadAd();
                }
            }
        }

        /*************************************************************************
         * 
         * Individual Ad error handling methods
         * 
         ************************************************************************/
        public void OnAdLoadFailed(InternalAdLoadFailedArgs args)
        {
            adsMutex.WaitOne();
            adErrors.Add(new AdErrorData(args.getErrorCode(), args.getErrorMessage()));
            int total = adErrors.Count + ads.Count;
            adsMutex.ReleaseMutex();
            if (total == nDistinctAds)
            {
                AllAdsResponded();
            }
        }
        public void OnAdLoaded(Object context)
        {
            ImageTextureAd ad = (ImageTextureAd)context;
            adsMutex.WaitOne();
            ads.Add(ad);
            int total = adErrors.Count + ads.Count;
            adsMutex.ReleaseMutex();
            if (total == nDistinctAds)
            {
                AllAdsResponded();
            }
        }
        private void AllAdsResponded()
        {
            if (ads.Count < nDistinctAds)
            {
                String errorMesage = "Only " + ads.Count + " ads could be loaded for adUnit " + this.adUnitId + ". Displaying these ads.";
                AdUnitFailedArgs args = new AdUnitFailedArgs(AdUnitFailedArgs.ErrorCode.FEW_ADS_LOADED, errorMesage, this.adUnitId);
                adUnitListener.OnAdUnitLoadFailed(args);
                if (ads.Count == 0)
                {
                    return;
                }
            }
            adUnitListener.OnAdUnitLoaded(this); //Notify that all ads have been loaded
        }

        /*************************************************************************
         * 
         * Instnance handling methods
         * 
         ************************************************************************/
        public ImageTextureAdInstance CreateInstance(String instanceId, IAdObject adObject)
        {
            if (ads.Count == 0)
                return null;
            ImageTextureAdInstance instance = new ImageTextureAdInstance(instanceId, ads[adIndex], adObject);
            instances.Add(instance.GetInstanceId(), instance);
            adIndex = (adIndex + 1) % ads.Count;
            AppParams param = AdConfigurer.GetAppParams();
            AnalyticsManager.Push(new AdServedLog(param.userId, this.adUnitId, instance.GetAdServingId(), instanceId), AnalyticsManager.TYPE.AD_DISPLAYED, AnalyticsManager.PRIORITY.MEDIUM);
            return instance;
        }

        public Hashtable GetInstances()
        {
            return instances;
        }

        public ImageTextureAdInstance GetInstance(String instanceId)
        {
            return (ImageTextureAdInstance)instances[instanceId];
        }

        private class AdErrorData
        {
            public int errorCode { get; set; }
            public String errorMessage { get; set; }
            public AdErrorData(int errorCode, String errorMessage)
            {
                this.errorCode = errorCode;
                this.errorMessage = errorMessage;
            }
        }
    }
}
