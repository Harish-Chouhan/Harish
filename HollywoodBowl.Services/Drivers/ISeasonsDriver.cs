using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace HollywoodBowl.Services
{
    public interface ISeasonDriver
    {
        Task<List<Season>> SeasonsAsync();
    }
}
