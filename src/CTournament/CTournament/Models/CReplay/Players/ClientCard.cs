using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CTournament.Models.CReplay.Players
{
    public class ClientCard
    {
        public string CfgId { get; set; }
        public int Id { get; set; }
        public string Collection { get; set; }
        public string Visual { get; set; }
        public RoleOperatorsEnumEn Role { get; set; }
        public string Ability { get; set; }
        public int CardType { get; set; }
        public int OwnedUnlocksCount { get; set; }
        public PlayerItems[] Items { get; set; }
        public int ItemsCount { get; set; }
        public int TotalExperience { get; set; }
        public bool Marked { get; set; }
        public int Experience { get; set; }
        public bool IsUpgradable { get; set; }
        public bool IsLocked { get; set; }


        [NotMapped]
        public string RoleView { get; set; }
        [NotMapped]
        public string VisualNameRu { get; set; }
        public static void FillObject(ClientCard clientCard)
        {
            clientCard.RoleView = Enum.GetName(typeof(RoleOperatorsEnumRu), clientCard.Role);
            clientCard.VisualNameRu = DictionaryTemplates.GetNameOperator(clientCard.Visual);
        }

    }
}
