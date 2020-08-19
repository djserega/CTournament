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

        public int Damage { get; set; } = 0;
        public int Heal { get; set; } = 0;
        public int Time { get; set; } = 0;
        public int Death { get; set; } = 0;
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
            Damage = resultInfo.PlayersData.Sum(item => item.DamageDealt);
            Heal = resultInfo.PlayersData.Sum(item => item.HitPointHealed);
            Death = resultInfo.PlayersData.Sum(item => item.Deaths);
            Minutes = resultInfo.MatchTimeView.Minute;
            Seconds = resultInfo.MatchTimeView.Second;
            Time = Minutes * 60 + Seconds;
        }
    }
}
