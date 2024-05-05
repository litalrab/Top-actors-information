using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ActorsInformation.Models
{
    public class GetActorsRequest
    {
        public string Name { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Please enter valid integer Number")]
        public int? MinRank { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Please enter valid integer Number")]
        public int? MaxRank { get; set; }

        public int Skip { get; set; } = 0;
        public int Take { get; set; } = 20;
    }
}
