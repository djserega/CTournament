using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CTournament.Models.CReplay
{
    public class MissionInfo
    {
        public string MapName { get; set; }
        public string MissionName { get; set; }
        public string Type { get; set; }
        public object Route { get; set; }
        public int Difficutly { get; set; }

        [NotMapped]
        public string MapNameView { get; set; }
        [NotMapped]
        public string MissionNameView { get; set; }
        [NotMapped]
        public string TypeView { get; set; }

        internal static void FillObject(MissionInfo missionInfo)
        {
            missionInfo.MapNameView = DictionaryTemplates.GetNameMap(missionInfo.MapName);
            missionInfo.MissionNameView = DictionaryTemplates.GetNameMission(missionInfo.MissionName);
            missionInfo.TypeView = DictionaryTemplates.GetTypeMission(missionInfo.Type);
        }
    }
}
