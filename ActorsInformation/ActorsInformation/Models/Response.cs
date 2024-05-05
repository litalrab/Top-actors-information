using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ActorsInformation.Models
{
    public class Response
    {
        public string TraceId { get; set; } = Guid.NewGuid().ToString();
        public bool IsSuccess { get; set; }
        public int StatusCode { get; set; }
        public List<Error> Errors { get; set; }
    }
}
