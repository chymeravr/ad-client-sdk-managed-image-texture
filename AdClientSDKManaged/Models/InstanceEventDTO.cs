using System;

namespace co.chimeralabs.ads.managed.Models
{
    class InstanceEventDTO
    {
        public String adId { get; set; }
        public String instanceId { get; set; }
        public EventType eventType { get; set; }
        public String eventMessage { get; set; }

        public InstanceEventDTO()
        {

        }

        public InstanceEventDTO(String adId, String instanceId, EventType eventType, String eventMessage)
        {
            this.adId = adId;
            this.instanceId = instanceId;
            this.eventType = eventType;
            this.eventMessage = eventMessage;
        }

        public enum EventType
        {
            AdAttachedToInstance,
			InstanceDisplayed
        }
    }
}
