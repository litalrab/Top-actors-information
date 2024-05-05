using ActorsInformation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ActorsInformation.Services
{
    public interface IMoviesScraper
    {
        public List<Actor> LoadActors();

    }
}
