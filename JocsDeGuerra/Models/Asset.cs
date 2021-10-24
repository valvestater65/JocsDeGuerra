using System;

namespace JocsDeGuerra.Models
{
    public class Asset
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Abbrv { get; set; }
        public int BaseProductionCost { get; set; }
        public int BaseResearchCost { get; set; }
        public bool Enabled { get; set; }
    }
}
