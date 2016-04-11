using System;

namespace co.chimeralabs.ads.managed.Models
{
    class InstanceParamsUpdateLog
    {
        public String userId { get; set; }
        public String adUnitId { get; set; }
        public String adServingId { get; set; }
        public String adInstanceId { get; set; }
        public long time { get; set; }
        public float cameraLookAtX { get; set; }
        public float cameraLookAtY { get; set; }
        public float cameraLookAtZ { get; set; }
        public float cameraX { get; set; }
        public float cameraY { get; set; }
        public float cameraZ { get; set; }
        public float adObjectX { get; set; }
        public float adObjectY { get; set; }
        public float adObjectZ { get; set; }

        public InstanceParamsUpdateLog()
        {

        }

        public InstanceParamsUpdateLog(String userId, String adUnitId, String adServingId, String adInstanceId, long time, float cameraLookAtX, float cameraLookAtY, float cameraLookAtZ, float cameraX, float cameraY, float cameraZ, float adObjectX, float adObjectY, float adObjectZ)
        {
            this.userId = userId;
            this.adUnitId = adUnitId;
            this.adServingId = adServingId;
            this.adInstanceId = adInstanceId;
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
