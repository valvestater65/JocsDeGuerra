namespace JocsDeGuerra.Models
{
    public class Battle
    {
        public MapLocation Location { get; set; }
        public Team Winner { get; set; }
        public Team Looser { get; set; }
        public bool Draw { get; set; }
    }
}
