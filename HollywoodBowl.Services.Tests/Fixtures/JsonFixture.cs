using System;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;

namespace HollywoodBowl.Services.Tests
{
    public class JsonFixture
    {
        public JsonSerializerSettings Settings;

        public JsonFixture()
        {
            var contractResolver = new DefaultContractResolver
            {
                NamingStrategy = new SnakeCaseNamingStrategy()
            };

            Settings = new JsonSerializerSettings
            {
                ContractResolver = contractResolver
            };
        }
    }
}
