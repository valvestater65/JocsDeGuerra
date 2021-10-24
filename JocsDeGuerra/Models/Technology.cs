using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JocsDeGuerra.Models
{
    public class Technology
    {
        public Guid Id { get; set; }
        public string TechonologyType { get; set; }
        public string Name { get; set; }
        public int BaseResearchCost { get; set; }
        public bool Unlimited { get; set; }
    }
}
