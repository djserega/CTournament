using System;
using System.Collections.Generic;
using System.Text;

namespace CTournament.Models.CReplay
{
    public class RewardInfo
    {
        public Money.MoneyInfo Money { get; set; }
        public ShopEntriesInfo ShopEntries { get; set; }
        public int Xp { get; set; }
        public bool HasReward { get; set; }
    }
}
