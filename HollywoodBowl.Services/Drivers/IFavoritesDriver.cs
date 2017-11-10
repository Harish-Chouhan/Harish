using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HollywoodBowl.Services
{
    public interface IFavoritesDriver
    {
        Task<List<Favorite<T>>> Favorites<T>()
            where T: IIdentifiable;  
    }
}
