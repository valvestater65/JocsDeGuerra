using System;
using System.Collections.Generic;

namespace JocsDeGuerra.Models.ViewModels
{
    public class InformeTornViewModel
    {

        public Guid Id { get; set; }
        public Turn CurrentTurn { get; set; }
        public Team Team { get; set; }
        public List<MapLocation> MapLocations { get; set; }
        public List<Asset> Assets { get; set; }
        public TurnActionsViewModel TurnActions { get; set; }

        public InformeTornViewModel()
        {
            TurnActions = new TurnActionsViewModel();
            MapLocations =  new List<MapLocation>();
            Assets = new List<Asset>();
        }
    }

    public class TurnActionsViewModel
    {
        public Guid TeamId { get; set; }
        public string ProductionText { get; set; }
        public string ExplorationText { get; set; }
        public string ResearchText { get; set; }
        public string IndustryText { get; set; }
        public double ProductionPoints { get; set; }
        public double ExplorationPoints { get; set; }
        public double ResearchPoints { get; set; }
        public double IndustryPoints { get; set; }
    }
}
