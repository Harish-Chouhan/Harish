using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace HollywoodBowl.Services
{
    public interface IEventDriver
    {
        Task<List<Event>> RangeAsync(DateTime start, DateTime end);
        Task<List<Event>> SeasonAsync(int season);
    }
}
