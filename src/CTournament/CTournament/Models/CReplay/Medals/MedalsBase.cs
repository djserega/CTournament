using System;
using System.Collections.Generic;
using System.Text;

namespace CTournament.Models.CReplay.Medals
{
    public class MedalsBase
    {
        public string Visual { get; set; }
        public bool Enabled { get; set; }
        public int RibbonsCount { get; set; }
        public RewardInfo Reward { get; set; }
        public RewardInfo PremiumReward { get; set; }
    }
}
