using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace HollywoodBowl.Services
{
    public interface ISeasonsDriver
    {
        Task<List<Season>> SeasonsAsync();
    }
}
