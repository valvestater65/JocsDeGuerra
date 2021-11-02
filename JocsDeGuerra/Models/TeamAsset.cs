using System;

namespace JocsDeGuerra.Models
{
    public class TeamAsset
    {
        public Guid Id { get; set; }
        public Asset Asset { get; set; }
        public int Available { get; set; }
        public int Reserved { get; set; }
    }
}
