using System.Collections.Generic;

namespace JocsDeGuerra.Models
{
    public class OwnedLocation
    {
        public MapLocation Location { get; set; } = new MapLocation();
        public string AssignedResources { get; set; } = "";


        public OwnedLocation()
        {
            
        }
    }
}
