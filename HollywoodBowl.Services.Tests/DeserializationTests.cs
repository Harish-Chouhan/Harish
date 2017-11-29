using System;
using Xunit;
using Newtonsoft.Json;


namespace HollywoodBowl.Services.Tests
{
    public class Deserialization: IClassFixture<JsonFixture>
    {
        JsonFixture json;

        public Deserialization(JsonFixture json)
        {
            this.json = json;
        }

        [Fact]
        public void DeserializeArtist()
        {
            var data = ResourceManager.FileString("artist.json");
            var obj = JsonConvert.DeserializeObject<Artist>(data, json.Settings);

            Assert.True(obj.FirstName == "Alice");
            Assert.True(obj.LastName == "Boyum");
            Assert.True(obj.Title == "Art Critic");
            Assert.True(obj.Bio == "<p>Lorem ipsum dolor</p>");
            Assert.True(obj.Website == "https://www.example.com");
            Assert.True(obj.Organization == "");
        }

        [Fact]
        public void DeserializePartner()
        {
            var data = ResourceManager.FileString("partner.json");
            var obj = JsonConvert.DeserializeObject<Partner>(data, json.Settings);

            Assert.True(obj.Name == "SLB Printing");
            Assert.True(obj.Relationship == PartnerType.Media);
            Assert.True(obj.Description == "<p>Lorem ipsum dolor</p>");
            Assert.True(obj.Tagline == "Service Beyond Expectation");
            Assert.True(obj.Website == "https://www.example.com");
            Assert.True(obj.Image == "https://www.example.com/foo.jpg");
        }

        [Fact]
        public void DeserializePerformance()
        {
            var data = ResourceManager.FileString("performance.json");
            var obj = JsonConvert.DeserializeObject<Performance>(data, json.Settings);

            Assert.True(obj.StartTime == new DateTimeOffset(year: 2017, month: 3, day: 25, hour: 18, minute: 30, second: 0, offset: new TimeSpan()));
            Assert.True(obj.PresaleTime == new DateTimeOffset(year: 2017, month: 3, day: 1, hour: 0, minute: 0, second: 0, offset: new TimeSpan()));
            Assert.True(obj.SaleTime == new DateTimeOffset(year: 2017, month: 3, day: 5, hour: 0, minute: 0, second: 0, offset: new TimeSpan()));
            Assert.True(obj.GateTime == new DateTimeOffset(year: 2017, month: 3, day: 25, hour: 17, minute: 0, second: 0, offset: new TimeSpan()));
            Assert.True(obj.PreperformanceTalkStartTime == new DateTime(year: 0001, month: 1, day: 1, hour: 15, minute: 0, second: 0));
            Assert.True(obj.Series.Name == "Thursday Night Classics");
            Assert.True(obj.Series.ShortCode == "TNC");
        }

        [Fact]
        public void DeserializePiece()
        {
            var data = ResourceManager.FileString("piece.json");
            var obj = JsonConvert.DeserializeObject<Piece>(data, json.Settings);

            Assert.True(obj.Name == "Piece Name");
            Assert.True(obj.Duration.Ticks == new TimeSpan(hours: 0, minutes: 0, seconds: 300).Ticks);
            Assert.True(obj.Description == "<p>Lorem Ipsum Dolor</p>");
            Assert.True(obj.Composer.FirstName == "John");
            Assert.True(obj.Composer.LastName == "Korsman");
            Assert.True(obj.Composer.Title == "Composer");
            Assert.True(obj.Composer.Bio == "<p>Lorem ipsum dolor</p>");
        }

