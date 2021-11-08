using System;

namespace JocsDeGuerra.Models
{
    public class Battle
    {
        public MapLocation Location { get; set; }
        public Guid WinnerId { get; set; }
        public Guid LooserId { get; set; }
        public bool Draw { get; set; }
    }
}
