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

        public int PlayedRoundsCount { get; set; } = 1;

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
            switch (DefaultValues.RateFormulaVariant)
            {
                case RateFormulaVariants.speedRun:
                    PlayedRoundsCount = 3;
                    Damage = 35452;
                    Heal = 2273;
                    Time = 899;
                    Death = 3;
                    Minutes = 14;
                    Seconds = 59;
                    break;
                case RateFormulaVariants.damagePerRound:
                    PlayedRoundsCount = 3;
                    Damage = 3000;
                    Heal = 0;
                    Time = 0;
                    Death = 0;
                    Minutes = 0;
                    Seconds = 0;
                    break;
            }
        }

        public void FillResultInfo(CReplay.ResultInfo resultInfo)
        {
            PlayedRoundsCount = resultInfo.PlayedRoundsCount;

            switch (DefaultValues.RateFormulaVariant)
            {
                case RateFormulaVariants.speedRun:
                    AllKills = resultInfo.PlayersData.Sum(item => item.AllKills);
                    PlayerKills = resultInfo.PlayersData.Sum(item => item.PlayerKills);
                    BotKills = resultInfo.PlayersData.Sum(item => item.BotKills);
                    Death = resultInfo.PlayersData.Sum(item => item.Deaths);
                    Assists = resultInfo.PlayersData.Sum(item => item.Assists);
                    Damage = resultInfo.PlayersData.Sum(item => item.DamageDealt);
                    Heal = resultInfo.PlayersData.Sum(item => item.HitPointHealed);
                    break;
                case RateFormulaVariants.damagePerRound:
                    //           (F      - PRC              ) * 100
                    AllKills = resultInfo.PlayersData.Where(el => el.IsCurrentUser).Sum(item => item.AllKills);
                    PlayerKills = resultInfo.PlayersData.Where(el => el.IsCurrentUser).Sum(item => item.PlayerKills);
                    BotKills = resultInfo.PlayersData.Where(el => el.IsCurrentUser).Sum(item => item.BotKills);
                    Death = resultInfo.PlayersData.Where(el => el.IsCurrentUser).Sum(item => item.Deaths);
                    Assists = resultInfo.PlayersData.Where(el => el.IsCurrentUser).Sum(item => item.Assists);
                    Damage = resultInfo.PlayersData.Where(el => el.IsCurrentUser).Sum(item => item.DamageDealt);
                    Heal = resultInfo.PlayersData.Where(el => el.IsCurrentUser).Sum(item => item.HitPointHealed);
                    break;
                default:
                    break;
            }

            Minutes = resultInfo.MatchTimeView.Minute;
            Seconds = resultInfo.MatchTimeView.Second;
            Time = Minutes * 60 + Seconds;
        }
    }
}
