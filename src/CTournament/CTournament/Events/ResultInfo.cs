using System;
using System.Collections.Generic;
using System.Text;

namespace CTournament.Events
{
    internal class ResultInfo
    {
        internal static event EventHandler<Models.CReplay.ResultInfo> Updater;
        internal static void Updating(Models.CReplay.ResultInfo resultInfoInfo)
        {
            Updater?.Invoke(null, resultInfoInfo);
        }

    }
}
