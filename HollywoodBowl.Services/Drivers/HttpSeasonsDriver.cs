using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace HollywoodBowl.Services
{
    public class HttpSeasonDriver
    {
        public async Task<List<Season>> SeasonsAsync()
        {
            await Task.Delay(60);
            return new List<Season>();
        }
    }
}
