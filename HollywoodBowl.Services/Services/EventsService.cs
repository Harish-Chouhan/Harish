using System;
using System.Collections.Generic;
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

        public IObservable<List<Event>> InRange(DateTime start, DateTime end)
        {
            return Driver.InRange(start, end);
        }

        public IObservable<List<Event>> Season(int season)
        {
            return Driver.Season(season);
        }
    }
}
