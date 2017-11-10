using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LAPhil.Application;
using LAPhil.Logging;

namespace HollywoodBowl.Services
{
    public class MockFavoritesDriver: IFavoritesDriver
    {
        Logger<MockFavoritesDriver> Log = ServiceContainer.Resolve<LoggingService>().GetLogger<MockFavoritesDriver>();

        public MockFavoritesDriver()
        {
            Log.Debug("Initialized Mock Favorites Driver");
        }

        public async Task<List<Favorite<T>>> Favorites<T>() 
            where T: IIdentifiable
        {
            await Task.Delay(60);
            return new List<Favorite<T>>();
        }

    }
}
