using System;
using System.Collections.Generic;


namespace HollywoodBowl.Services
{
    public interface IEventsDriver
    {
        IObservable<List<Event>> InRange(DateTime start, DateTime end);
        IObservable<List<Event>> Season(int season);
    }
}
