using System;
using System.Collections.Generic;
using System.Text;

namespace CTournament.Events
{
    internal class UserStatisticsItemUpdater
    {
        internal static event EventHandler<Models.UserStatisticsItemValue> Updater;
        internal static void Update(Models.UserStatisticsItemValue value)
        {
            Updater?.Invoke(null, value);
        }
    }
}
