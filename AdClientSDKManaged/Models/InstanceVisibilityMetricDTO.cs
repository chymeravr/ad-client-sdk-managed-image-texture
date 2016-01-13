using System;
namespace co.chimeralabs.ads.managed.Models
{
    public class InstanceVisibilityMetricDTO
    {
        public String adId { get; set; }
        public String instanceId { get; set; }
		public long time { get; set; }
        public float cameraLookAtX {get; set;}
        public float cameraLookAtY {get; set;}
        public float cameraLookAtZ {get; set;}
        public float cameraX {get; set;}
        public float cameraY {get; set;}
        public float cameraZ {get; set;}
        public float adObjectX {get; set;}
        public float adObjectY {get; set;}
        public float adObjectZ { get; set;}

        public InstanceVisibilityMetricDTO()
        {

        }

        public InstanceVisibilityMetricDTO(String adId, String instanceId, long time, float cameraLookAtX, float cameraLookAtY, float cameraLookAtZ, float cameraX, float cameraY, float cameraZ, float adObjectX, float adObjectY, float adObjectZ)
        {
            this.adId = adId;
            this.instanceId = instanceId;
			this.time = time;
            this.cameraLookAtX = cameraLookAtX;
            this.cameraLookAtY = cameraLookAtY;
            this.cameraLookAtZ = cameraLookAtZ;
            this.cameraX = cameraX;
            this.cameraY = cameraY;
            this.cameraZ = cameraZ;
            this.adObjectX = adObjectX;
            this.adObjectY = adObjectY;
            this.adObjectZ = adObjectZ;
        }
    }
}
