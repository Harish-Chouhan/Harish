using System;
using System.Reactive.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using LAPhil.Logging;
using LAPhil.Application;
using LAPhil.HTTP;



namespace HollywoodBowl.Services
{
    public class HttpBinGet
    {
        public string Origin { get; set; }
        public string Url { get; set; }
    }

    public class MockEventsDriver: IEventsDriver
    {
        Logger<MockEventsDriver> Log = ServiceContainer.Resolve<LoggingService>().GetLogger<MockEventsDriver>();
        HttpService Http = ServiceContainer.Resolve<HttpService>();


        public MockEventsDriver()
        {
            Log.Debug("Initialized Mock Events Driver");
        }



        public IObservable<List<Event>> InRange(DateTime start, DateTime end)
        {

            return Observable.FromAsync(async () =>
            {
                await Task.Delay(60);

                var mockResult = new List<Event>(){
                    new Event() {Id = 1, Date = DateTime.Today, Label="Mock Event 1"},
                    new Event() {Id = 2, Date = DateTime.Today + TimeSpan.FromDays(1), Label="Mock Event 2", Season=1},
                    new Event() {Id = 3, Date = DateTime.Today + TimeSpan.FromDays(2), Label="Mock Event 3", Season=1},
                    new Event() {Id = 4, Date = DateTime.Today + TimeSpan.FromDays(3), Label="Mock Event 4", Season=1},
                    new Event() {Id = 5, Date = DateTime.Today + TimeSpan.FromDays(4), Label="Mock Event 5", Season=2},
                };

                return mockResult;
            })
            .WithCache("events");
        }

        public IObservable<List<Event>> Season(int season)
        {

            return Observable.FromAsync(async () =>
            {
                await Task.Delay(60);

                var mockResult = new List<Event>(){
                    new Event() {Id = 1, Date = DateTime.Today, Label="Mock Event 1", Season=season},
                    new Event() {Id = 2, Date = DateTime.Today + TimeSpan.FromDays(1), Label="Mock Event 2", Season=season},
                    new Event() {Id = 3, Date = DateTime.Today + TimeSpan.FromDays(2), Label="Mock Event 3", Season=season},
                    new Event() {Id = 4, Date = DateTime.Today + TimeSpan.FromDays(3), Label="Mock Event 4", Season=season},
                    new Event() {Id = 5, Date = DateTime.Today + TimeSpan.FromDays(4), Label="Mock Event 5", Season=season},
                };

                return mockResult;
            });
        }
    }
}
