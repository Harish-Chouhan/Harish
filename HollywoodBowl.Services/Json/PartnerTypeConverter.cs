using System;
using Newtonsoft.Json;


namespace HollywoodBowl.Services.Json
{
    public class PartnerTypeConverter : JsonConverter
    {

        public PartnerTypeConverter(params Type[] types)
        {
        }

        public PartnerTypeConverter()
        {
        }

        public override bool CanConvert(Type objectType)
        {
            return true;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType != JsonToken.String)
            {
                return PartnerType.Unknown;
            }

            var value = reader.Value.ToString();

            switch(value)
            {
                case "Partner":
                    return PartnerType.Partner;
                case "Sponsor":
                    return PartnerType.Sponsor;
                case "Media Sponsor":
                    return PartnerType.Media;
                default: 
                    return PartnerType.Unknown;
            }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
