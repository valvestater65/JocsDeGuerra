using System;
using System.Collections.Generic;

namespace JocsDeGuerra.Models
{
    public class Team
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int BaseProductionPoints { get; set; }
        public int BaseExplorationPoints { get; set; }
        public int BaseResearchPoints { get; set; }
        public AccumulatedPoints AvailablePoints { get; set; }
        public List<MapLocation> OwnedLocations { get; set; }
        public List<TeamAsset> AvailableAssets { get; set; }
    }
}
