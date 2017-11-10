using System;


namespace HollywoodBowl.Services
{
    public class Event: IIdentifiable
    {
        public int Id { get; set; }
        public string Label { get; set;}
        public DateTime Date { get; set; }
        public int Season { get; set; }

        public Event()
        {
        }
    }
}
