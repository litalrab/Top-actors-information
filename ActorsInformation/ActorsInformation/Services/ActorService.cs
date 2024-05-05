using ActorsInformation.Controllers;
using ActorsInformation.Data;
using ActorsInformation.Exceptions;
using ActorsInformation.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ActorsInformation.Services
{
    public class ActorService : IActorService
    {
        private AppDataContext DBContext;
        private IMoviesScraper ActorScraper;

        public ActorService(AppDataContext context, IMoviesScraper actorScraper)
        {
            DBContext = context;
            ActorScraper = actorScraper;
        }

        public async Task<List<Actor>> GetActorsAsync(GetActorsRequest getActorsRequest)
        {
            var query = DBContext.Actors.AsQueryable();

            if (!string.IsNullOrEmpty(getActorsRequest.Name))
                query = query.Where(
                    actor =>
                        actor.Name.Contains(
                            getActorsRequest.Name,
                            StringComparison.InvariantCultureIgnoreCase
                        )
                );

            if (getActorsRequest.MinRank.HasValue)
                query = query.Where(actor => actor.Rank >= getActorsRequest.MinRank.Value);

            if (getActorsRequest.MaxRank.HasValue)
                query = query.Where(actor => actor.Rank <= getActorsRequest.MaxRank.Value);


            query = query.Skip(getActorsRequest.Skip).Take(getActorsRequest.Take);

            return await query.ToListAsync();
        }
        public void LoadActors()
        {
            List<Actor> actors = (List<Actor>)ActorScraper.LoadActors();
            DBContext.Actors.AddRange(actors);
            DBContext.SaveChanges();
        }

        public async Task AddActor(Actor actor)
        {
            Validate(actor);
            DBContext.Actors.Add(actor);
            await SaveAsync();
        }

        public async Task<Actor> GetActor(string actorId)
        {
            try
            {
                Actor actor = await GetActorByid(actorId);
                return actor;
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        private async Task<Actor> GetActorByid(string actorId)
        {
            Actor actor = await DBContext.Actors.FirstOrDefaultAsync(actor => actor.Id == actorId);
            if (actor == null)
            {
                throw new ActorNotFoundException();
            }

            return actor;
        }

        public async Task DeleteActor(string id)
        {
            Actor actor = await GetActorByid(id);
            DBContext.Actors.Remove(actor);
            await SaveAsync();
        }

        public async Task UpdateActor(Actor actor)
        {
            var existingActor = DBContext
                .Actors
                .FirstOrDefault(
                    a => a.Id.Equals(actor.Id, StringComparison.InvariantCultureIgnoreCase)
                );

            if (existingActor != null)
            {
                Validate(actor);
                DBContext.Actors.Remove(existingActor);
            }
            
            DBContext.Actors.Add(actor);
            await SaveAsync();
        }

        private void Validate(Actor actor)
        {
            if (DBContext.Actors.Any(a => a.Rank == actor.Rank))
            {
                throw new DuplicateRankException();

            }
        }

        private async Task SaveAsync()
        {
            await DBContext.SaveChangesAsync();
        }
    }
}
