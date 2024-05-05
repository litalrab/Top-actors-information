using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ActorsInformation.Models
{
    public class ActorsResponseItem
    {

        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; }

    }
    public class ActorsResponse : Response
    {
        public List<ActorsResponseItem> Actors { get; set; }
    }
}
