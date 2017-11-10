using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LAPhil.Application;
using LAPhil.Logging;



namespace HollywoodBowl.Services
{
    
    public class SeasonsService
    {
        Logger<SeasonsService> Log = ServiceContainer.Resolve<LoggingService>().GetLogger<SeasonsService>();
        public readonly ISeasonsDriver Driver;

        public SeasonsService(ISeasonsDriver driver)
        {
            Driver = driver;
        }

        public async Task<List<Season>> SeasonsAsync()
        {
            return await Driver.SeasonsAsync();
        }
    }
}
