//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34209
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using co.chimeralabs.ads.managed.Models;

namespace co.chimeralabs.ads.managed
{
	public class AdInstance
	{
		private String instanceId;
		private long startTime;
		private long lastTime;
        private String adUnitId;
        private String adServingId;
		private Boolean isInstancedDisplayed = false;
        private IAdObject ao;
		public AdInstance (String instanceId, String adUnitId, String adServingId, IAdObject adObject)
		{
            this.ao = adObject;
			this.instanceId = instanceId;
            this.adUnitId = adUnitId;
            this.adServingId = adServingId;
			startTime = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
			lastTime = 0;
            //AnalyticsWebManager.Push(new InstanceEventDTO(adData.adUnitId, this.instanceId, InstanceEventDTO.EventType.AdAttachedToInstance, ""), AnalyticsWebWrapperDTO.Action.InstanceEvent, AnalyticsWebManager.PRIORITY.HIGH);
		}
		public String GetInstanceId(){
			return this.instanceId;
		}
        public String GetAdUnitId()
        {
            return this.adUnitId;
        }
		public void UpdateVisibilityMetric(){
			long newTime = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond - startTime;
			if ((newTime - lastTime) > AnalyticsWebManager.timeResolution) {
				lastTime = newTime;
                ao.Update();
                InstanceVisibilityMetricDTO dtoObj = new InstanceVisibilityMetricDTO(adUnitId, this.instanceId, newTime, ao.CameraLA_X(), ao.CameraLA_Y(), ao.CameraLA_Z(), ao.CameraX(), ao.CameraY(), ao.CameraZ(), ao.AdObjectX(), ao.AdObjectY(), ao.AdObjectZ());
                //AnalyticsWebManager.Push(dtoObj, AnalyticsWebWrapperDTO.Action.AdInstanceVisibilityMetricUpdate, AnalyticsWebManager.PRIORITY.LOW);
			}
		}
		public void MarkInstanceDisplayedTrue(){
			isInstancedDisplayed = true;
			//AnalyticsWebManager.Push(new InstanceEventDTO(adData.adUnitId, this.instanceId, InstanceEventDTO.EventType.InstanceDisplayed, ""), AnalyticsWebWrapperDTO.Action.InstanceEvent, AnalyticsWebManager.PRIORITY.HIGH);
		}
		public Boolean IsInstanceDisplayed(){
			return this.isInstancedDisplayed;
		}
	}
}