        [Fact]
        public void DeserializeProgram()
        {
            var data = ResourceManager.FileString("program.json");
            var obj = JsonConvert.DeserializeObject<Program>(data, json.Settings);

            Assert.True(obj.Id == 1);
            Assert.True(obj.IsLeaseEvent == true);
            Assert.True(obj.Name == "Music");
            Assert.True(obj.Image == "https://www.example.com/foo.jpg");
            Assert.True(obj.Description == "<p>Lorem ipsum dolor</p>");
            Assert.True(obj.PreperformanceSpeaker.FirstName == "Alice");
            Assert.True(obj.PreperformanceSpeaker.LastName == "Boyum");
            Assert.True(obj.PreperformanceSpeaker.Title == "Art Critic");
            Assert.True(obj.PreperformanceSpeaker.Bio == "<p>Lorem ipsum dolor</p>");
            Assert.True(obj.PreperformanceSpeaker.Website == "https://www.example.com");
            Assert.True(obj.PreperformanceSpeaker.Organization == "LA Times");
            Assert.True(obj.Season.Name == "Season HB 5");
            Assert.True(obj.Season.StartDate == new DateTime(2017, 01, 20));
            Assert.True(obj.Season.EndDate == new DateTime(2017, 12, 31));
            Assert.True(obj.Venue.Name == "Venue 1");
            Assert.True(obj.Venue.Website == "https://www.example.com");
            Assert.True(obj.Venue.Phone == "310-555-5555");
            Assert.True(obj.Venue.Address == "1234 Lane Ave., Los Angeles, CA 90001");
            Assert.True(obj.Venue.Image == "https://www.example.com/foo.jpg");
            Assert.True(obj.Venue.Description == "<p>Lorem ipsum dolor</p>");
            Assert.True(obj.Performances.Count == 1);
            Assert.True(obj.Performances[0].StartTime == new DateTimeOffset(year: 2017, month: 3, day: 25, hour: 18, minute: 30, second: 0, offset: new TimeSpan()));
            Assert.True(obj.Performances[0].PresaleTime == new DateTimeOffset(year: 2017, month: 3, day: 1, hour: 0, minute: 0, second: 0, offset: new TimeSpan()));
            Assert.True(obj.Performances[0].SaleTime == new DateTimeOffset(year: 2017, month: 3, day: 5, hour: 0, minute: 0, second: 0, offset: new TimeSpan()));
            Assert.True(obj.Performances[0].GateTime == new DateTimeOffset(year: 2017, month: 3, day: 25, hour: 17, minute: 0, second: 0, offset: new TimeSpan()));
            Assert.True(obj.Performances[0].PreperformanceTalkStartTime == new DateTime(year: 0001, month: 1, day: 1, hour: 15, minute: 0, second: 0));
            Assert.True(obj.Performances[0].Series.Name == "Thursday Night Classics");
            Assert.True(obj.Performances[0].Series.ShortCode == "TNC");
        }

        [Fact]
        public void DeserializeProgramDetail()
        {
            var data = ResourceManager.FileString("program_detail.json");
            var obj = JsonConvert.DeserializeObject<ProgramDetail>(data, json.Settings);

            Assert.NotNull(obj.Data);
            Assert.True(obj.Data.Performers.Count == 2);
            Assert.True(obj.Data.Partners.Count == 1);
            Assert.True(obj.Data.Pieces.Count == 1);

            Assert.True(obj.Data.Pieces[0].Name == "Piece Name");
            Assert.True(obj.Data.Pieces[0].Duration.Ticks == new TimeSpan(hours: 0, minutes: 0, seconds: 300).Ticks);
            Assert.True(obj.Data.Pieces[0].Description == "<p>Lorem Ipsum Dolor</p>");
            Assert.True(obj.Data.Pieces[0].Composer.FirstName == "John");
            Assert.True(obj.Data.Pieces[0].Composer.LastName == "Korsman");
            Assert.True(obj.Data.Pieces[0].Composer.Title == "Composer");
            Assert.True(obj.Data.Pieces[0].Composer.Bio == "<p>Lorem ipsum dolor</p>");

            Assert.True(obj.Data.Partners[0].Name == "SLB Printing");
            Assert.True(obj.Data.Partners[0].Relationship == PartnerType.Media);
            Assert.True(obj.Data.Partners[0].Description == "<p>Lorem ipsum dolor</p>");
            Assert.True(obj.Data.Partners[0].Tagline == "Service Beyond Expectation");
            Assert.True(obj.Data.Partners[0].Website == "https://www.example.com");
            Assert.True(obj.Data.Partners[0].Image == "https://www.example.com/foo.jpg");

            Assert.True(obj.Data.Performers[0].FirstName == "Lucy");
            Assert.True(obj.Data.Performers[0].LastName == "Pepper");
            Assert.True(obj.Data.Performers[0].Title == "Choreographer");
            Assert.True(obj.Data.Performers[0].Bio == "<p>Lorem ipsum dolor</p>");
            Assert.True(obj.Data.Performers[0].Website == "https://www.example.com");
            Assert.True(obj.Data.Performers[0].Organization == "");

            Assert.True(obj.Data.Performers[1].FirstName == "Ollie");
            Assert.True(obj.Data.Performers[1].LastName == "Chisholm");
            Assert.True(obj.Data.Performers[1].Title == "Costume Designer");
            Assert.True(obj.Data.Performers[1].Bio == "<p>Lorem ipsum dolor</p>");
            Assert.True(obj.Data.Performers[1].Website == "");
            Assert.True(obj.Data.Performers[1].Organization == "Artist Organization");
        }

