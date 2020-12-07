using System;
using System.Collections.Generic;
using System.Text;

namespace CTournament.Models.CReplay.Players
{
    public class PlayerItems
    {
        public string Slot { get; set; }
        public string CfgId { get; set; }
        public string SkinVisual { get; set; }
        public string ItemType { get; set; }
        public bool Unlocked { get; set; }
        public string Visual { get; set; }
        public int Id { get; set; }
        public PlayerItemsUpgrades[] Upgrades { get; set; }
    }
}
