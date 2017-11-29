using System;
using System.Collections.Generic;



namespace HollywoodBowl.Services
{
    public class ProgramDetailData{
        public List<Artist> Performers { get; set; }
        public List<Partner> Partners { get; set; }
        public List<Piece> Pieces { get; set; }
    }

    public class ProgramDetail
    {
        public ProgramDetailData Data { get; set; }

        public ProgramDetail()
        {
        }
    }
}