        [Fact]
        public void DeserializeProgramList()
        {
            var data = ResourceManager.FileString("program_list.json");
            var obj = JsonConvert.DeserializeObject<ProgramList>(data, json.Settings);

            Assert.True(obj.Data.Count == 1);
            Assert.True(obj.Data[0].Id == 1);
            Assert.True(obj.Data[0].IsLeaseEvent == true);
            Assert.True(obj.Data[0].Name == "Music");
            Assert.True(obj.Data[0].Image == "https://www.example.com/foo.jpg");
            Assert.True(obj.Data[0].Description == "<p>Lorem ipsum dolor</p>");
            Assert.True(obj.Data[0].PreperformanceSpeaker.FirstName == "Alice");
            Assert.True(obj.Data[0].PreperformanceSpeaker.LastName == "Boyum");
            Assert.True(obj.Data[0].PreperformanceSpeaker.Title == "Art Critic");
            Assert.True(obj.Data[0].PreperformanceSpeaker.Bio == "<p>Lorem ipsum dolor</p>");
            Assert.True(obj.Data[0].PreperformanceSpeaker.Website == "https://www.example.com");
            Assert.True(obj.Data[0].PreperformanceSpeaker.Organization == "LA Times");
            Assert.True(obj.Data[0].Season.Name == "Season HB 5");
            Assert.True(obj.Data[0].Season.StartDate == new DateTime(2017, 01, 20));
            Assert.True(obj.Data[0].Season.EndDate == new DateTime(2017, 12, 31));
            Assert.True(obj.Data[0].Venue.Name == "Venue 1");
            Assert.True(obj.Data[0].Venue.Website == "https://www.example.com");
            Assert.True(obj.Data[0].Venue.Phone == "310-555-5555");
            Assert.True(obj.Data[0].Venue.Address == "1234 Lane Ave., Los Angeles, CA 90001");
            Assert.True(obj.Data[0].Venue.Image == "https://www.example.com/foo.jpg");
            Assert.True(obj.Data[0].Venue.Description == "<p>Lorem ipsum dolor</p>");
            Assert.True(obj.Data[0].Performances.Count == 1);
            Assert.True(obj.Data[0].Performances[0].StartTime == new DateTimeOffset(year: 2017, month: 3, day: 25, hour: 18, minute: 30, second: 0, offset: new TimeSpan()));
            Assert.True(obj.Data[0].Performances[0].PresaleTime == new DateTimeOffset(year: 2017, month: 3, day: 1, hour: 0, minute: 0, second: 0, offset: new TimeSpan()));
            Assert.True(obj.Data[0].Performances[0].SaleTime == new DateTimeOffset(year: 2017, month: 3, day: 5, hour: 0, minute: 0, second: 0, offset: new TimeSpan()));
            Assert.True(obj.Data[0].Performances[0].GateTime == new DateTimeOffset(year: 2017, month: 3, day: 25, hour: 17, minute: 0, second: 0, offset: new TimeSpan()));
            Assert.True(obj.Data[0].Performances[0].PreperformanceTalkStartTime == new DateTime(year: 0001, month: 1, day: 1, hour: 15, minute: 0, second: 0));
            Assert.True(obj.Data[0].Performances[0].Series.Name == "Thursday Night Classics");
            Assert.True(obj.Data[0].Performances[0].Series.ShortCode == "TNC");
        }

        [Fact]
        public void DeserializeSeason()
        {
            var data = ResourceManager.FileString("season.json");
            var obj = JsonConvert.DeserializeObject<Season>(data, json.Settings);

            Assert.True(obj.Name == "Season HB 5");
            Assert.True(obj.StartDate == new DateTime(2017, 01, 20));
            Assert.True(obj.EndDate == new DateTime(2017, 12, 31));
        }

        [Fact]
        public void DeserializeSeries()
        {
            var data = ResourceManager.FileString("series.json");
            var obj = JsonConvert.DeserializeObject<Series>(data, json.Settings);

            Assert.True(obj.Name == "Thursday Night Classics");
            Assert.True(obj.ShortCode == "TNC");
        }

        [Fact]
        public void DeserializeVenue()
        {
            var data = ResourceManager.FileString("venue.json");
            var obj = JsonConvert.DeserializeObject<Venue>(data, json.Settings);

            Assert.True(obj.Name == "Venue 1");
            Assert.True(obj.Phone == "310-555-5555");
            Assert.True(obj.Address == "1234 Lane Ave., Los Angeles, CA 90001");
            Assert.True(obj.Description == "<p>Lorem ipsum dolor</p>");
            Assert.True(obj.Image == "https://www.example.com/foo.jpg");
        }
    }
}
