using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CTournament.Models
{
    public class CalculationParameters : BindableBase
    {
        public CalculationParameters()
        {
        }
        public CalculationParameters(CReplay.ResultInfo resultInfo)
        {
            FillResultInfo(resultInfo);
        }

        public int AllKills { get; set; } = 0;
        public int PlayerKills { get; set; } = 0;
        public int BotKills { get; set; } = 0;
        public int Death { get; set; } = 0;
        public int Assists { get; set; } = 0;
        public int Damage { get; set; } = 0;
        public int Heal { get; set; } = 0;

        public int Time { get; set; } = 0;
        public int Minutes { get; set; } = 0;
        public int Seconds { get; set; } = 0;

        public void SetDefault()
        {
            Damage = 35452;
            Heal = 2273;
            Time = 899;
            Death = 3;
            Minutes = 14;
            Seconds = 59;
        }

        public void FillResultInfo(CReplay.ResultInfo resultInfo)
        {
            AllKills = resultInfo.PlayersData.Sum(item => item.AllKills);
            PlayerKills = resultInfo.PlayersData.Sum(item => item.PlayerKills);
            BotKills = resultInfo.PlayersData.Sum(item => item.BotKills);
            Death = resultInfo.PlayersData.Sum(item => item.Deaths);
            Assists = resultInfo.PlayersData.Sum(item => item.Assists);
            Damage = resultInfo.PlayersData.Sum(item => item.DamageDealt);
            Heal = resultInfo.PlayersData.Sum(item => item.HitPointHealed);

            Minutes = resultInfo.MatchTimeView.Minute;
            Seconds = resultInfo.MatchTimeView.Second;
            Time = Minutes * 60 + Seconds;
        }
    }
}
