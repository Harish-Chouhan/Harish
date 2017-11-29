using System;
using HollywoodBowl.Services.Json;
using Newtonsoft.Json;


namespace HollywoodBowl.Services
{
    public class Piece
    {
        public string Name { get; set; }

        [JsonConverter(typeof(TimeSpanConverter))]
        public TimeSpan Duration { get; set; }

        public string Description { get; set; }
        public Artist Composer { get; set; }

        public Piece()
        {
        }
    }
}
