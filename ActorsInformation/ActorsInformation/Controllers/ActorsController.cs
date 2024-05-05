using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ActorsInformation.Data;
using ActorsInformation.Models;
using ActorsInformation.Services;
using System.Net;
using Microsoft.AspNetCore.Authorization;

namespace ActorsInformation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActorsController : ControllerBase
    {
        private readonly IActorService ActorService;
        public ActorsController(IActorService actorService)
        {
            ActorService = actorService;
        }

        // GET: api/Actors
        [HttpGet]
        public async Task<ActorsResponse> GetActors([FromQuery] GetActorsRequest request)
        {
            List<Actor> actors = await ActorService.GetActorsAsync(request);

            List<ActorsResponseItem> data = actors.Select(a => new ActorsResponseItem { Id = a.Id, Name = a.Name }).ToList();
            return new ActorsResponse
            {
                IsSuccess = true,
                Actors = data,
                StatusCode = StatusCodes.Status200OK,
            };
        }

        // GET: api/Actors/5
        [HttpGet("{id}")]
        public async Task<ActorResponse> GetActor(string id)
        {
            Actor actor = await ActorService.GetActor(id);

            return new ActorResponse
            {
                IsSuccess = true,
                Actor = actor,
                StatusCode = StatusCodes.Status200OK,
            };
        }

        [HttpPost]
        public async Task<Response> AddActor([FromBody] Actor addActorRequest)
        {
           
            await ActorService.AddActor(addActorRequest);

            return new Response
            {
                IsSuccess = true,
                StatusCode = StatusCodes.Status200OK,
            };
        }

        // PUT: api/Actors/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut]
       
        public async Task<Response> UpdateActor(Actor actor)
        {
            await ActorService.UpdateActor(actor);
         
            return new Response
            {
                IsSuccess = true,
                StatusCode = StatusCodes.Status200OK,
            };
        }

        // DELETE: api/Actors/5
        [HttpDelete("{id}")]
        public async Task<Response> DeleteActor(string id)
        {
            await ActorService.DeleteActor(id);
          
            return new Response
            {
                IsSuccess = true,
                StatusCode = StatusCodes.Status200OK,
            };
        }
    }
}
