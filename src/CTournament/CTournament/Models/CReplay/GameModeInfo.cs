using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CTournament.Models.CReplay
{
    public class GameModeInfo
    {
        [JsonProperty("game_mode")]
        public string GameMode { get; set; }
        public string Date { get; set; }
        public double ReplayStartSeconds { get; set; }
        public string MatchId { get; set; }
        //public List<Endpoints> Endpoints { get; set; }
        public string Map { get; set; }
        public string Mission { get; set; }
        public List<int> Segments { get; set; }
        public string GameServerVersion { get; set; }
        public List<Players.PlayersInfo> Players { get; set; }
        public string ReplayPath { get; set; }

        [NotMapped]
        public string GameModeView { get; set; }
        [NotMapped]
        public DateTime DateView { get; set; }
        [NotMapped]
        public string MissionView { get; set; }
        [NotMapped]
        public string MapView { get; set; }

        internal static void FillObject(GameModeInfo gameModeInfo)
        {
            gameModeInfo.GameModeView = DictionaryTemplates.GetTypeMission(gameModeInfo.GameMode);

            if ( DateTime.TryParse(gameModeInfo.Date, out DateTime dateTimeParse))
                gameModeInfo.DateView = dateTimeParse;

            gameModeInfo.MissionView = DictionaryTemplates.GetNameMission(gameModeInfo.Mission);
            gameModeInfo.MapView = DictionaryTemplates.GetNameMap(gameModeInfo.Map);

            for (int i = 0; i < gameModeInfo.Players.Count; i++)
                CReplay.Players.PlayersInfo.FillObject(gameModeInfo.Players[i]);
        }
    }
}
