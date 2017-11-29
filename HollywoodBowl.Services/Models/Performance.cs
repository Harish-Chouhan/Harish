using System;
using Newtonsoft.Json;
using HollywoodBowl.Services.Json;



namespace HollywoodBowl.Services
{
    public class Performance
    {
        
        public DateTimeOffset StartTime { get; set; }
        public DateTimeOffset PresaleTime { get; set; }
        public DateTimeOffset SaleTime { get; set; }
        public DateTimeOffset GateTime { get; set; }

        //[JsonProperty("preperformance_talk_start_time")]
        [JsonConverter(typeof(TimeOnlyConverter))]
        public DateTime PreperformanceTalkStartTime { get; set; }

        public Series Series { get; set; }

        public Performance()
        {
        }
    }
}
