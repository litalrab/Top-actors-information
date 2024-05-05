using ActorsInformation.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ActorsInformation.Data
{
    public class AppDataContext : DbContext
    {
        public AppDataContext(DbContextOptions<AppDataContext> options) : base(options)
        {
        }
        public DbSet<Actor> Actors { get; set; }
    }
}
