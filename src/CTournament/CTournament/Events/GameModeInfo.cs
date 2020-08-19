using System;
using System.Collections.Generic;
using System.Text;

namespace CTournament.Events
{
    internal class GameModeInfo
    {
        internal static event EventHandler<Models.CReplay.GameModeInfo> Updater;
        internal static void Updating(Models.CReplay.GameModeInfo gameModeInfo)
        {
            Updater?.Invoke(null, gameModeInfo);
        }
    }
}
