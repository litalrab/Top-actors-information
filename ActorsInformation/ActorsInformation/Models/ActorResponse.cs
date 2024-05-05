using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ActorsInformation.Models
{
    public class ActorResponse : Response
    {
        public Actor Actor { get; set; }
    }
}
