using System;
using System.Collections.Generic;
using System.Text;

namespace CTournament.Models
{
    public class TournamentReplay
    {
        public DateTime Date { get; set; }
        public string Name { get; set; }
        public string FullName { get; set; }
        public CalculationParameters Data { get; set; }
        public CReplay.Players.PlayersInfo[] PlayersData { get; set; }
        public DateTime MatchTime{ get; set; }
        public string MatchTimeAsTime { get; set; }
        public string Capitan { get; set; }
        public string Operator { get; set; }
        public int MinLevelOperators { get; set; }
        public bool IsRecruit { get; set; }
        public string Mission { get; set; }
        public string GameMode { get; set; }
        public int PlayedRoundsCount { get; set; }

        public double Result { get; set; }
    }
}
