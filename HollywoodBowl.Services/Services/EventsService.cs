using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LAPhil.Application;
using LAPhil.Logging;


namespace HollywoodBowl.Services
{
    public class EventsService
    {
        Logger<EventsService> Log = ServiceContainer.Resolve<LoggingService>().GetLogger<EventsService>();
        public readonly IEventsDriver Driver;

        public EventsService(IEventsDriver driver)
        {
            Driver = driver;
        }

        public async Task<List<Event>> RangeAsync(DateTime start, DateTime end)
        {
            return await Driver.RangeAsync(start, end);
        }

        public IObservable<HttpBinGet>RangeObservable(DateTime start, DateTime end)
        {
            return Driver.RangeObservable(start, end);
        }

        public async Task<List<Event>> SeasonAsync(int season)
        {
            return await Driver.SeasonAsync(season);
        }
    }
}
