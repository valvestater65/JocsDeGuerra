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
    }
}
