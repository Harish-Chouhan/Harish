using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LAPhil.Application;
using LAPhil.Logging;



namespace HollywoodBowl.Services
{
    
    public class SeasonService
    {
        Logger<SeasonService> Log = ServiceContainer.Resolve<LoggingService>().GetLogger<SeasonService>();
        readonly ISeasonDriver Driver;

        public SeasonService(ISeasonDriver driver)
        {
            Driver = driver;
        }

        public Task<List<Season>> SeasonsAsync()
        {
            return Driver.SeasonsAsync();
        }
    }
}
