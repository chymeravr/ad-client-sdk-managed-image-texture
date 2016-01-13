using System;
using System.Collections;

namespace co.chimeralabs.ads.managed.Models
{
    public class AdEventDTO
    {
        public String adId { get; set; }
        public EventType eventType { get; set; }
        public String eventMessage { get; set; }

        public AdEventDTO()
        {

        }

        public AdEventDTO(String adId, EventType eventType, String eventMessage)
        {
            this.adId = adId;
            this.eventType = eventType;
            this.eventMessage = eventMessage;
        }

        public enum EventType
        {
            AdMetadataLoadFailed,
            AdMetadataLoadSuccess,
            AdResourceDownloadFailed,
            AdResourceDownloadSuccess
        }
    }
}
