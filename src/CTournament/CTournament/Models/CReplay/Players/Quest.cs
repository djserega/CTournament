namespace CTournament.Models.CReplay.Players
{
    public class Quest
    {
        public int NumId { get; set; }
        public int Type { get; set; }
        public string QuestId { get; set; }
        public string ConfigId { get; set; }
        public int Visual { get; set; }
        public decimal Count { get; set; }
        public decimal Target { get; set; }
        public bool IsInMatch { get; set; }
    }
}