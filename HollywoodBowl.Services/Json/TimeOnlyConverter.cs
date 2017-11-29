using System;
using System.Linq;
using Newtonsoft.Json;


namespace HollywoodBowl.Services.Json
{
    public class TimeOnlyConverter: JsonConverter
    {

        public override bool CanConvert(Type objectType)
        {
            return true;
        }


        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType != JsonToken.String)
            {
                return new DateTime(year: 0001, month: 1, day: 1, hour: 0, minute: 0, second: 0);
            }

            var value = reader.Value.ToString()
                              .Split(':')
                              .Select(x => int.Parse(x.Trim()))
                              .ToArray();

            var hour = value[0];
            var minute = value[1];

            return new DateTime(year: 0001, month: 1, day: 1, hour: hour, minute: minute, second: 0);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
