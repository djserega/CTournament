using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.AccessControl;
using System.Text;
using System.Windows.Media.Imaging;

namespace CTournament.Models.CReplay.Players
{
    public class PlayersInfo
    {
        public long UserId { get; set; }
        public string NickName { get; set; }
        public int Level { get; set; }
        public string UserBar { get; set; }
        public bool HasPremium { get; set; }
        public int TeamId { get; set; }
        public int RankedSeasonPoints { get; set; }
        public int RrankedSeasonNumBattles { get; set; }
        [JsonProperty("player_human_data")]
        public CoreGameCfg CoreGameCfg { get; set; }

        [NotMapped]
        public PlayersDataInfo PlayersDataInfo { get; set; }

        public static void FillObject(PlayersInfo playersInfo)
        {
            CoreGameCfg.FillObject(playersInfo.CoreGameCfg);
        }
    }
}
