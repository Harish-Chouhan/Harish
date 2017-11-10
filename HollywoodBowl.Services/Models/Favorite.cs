using System;

namespace HollywoodBowl.Services
{
    public class Favorite<T> 
        where T: IIdentifiable
    {
        T Value { get; set; }

        public Favorite(T value)
        {
            Value = value;
        }
    }
}
