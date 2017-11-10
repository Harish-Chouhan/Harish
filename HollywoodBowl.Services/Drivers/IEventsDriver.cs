using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace HollywoodBowl.Services
{
    public interface IEventsDriver
    {
        Task<List<Event>> RangeAsync(DateTime start, DateTime end);
        IObservable<HttpBinGet> RangeObservable(DateTime start, DateTime end);
        Task<List<Event>> SeasonAsync(int season);
    }
}
