using System;
using System.Collections.Generic;
using System.Text;

namespace CTournament.Models.CReplay.Players
{
    public class PlayerItemsUpgrades
    {
        public string Id { get; set; }
        public string Index { get; set; }
        public string XpCost { get; set; }
        public Money.MoneyCost Cost { get; set; }
        public string Visual { get; set; }
        public string ModuleType { get; set; }
        public bool WorkInProgress { get; set; }
        public bool Uninstallable { get; set; }
        public bool UninstallWhenDie { get; set; }
        public string Type { get; set; }
        public string SoundUpgrade { get; set; }
        public string AttachmentId { get; set; }
        public string UnlockableSlot { get; set; }
        public bool Unlocked { get; set; }
        public bool Enabled { get; set; }
    }
}
