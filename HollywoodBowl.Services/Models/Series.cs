using System;
using Newtonsoft.Json;


namespace HollywoodBowl.Services
{
    public class Series
    {
        public string Name { get; set; }

        [JsonProperty("shortcode")]
        public string ShortCode { get; set; }

        public Series()
        {
        }
    }
}
