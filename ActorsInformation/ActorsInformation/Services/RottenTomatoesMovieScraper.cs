using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ActorsInformation.Services
{
    public class RottenTomatoesMovieScraper : MoviesScraperBase
    {
        private static readonly string ROTTENTOMATOES_URL = "";
        private static readonly string SOURCE = "RottenTomatoes";

        private static readonly string MOVIES_EL_ID = "";
        private static readonly string NAME_EL_ID = "";
        private static readonly string RANK_EL_ID = "";
        private static readonly string TYPE_EL_ID = "";
        private static readonly string DETAILS_EL_ID = "";

        public RottenTomatoesMovieScraper() : base(ROTTENTOMATOES_URL, MOVIES_EL_ID, NAME_EL_ID, RANK_EL_ID, TYPE_EL_ID, DETAILS_EL_ID, SOURCE)
        {
        }
    }
}
