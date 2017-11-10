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
        readonly IEventDriver Driver;

        public EventsService(IEventDriver driver)
        {
            Driver = driver;
        }

        public Task RangeAsync(DateTime start, DateTime end)
        {
            return Driver.RangeAsync(start, end);
        }

        public Task SeasonAsync(int season)
        {
            return Driver.SeasonAsync(season);
        }
    }
}
