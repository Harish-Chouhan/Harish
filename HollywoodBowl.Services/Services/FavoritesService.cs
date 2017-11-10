using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LAPhil.Application;
using LAPhil.Logging;


namespace HollywoodBowl.Services
{
    public class FavoritesService
    {
        Logger<FavoritesService> Log = ServiceContainer.Resolve<LoggingService>().GetLogger<FavoritesService>();
        public readonly IFavoritesDriver Driver;


        public FavoritesService(IFavoritesDriver driver)
        {
            Driver = driver;
        }

        public async Task<List<Favorite<T>>> Favorites<T>()
            where T : IIdentifiable
        {
            return await Driver.Favorites<T>();
        }
    }
}
