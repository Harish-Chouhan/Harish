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

        public IObservable<List<Event>> InRange(DateTime start, DateTime end)
        {
            return Http
                .WithCache(key: "events")
                .Get<List<Event>>(url: "https://httpbin.org");
        }

        public IObservable<List<Event>> Season(int season)
        {
            return Http
                .WithCache(key: "events")
                .Get<List<Event>>(url: "https://httpbin.org");
        }
    }
}
