using StarWars.Models;
using StarWars.Models.Domain;
using System.Collections.Generic;

namespace StarWars.Models
{
    public class IndexViewModel
    {
        public IEnumerable<Character> Characters { get; set; }
        public PageViewModel PageViewModel { get; set; }
    }
}
