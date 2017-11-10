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

            return new List<Season>(){
                new Season() {Id = 1, Label=$"{today.Year} Season", StartDate = new DateTime(year: today.Year, month: 1, day: 1), EndDate=new DateTime(year: today.Year, month: 12, day: 31)},
                new Season() {Id = 2, Label=$"{today.Year + 1} Season", StartDate = new DateTime(year: today.Year + 1, month: 1, day: 1), EndDate=new DateTime(year: today.Year + 1, month: 12, day: 31)},
                new Season() {Id = 3, Label=$"{today.Year + 2} Season", StartDate = new DateTime(year: today.Year + 2, month: 1, day: 1), EndDate=new DateTime(year: today.Year + 2, month: 12, day: 31)},
            };
        }

    }
}
