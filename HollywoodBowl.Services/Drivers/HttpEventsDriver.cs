using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LAPhil.Application;
using LAPhil.Logging;
using LAPhil.HTTP;


namespace HollywoodBowl.Services
{
    public class HttpEventsDriver: IEventsDriver
    {
        Logger<HttpEventsDriver> Log = ServiceContainer.Resolve<LoggingService>().GetLogger<HttpEventsDriver>();

        HttpService Http = ServiceContainer.Resolve<HttpService>();
        public string Version { get; private set; }

        public HttpEventsDriver(string version)
        {
            Version = version;
            Log.Debug("Initialized Http Events Service Driver");
        }

        public async Task<List<Event>> RangeAsync(DateTime start, DateTime end)
        {
            return new List<Event>();
        }

        public async Task<List<Event>> SeasonAsync(int season)
        {
            return new List<Event>();
        }
    }
}
