using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ActorsInformation.Exceptions
{
    public class ActorNotFoundException : Exception
    {
        private static readonly string MESSAGE = "Actor doesn't exist";
        public ActorNotFoundException() : base(MESSAGE) { }
    }
}
