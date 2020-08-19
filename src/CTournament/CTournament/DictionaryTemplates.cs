using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace CTournament
{
    internal static class DictionaryTemplates
    {
        private static readonly string _nameElemetnByDefault = "<not-found>";

        private static Dictionary<string, string> _dictNameMap;
        private static Dictionary<string, string> _dictNameMission;
        private static Dictionary<string, string> _dictTypesMission;
        private static Dictionary<string, string> _dictNameOperators;

        static DictionaryTemplates()
        {
            FillDictNameMap();
            FillDictNameMission();
            FillDictTypesMission();
            FillDictNameOperators();
        }

        internal static Dictionary<string, string> GetDictNameMission() => _dictNameMission;
        internal static Dictionary<string, string> GetDictTypeMission() => _dictTypesMission;
        internal static Dictionary<string, string> GetDictNameOperators() => _dictNameOperators;

        #region Get value

        internal static string GetNameMap(string key) => GetDictValue(_dictNameMap, key);

        internal static string GetNameMission(string key) => GetDictValue(_dictNameMission, key);

        internal static string GetTypeMission(string key) => GetDictValue(_dictTypesMission, key);

        internal static string GetNameOperator(string key) => GetDictValue(_dictNameOperators, key);

        #endregion

        internal static string GetDictValue(Dictionary<string, string> dict, string key)
        {
            string name = _nameElemetnByDefault;

#if DEBUG
            name += " (" + key + ")";
#endif

            if (dict.ContainsKey(key))
                name = dict[key];
            else{
#if DEBUG
                Events.Messages.SendMessage(null, name);
#endif
            }

            return name;
        }

        #region Fill dictionary

        private static void FillDictNameMap() => _dictNameMap = DeserializeJsonString(GetJsonTextResource("NameMaps.json"));

        private static void FillDictNameMission() => _dictNameMission = DeserializeJsonString(GetJsonTextResource("NameMissions.json"));

        private static void FillDictTypesMission() => _dictTypesMission = DeserializeJsonString(GetJsonTextResource("TypesMission.json"));

        private static void FillDictNameOperators() => _dictNameOperators = DeserializeJsonString(GetJsonTextResource("NameOperators.json"));

        #endregion

        private static Dictionary<string, string> DeserializeJsonString(string value)
            => JsonConvert.DeserializeObject<Dictionary<string, string>>(value);

        private static string GetJsonTextResource(string nameResource)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();

            string jsonTextNameMIssions = null; ;
            using (Stream stream = assembly.GetManifestResourceStream($"CTournament.Resources.{nameResource}"))
            {
                byte[] temp = new byte[stream.Length];
                stream.Read(temp, 0, temp.Length);

                jsonTextNameMIssions = Encoding.UTF8.GetString(temp);
            }

            return jsonTextNameMIssions;
        }

    }
}
