using JocsDeGuerra.Models.ViewModels;
using System;
using System.Collections.Generic;

namespace JocsDeGuerra.Models
{
    public class Turn
    {
        public Guid Id { get; set; }
        public int TurnNumber { get; set; }
        public bool Completed { get; set; }
        public List<Team> Teams { get; set; }
        public Battle Battle { get; set; }
        public bool Current { get; set; }
        public List<TurnActionsViewModel> TurnActions { get; set; }

        public Turn()
        {
            TurnActions = new List<TurnActionsViewModel>();
            Teams = new List<Team>();
        }
    }
}
