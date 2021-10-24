using System.Collections.Generic;

namespace JocsDeGuerra.Models.ViewModels
{
    public class InformeTornViewModel
    {
        public Turn CurrentTurn { get; set; }
        public Team Team { get; set; }
        public List<MapLocation> MapLocations { get; set; }
        public List<Asset> Assets { get; set; }
        public TurnActionsViewModel TurnActions { get; set; }
    }

    public class TurnActionsViewModel
    {
        public string ProductionText { get; set; }
        public string ExplorationText { get; set; }
        public string ResearchText { get; set; }
        public string IndustryText { get; set; }
        public int ProductionPoints { get; set; }
        public int ExplorationPoints { get; set; }
        public int ResearchPoints { get; set; }
        public int IndustryPoints { get; set; }
    }
}
