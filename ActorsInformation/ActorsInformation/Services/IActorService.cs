using ActorsInformation.Controllers;
using ActorsInformation.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ActorsInformation.Services
{
    public interface IActorService
    {
        Task AddActor(Actor actor);
        Task DeleteActor(string id);
        Task<Actor> GetActor(string actorId);
        Task<List<Actor>> GetActorsAsync(GetActorsRequest getActorsRequest);
        void LoadActors();
        Task UpdateActor(Actor actor);
    }
}