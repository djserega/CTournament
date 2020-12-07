using System;
using System.Collections.Generic;
using System.Text;

namespace CTournament.Models.CReplay.Players
{
    public class CoreGameCfg
    {
        public double Rating { get; set; }
        public ClientCard ClientCard { get; set; }
        public GameData GameData { get; set; }
        public static void FillObject(CoreGameCfg coreGameCfg)
        {
            ClientCard.FillObject(coreGameCfg.ClientCard);
        }

    }
}
