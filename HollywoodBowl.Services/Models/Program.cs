using System.Collections.Generic;
using Newtonsoft.Json;


namespace HollywoodBowl.Services
{
    public class Program : IIdentifiable
    {
        public int Id { get; set; }

        [JsonProperty("lease_event")]
        public bool IsLeaseEvent { get; set; }

        public string Name { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public Artist PreperformanceSpeaker { get; set; }
        public Season Season { get; set; }
        public Venue Venue { get; set; }
        public List<Performance> Performances {get; set;}


        public Program()
        {
        }
    }
}
