using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LAPhil.Application;
using LAPhil.Logging;


namespace HollywoodBowl.Services
{
    public class MockSeasonsDriver: ISeasonsDriver
    {
        Logger<MockSeasonsDriver> Log = ServiceContainer.Resolve<LoggingService>().GetLogger<MockSeasonsDriver>();

        public MockSeasonsDriver()
        {
            Log.Debug("Initialized Mock Seasons Driver");
        }

        public async Task<List<Season>> SeasonsAsync()
        {
            await Task.Delay(60);
            var today = DateTime.Today;

            return new List<Season>();
        }

    }
}
