using System;
using System.Collections;

namespace co.chimeralabs.ads.managed.Models
{
    public class AdServedLog
    {
        public String userId { get; set; }
        public String adUnitId { get; set; }
        public String adServingId { get; set; }
        public String adInstanceId { get; set; }
        
        public AdServedLog()
        {
        }

        public AdServedLog(String userId, String adUnitId, String adServingId, String adInstanceId)
        {
            this.userId = userId;
            this.adUnitId = adUnitId;
            this.adServingId = adServingId;
            this.adInstanceId = adInstanceId;
        }
    }
}
