using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Windows.Media;

namespace CTournament.Models.CReplay
{
    public class ResultInfo
    {
        private int winnerTeamNumber;

        public Players.PlayersDataInfo[] PlayersData { get; set; }
        public int WinnerTeamNumber { get => winnerTeamNumber; set { winnerTeamNumber = value; if (value == 1) WinnerTeamNumberView = 5; else WinnerTeamNumberView = value; } }
        public string MatchTime { get; set; }
        public MissionInfo MissionInfo { get; set; }
        public Medals.MedalsInfo MedalsInfo { get; set; }
        public RewardInfo MatchReward { get; set; }
        public string BattlePassId { get; set; }
        public string MatchId { get; set; }

        [NotMapped]
        public DateTime MatchTimeView { get; set; }
        [NotMapped]
        public string MatchTimeViewTime { get; set; }
        [NotMapped]
        public bool Victory { get; set; }
        [NotMapped]
        public string VictoryOrDefeat { get => Victory ? "Победа" : "Поражение"; }
        [NotMapped]
        public string NicknameCurrentUser { get; set; }
        [NotMapped]
        public Brush BrushVictoryOrDefeat { get; set; } = DefaultValues.BrushTie;
        [NotMapped]
        public int WinnerTeamNumberView { get; set; }

        internal static void FillObject(ResultInfo resultInfo)
        {
            if (DateTime.TryParse(resultInfo.MatchTime, out DateTime MatchTimeParse))
            {
                resultInfo.MatchTimeView = MatchTimeParse;
                resultInfo.MatchTimeViewTime = DefaultValues.GetTimeFormat(resultInfo.MatchTimeView.TimeOfDay);
            }

            int teamCurrentUser = 0;
            foreach (Players.PlayersDataInfo itemPlayer in resultInfo.PlayersData)
            {
                if (itemPlayer.IsCurrentUser)
                {
                    teamCurrentUser = itemPlayer.TeamNumber;
                    resultInfo.NicknameCurrentUser = itemPlayer.Nickname;
                }
            }

            resultInfo.Victory = resultInfo.WinnerTeamNumber == teamCurrentUser;

            if (resultInfo.Victory)
                resultInfo.BrushVictoryOrDefeat = DefaultValues.BrushVictory;
            else
                resultInfo.BrushVictoryOrDefeat = DefaultValues.BrushDefeat;

            MissionInfo.FillObject(resultInfo.MissionInfo);
        }
    }
}
