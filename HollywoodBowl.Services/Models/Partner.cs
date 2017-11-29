using System;
using Newtonsoft.Json;
using HollywoodBowl.Services.Json;

namespace HollywoodBowl.Services
{

    public enum PartnerType{
        Partner,
        Sponsor,
        Media,
        Unknown
    }

    public class Partner
    {
        [JsonConverter(typeof(PartnerTypeConverter))]
        public PartnerType Relationship { get; set; }

        public string Name { get; set; }
        public string Tagline { get; set; }
        public string Website { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }

        public Partner()
        {
        }
    }
}
