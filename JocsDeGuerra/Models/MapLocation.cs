using JocsDeGuerra.Constants;
using System;

namespace JocsDeGuerra.Models
{
    public class MapLocation
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int ProductionPoints { get; set; }
        public int ResearchPoints { get; set; }
        public int ExplorationPoints { get; set; }
        public Team Owner { get; set; }
        public string BattleArchetipes { get; set; }
        public bool IsConquerable { get; set; }
        public bool IsNeutral { get; set; }
    }
}
