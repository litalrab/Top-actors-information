using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ActorsInformation.Models
{
    public class Actor
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        [Required(ErrorMessage = "Name is required")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use letters only please")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Details is required")]
        public string Details { get; set; }
        [Required(ErrorMessage = "Type is required")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use letters only please")]
        public string Type { get; set; }
        [Required(ErrorMessage = "Rank is required")]
        [Range(0, int.MaxValue, ErrorMessage = "Please enter valid integer Number")]
        public int Rank { get; set; }
        [Required(ErrorMessage = "Source is required")]
        public string Source { get; set; }
    }
}
