using System;
namespace co.chimeralabs.ads.managed.Models
{
    public class AnalyticsWebWrapperDTO
    {
        public Action action { get; set; }
        public String dtoObj { get; set; }

        public AnalyticsWebWrapperDTO()
        {

        }
        public AnalyticsWebWrapperDTO(Action action, String dtoObj)
        {
            this.action = action;
            this.dtoObj = dtoObj;
        }

        public enum Action
        {
            AdInstanceVisibilityMetricUpdate,
            AdEvent,
            InstanceEvent
        }
    }
}
