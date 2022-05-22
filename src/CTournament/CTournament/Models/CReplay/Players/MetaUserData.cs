using System.Collections.Generic;

namespace CTournament.Models.CReplay.Players
{
    public class MetaUserData
    {
        public string UserName { get; set; }
        public RoleOperatorsEnumEn Role { get; set; }
        public int UserId { get; set; }
        public int PartyId { get; set; }
        public int CardId { get; set; }
        public int Level { get; set; }
        public int TeamNumber { get; set; }
        public int ReportPushesRemain { get; set; }
        public string Collection { get; set; }
        public string PrimaryAbulity { get; set; }
        public QuartersTechnologies QuartersTechnologies { get; set; }
        public string UserBar { get; set; }
        public string CardPreset { get; set; }
        public bool HasPremium { get; set; }
        public bool HasFarmerBonus { get; set; }
        public bool AlreadyPlayed { get; set; }
        public ulong Access { get; set; }
        public List<Quest> QuestProgress { get; set; }
        public int MyProperty { get; set; }
        public int CurrentWeeklyQuestChapter { get; set; }
        public int TotalWeeklyQuestChapters { get; set; }
        public int RankedSeasonPoints { get; set; }
        public int RankedSeasonNumBattles { get; set; }
        public ConsumablesInfo SelectedConsumables { get; set; }
    }
}