using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LAPhil.Logging;
using LAPhil.Application;


namespace HollywoodBowl.Services
{
    public class MockEventDriver: IEventsDriver
    {
        Logger<MockEventDriver> Log = ServiceContainer.Resolve<LoggingService>().GetLogger<MockEventDriver>();

        public MockEventDriver()
        {
            Log.Debug("Initialized Mock Event Service Driver");
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
