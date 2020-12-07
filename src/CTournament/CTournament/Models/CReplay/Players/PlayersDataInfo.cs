using System.Collections.Generic;

namespace CTournament.Models.CReplay.Players
{
    public class PlayersDataInfo
    {
        public int GameId { get; set; }
        public long MetaId { get; set; }
        public int Level { get; set; }
        public bool HasPremium { get; set; }
        public int Role { get; set; }
        public ConsumablesInfo SelectedConsumables { get; set; }
        public bool IsCurrentUser { get; set; }
        public string Userbar { get; set; }
        public int TeamNumber { get; set; }
        public string Nickname { get; set; }
        public int Kills { get; set; }
        public int PlayerKills { get; set; }
        public int BotKills { get; set; }
        public int Deaths { get; set; }
        public int Assists { get; set; }
        public int HitPointHealed { get; set; }
        public int DamageDealt { get; set; }
        public int DamageRecieved { get; set; }

        public int AllKills { get => Kills + PlayerKills + BotKills; }

        public int SortedRole { get => GetKeyRole(Role); }

        private static int GetKeyRole(int idRole)
        {
            if (idRole == 1)
                idRole = 4;
            else if (idRole == 2)
                idRole = 3;
            else if (idRole == 3)
                idRole = 2;

            return idRole;
        }
    }
}
