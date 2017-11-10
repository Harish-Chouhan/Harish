using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace HollywoodBowl.Services
{
    public class MockSeasonsDriver: ISeasonsDriver
    {
        public async Task<List<Season>> SeasonsAsync()
        {
            return new List<Season>();
        }

    }
}
