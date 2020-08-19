using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.AccessControl;
using System.Text;
using System.Windows.Media.Imaging;

namespace CTournament.Models.CReplay.Players
{
    public class PlayersInfo
    {
        public int UserId { get; set; }
        public string NickName { get; set; }
        public int Role { get; set; }
        public string UserBar { get; set; }
        public string Visual { get; set; }
        public ConsumablesInfo Consumables { get; set; }
        public int TeamId { get; set; }
        public int Level { get; set; }
        public bool HasPremium { get; set; }
        public bool HasFamerBonus { get; set; }
        public int CardType { get; set; }
        public int NumOpenedModules { get; set; }
        public string LocalesKeys { get; set; }

        [NotMapped]
        public string RoleView { get; set; }
        [NotMapped]
        public PlayersDataInfo PlayersDataInfo { get; set; }
        [NotMapped]
        public string VisualNameRu { get; set; }

        public static void FillObject(PlayersInfo playersInfo)
        {
            playersInfo.RoleView = Enum.GetName(typeof(RoleOperatorsEnumRu), playersInfo.Role);
            playersInfo.VisualNameRu = DictionaryTemplates.GetNameOperator(playersInfo.Visual);
        }
    }
}
