using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ActorsInformation.Exceptions
{
    public class DuplicateRankException : Exception
    {
        private static readonly string MESSAGE = "Duplicate Rank";

        public DuplicateRankException() : base(MESSAGE) { }
    }
}
