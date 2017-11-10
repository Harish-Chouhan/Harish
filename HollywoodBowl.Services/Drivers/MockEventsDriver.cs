using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Reactive;
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


        public IObservable<HttpBinGet> RangeObservable(DateTime start, DateTime end)
        {
            return Http
                .WithCache(key: "events")
                .GetObservable<HttpBinGet>(url: "https://httpbin.org/get");
        }

        public async Task<List<Event>> RangeAsync(DateTime start, DateTime end)
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
        }

        public async Task<List<Event>> SeasonAsync(int season)
        {
            await Task.Delay(60);

            return new List<Event>(){
                new Event() {Id = 1, Date = DateTime.Today, Label="Mock Event 1"},
                new Event() {Id = 2, Date = DateTime.Today + TimeSpan.FromDays(1), Label="Mock Event 2", Season=season},
                new Event() {Id = 3, Date = DateTime.Today + TimeSpan.FromDays(2), Label="Mock Event 3", Season=season},
                new Event() {Id = 4, Date = DateTime.Today + TimeSpan.FromDays(3), Label="Mock Event 4", Season=season},
                new Event() {Id = 5, Date = DateTime.Today + TimeSpan.FromDays(4), Label="Mock Event 5", Season=season},
            };
        }
    }
}
