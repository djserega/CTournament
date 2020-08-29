using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CTournament.Statistics
{
    internal class Statistic
    {
        #region Fields_dict

        #region PlayerKills

        private readonly Dictionary<string, int> _dictPlayerKillsUser = new Dictionary<string, int>();
        private readonly Dictionary<string, int> _dictPlayerKillsOperators = new Dictionary<string, int>();

        private readonly Dictionary<string, int> _dictSumPlayerKillsUser = new Dictionary<string, int>();
        private readonly Dictionary<string, int> _dictSumPlayerKillsOperators = new Dictionary<string, int>();

        #endregion

        #region BotKills

        private readonly Dictionary<string, int> _dictBotKillsUser = new Dictionary<string, int>();
        private readonly Dictionary<string, int> _dictBotKillsOperators = new Dictionary<string, int>();

        private readonly Dictionary<string, int> _dictSumBotKillsUser = new Dictionary<string, int>();
        private readonly Dictionary<string, int> _dictSumBotKillsOperators = new Dictionary<string, int>();

        #endregion

        #region Death

        private readonly Dictionary<string, int> _dictDeathUser = new Dictionary<string, int>();
        private readonly Dictionary<string, int> _dictDeathOperators = new Dictionary<string, int>();

        private readonly Dictionary<string, int> _dictSumDeathUser = new Dictionary<string, int>();
        private readonly Dictionary<string, int> _dictSumDeathOperators = new Dictionary<string, int>();

        #endregion

        #region Assist

        private readonly Dictionary<string, int> _dictAssistUser = new Dictionary<string, int>();
        private readonly Dictionary<string, int> _dictAssistOperators = new Dictionary<string, int>();

        private readonly Dictionary<string, int> _dictSumAssistUser = new Dictionary<string, int>();
        private readonly Dictionary<string, int> _dictSumAssistOperators = new Dictionary<string, int>();

        #endregion

        #region Damage

        private readonly Dictionary<string, int> _dictDamageDealtUser = new Dictionary<string, int>();
        private readonly Dictionary<string, int> _dictDamageDealtOperators = new Dictionary<string, int>();

        private readonly Dictionary<string, int> _dictSumDamageDealtUser = new Dictionary<string, int>();
        private readonly Dictionary<string, int> _dictSumDamageDealtOperators = new Dictionary<string, int>();

        #endregion

        #region Healed

        private readonly Dictionary<string, int> _dictHealedUser = new Dictionary<string, int>();
        private readonly Dictionary<string, int> _dictHealedOperators = new Dictionary<string, int>();

        private readonly Dictionary<string, int> _dictSumHealedUser = new Dictionary<string, int>();
        private readonly Dictionary<string, int> _dictSumHealedOperators = new Dictionary<string, int>();

        #endregion

        #endregion

        internal void Init()
        {
            _dictPlayerKillsUser.Clear();
            _dictPlayerKillsOperators.Clear();
            _dictSumPlayerKillsUser.Clear();
            _dictSumPlayerKillsOperators.Clear();

            _dictBotKillsUser.Clear();
            _dictBotKillsOperators.Clear();
            _dictSumBotKillsUser.Clear();
            _dictSumBotKillsOperators.Clear();

            _dictDeathUser.Clear();
            _dictDeathOperators.Clear();
            _dictSumDeathUser.Clear();
            _dictSumDeathOperators.Clear();

            _dictAssistUser.Clear();
            _dictAssistOperators.Clear();
            _dictSumAssistUser.Clear();
            _dictSumAssistOperators.Clear();

            _dictDamageDealtUser.Clear();
            _dictDamageDealtOperators.Clear();
            _dictSumDamageDealtUser.Clear();
            _dictSumDamageDealtOperators.Clear();

            _dictHealedUser.Clear();
            _dictHealedOperators.Clear();
            _dictSumHealedUser.Clear();
            _dictSumHealedOperators.Clear();
        }

        internal void FillDataReplays(List<Models.TournamentReplay> replays)
        {
            foreach (Models.TournamentReplay replay in replays)
            {
                foreach (Models.CReplay.Players.PlayersInfo itemInfo in replay.PlayersData)
                {
                    AddData(itemInfo);
                }
            }
        }

        internal void AddData(Models.CReplay.Players.PlayersInfo playersInfo)
        {
            #region PlayerKills

            AddDataToDictionary(_dictPlayerKillsUser, playersInfo.NickName, playersInfo.PlayersDataInfo.PlayerKills);
            AddDataToDictionary(_dictPlayerKillsOperators, playersInfo.VisualNameRu, playersInfo.PlayersDataInfo.PlayerKills);

            AddDataToDictionary(_dictSumPlayerKillsUser, playersInfo.NickName, playersInfo.PlayersDataInfo.PlayerKills, true);
            AddDataToDictionary(_dictSumPlayerKillsOperators, playersInfo.VisualNameRu, playersInfo.PlayersDataInfo.PlayerKills, true);

            #endregion

            #region BotKills

            AddDataToDictionary(_dictBotKillsUser, playersInfo.NickName, playersInfo.PlayersDataInfo.BotKills);
            AddDataToDictionary(_dictBotKillsOperators, playersInfo.VisualNameRu, playersInfo.PlayersDataInfo.BotKills);

            AddDataToDictionary(_dictSumBotKillsUser, playersInfo.NickName, playersInfo.PlayersDataInfo.BotKills, true);
            AddDataToDictionary(_dictSumBotKillsOperators, playersInfo.VisualNameRu, playersInfo.PlayersDataInfo.BotKills, true);

            #endregion

            #region Death

            AddDataToDictionary(_dictDeathUser, playersInfo.NickName, playersInfo.PlayersDataInfo.Deaths);
            AddDataToDictionary(_dictDeathOperators, playersInfo.VisualNameRu, playersInfo.PlayersDataInfo.Deaths);

            AddDataToDictionary(_dictSumDeathUser, playersInfo.NickName, playersInfo.PlayersDataInfo.Deaths, true);
            AddDataToDictionary(_dictSumDeathOperators, playersInfo.VisualNameRu, playersInfo.PlayersDataInfo.Deaths, true);

            #endregion

            #region Assist

            AddDataToDictionary(_dictAssistUser, playersInfo.NickName, playersInfo.PlayersDataInfo.Assists);
            AddDataToDictionary(_dictAssistOperators, playersInfo.VisualNameRu, playersInfo.PlayersDataInfo.Assists);

            AddDataToDictionary(_dictSumAssistUser, playersInfo.NickName, playersInfo.PlayersDataInfo.Assists, true);
            AddDataToDictionary(_dictSumAssistOperators, playersInfo.VisualNameRu, playersInfo.PlayersDataInfo.Assists, true);

            #endregion

            #region DamageDealt

            AddDataToDictionary(_dictDamageDealtUser, playersInfo.NickName, playersInfo.PlayersDataInfo.DamageDealt);
            AddDataToDictionary(_dictDamageDealtOperators, playersInfo.VisualNameRu, playersInfo.PlayersDataInfo.DamageDealt);

            AddDataToDictionary(_dictSumDamageDealtUser, playersInfo.NickName, playersInfo.PlayersDataInfo.DamageDealt, true);
            AddDataToDictionary(_dictSumDamageDealtOperators, playersInfo.VisualNameRu, playersInfo.PlayersDataInfo.DamageDealt, true);

            #endregion

            #region Healed

            AddDataToDictionary(_dictHealedUser, playersInfo.NickName, playersInfo.PlayersDataInfo.HitPointHealed);
            AddDataToDictionary(_dictHealedOperators, playersInfo.VisualNameRu, playersInfo.PlayersDataInfo.HitPointHealed);

            AddDataToDictionary(_dictSumHealedUser, playersInfo.NickName, playersInfo.PlayersDataInfo.HitPointHealed, true);
            AddDataToDictionary(_dictSumHealedOperators, playersInfo.VisualNameRu, playersInfo.PlayersDataInfo.HitPointHealed, true);

            #endregion

        }

        internal List<Models.ItemUserStatisticsItemUpdater> GetAllStatistics()
        {
            List<Models.ItemUserStatisticsItemUpdater> userStatistics = new List<Models.ItemUserStatisticsItemUpdater>
                {
                    // new ItemUserStatisticsItemUpdater("", () => _statistic.()),

                    new Models.ItemUserStatisticsItemUpdater("TopPlayerKillsUsers",     () => GetTopPlayerKillsUsers()),
                    new Models.ItemUserStatisticsItemUpdater("TopPlayerKillsOperators", () => GetTopPlayerKillsOperators()),
                    new Models.ItemUserStatisticsItemUpdater("SumPlayerKillsUsers",     () => GetSumPlayerKillsUsers()),
                    new Models.ItemUserStatisticsItemUpdater("SumPlayerKillsOperators", () => GetSumPlayerKillsOperators()),

                    new Models.ItemUserStatisticsItemUpdater("TopBotKillsUsers",        () => GetTopBotKillsUsers()),
                    new Models.ItemUserStatisticsItemUpdater("TopBotKillsOperators",    () => GetTopBotKillsOperators()),
                    new Models.ItemUserStatisticsItemUpdater("SumBotKillsUsers",        () => GetSumBotKillsUsers()),
                    new Models.ItemUserStatisticsItemUpdater("SumBotKillsOperators",    () => GetSumBotKillsOperators()),

                    new Models.ItemUserStatisticsItemUpdater("TopDeathUsers",           () => GetTopDeathUsers()),
                    new Models.ItemUserStatisticsItemUpdater("TopDeathOperators",       () => GetTopDeathOperators()),
                    new Models.ItemUserStatisticsItemUpdater("SumDeathUsers",           () => GetSumDeathUsers()),
                    new Models.ItemUserStatisticsItemUpdater("SumDeathOperators",       () => GetSumDeathOperators()),

                    new Models.ItemUserStatisticsItemUpdater("TopAssistUsers",          () => GetTopAssistUsers()),
                    new Models.ItemUserStatisticsItemUpdater("TopAssistOperators",      () => GetTopAssistOperators()),
                    new Models.ItemUserStatisticsItemUpdater("SumAssistUsers",          () => GetSumAssistUsers()),
                    new Models.ItemUserStatisticsItemUpdater("SumAssistOperators",      () => GetSumAssistOperators()),

                    new Models.ItemUserStatisticsItemUpdater("TopDamageDealtUsers",     () => GetTopDamageDealtUsers()),
                    new Models.ItemUserStatisticsItemUpdater("TopDamageDealtOperators", () => GetTopDamageDealtOperators()),
                    new Models.ItemUserStatisticsItemUpdater("SumDamageDealtUsers",     () => GetSumDamageDealtUsers()),
                    new Models.ItemUserStatisticsItemUpdater("SumDamageDealtOperators", () => GetSumDamageDealtOperators()),

                    new Models.ItemUserStatisticsItemUpdater("TopHealedUsers",          () => GetTopHealedUsers()),
                    new Models.ItemUserStatisticsItemUpdater("TopHealedOperators",      () => GetTopHealedOperators()),
                    new Models.ItemUserStatisticsItemUpdater("SumHealedUsers",          () => GetSumHealedUsers()),
                    new Models.ItemUserStatisticsItemUpdater("SumHealedOperators",      () => GetSumHealedOperators()),
                };

            return userStatistics;
        }

        private void AddDataToDictionary(Dictionary<string, int> dict, string key, int value, bool sum = false)
        {
            if (value > 0)
            {
                if (dict.ContainsKey(key))
                {
                    if (sum)
                        dict[key] += value;
                    else
                    {
                        if (dict[key] < value)
                            dict[key] = value;
                    }
                }
                else
                    dict.Add(key, value);
            }
        }

        #region PlayerKills

        internal List<Models.UserStatisticsItemValueItem> GetTopPlayerKillsUsers() => GetTopDictValue(_dictPlayerKillsUser);
        internal List<Models.UserStatisticsItemValueItem> GetTopPlayerKillsOperators() => GetTopDictValue(_dictPlayerKillsOperators);

        internal List<Models.UserStatisticsItemValueItem> GetSumPlayerKillsUsers() => GetTopDictValue(_dictSumPlayerKillsUser);
        internal List<Models.UserStatisticsItemValueItem> GetSumPlayerKillsOperators() => GetTopDictValue(_dictSumPlayerKillsOperators);

        #endregion

        #region BotKills

        internal List<Models.UserStatisticsItemValueItem> GetTopBotKillsUsers() => GetTopDictValue(_dictBotKillsUser);
        internal List<Models.UserStatisticsItemValueItem> GetTopBotKillsOperators() => GetTopDictValue(_dictBotKillsOperators);

        internal List<Models.UserStatisticsItemValueItem> GetSumBotKillsUsers() => GetTopDictValue(_dictSumBotKillsUser);
        internal List<Models.UserStatisticsItemValueItem> GetSumBotKillsOperators() => GetTopDictValue(_dictSumBotKillsOperators);

        #endregion

        #region Death

        internal List<Models.UserStatisticsItemValueItem> GetTopDeathUsers() => GetTopDictValue(_dictDeathUser);
        internal List<Models.UserStatisticsItemValueItem> GetTopDeathOperators() => GetTopDictValue(_dictDeathOperators);

        internal List<Models.UserStatisticsItemValueItem> GetSumDeathUsers() => GetTopDictValue(_dictSumDeathUser);
        internal List<Models.UserStatisticsItemValueItem> GetSumDeathOperators() => GetTopDictValue(_dictSumDeathOperators);

        #endregion

        #region Assist

        internal List<Models.UserStatisticsItemValueItem> GetTopAssistUsers() => GetTopDictValue(_dictAssistUser);
        internal List<Models.UserStatisticsItemValueItem> GetTopAssistOperators() => GetTopDictValue(_dictAssistOperators);

        internal List<Models.UserStatisticsItemValueItem> GetSumAssistUsers() => GetTopDictValue(_dictSumAssistUser);
        internal List<Models.UserStatisticsItemValueItem> GetSumAssistOperators() => GetTopDictValue(_dictSumAssistOperators);

        #endregion

        #region DamageDealt

        internal List<Models.UserStatisticsItemValueItem> GetTopDamageDealtUsers() => GetTopDictValue(_dictDamageDealtUser);
        internal List<Models.UserStatisticsItemValueItem> GetTopDamageDealtOperators() => GetTopDictValue(_dictDamageDealtOperators);

        internal List<Models.UserStatisticsItemValueItem> GetSumDamageDealtUsers() => GetTopDictValue(_dictSumDamageDealtUser);
        internal List<Models.UserStatisticsItemValueItem> GetSumDamageDealtOperators() => GetTopDictValue(_dictSumDamageDealtOperators);

        #endregion

        #region Healed

        internal List<Models.UserStatisticsItemValueItem> GetTopHealedUsers() => GetTopDictValue(_dictHealedUser);
        internal List<Models.UserStatisticsItemValueItem> GetTopHealedOperators() => GetTopDictValue(_dictHealedOperators);

        internal List<Models.UserStatisticsItemValueItem> GetSumHealedUsers() => GetTopDictValue(_dictSumHealedUser);
        internal List<Models.UserStatisticsItemValueItem> GetSumHealedOperators() => GetTopDictValue(_dictSumHealedOperators);

        #endregion

        private List<Models.UserStatisticsItemValueItem> GetTopDictValue(Dictionary<string, int> dict, int count = 3)
        {
            SortedDictionary<double, string> sortedValue = new SortedDictionary<double, string>();
            foreach (KeyValuePair<string, int> itemValue in dict)
            {
                double newValue = itemValue.Value + 0.0999;

                while (sortedValue.ContainsKey(newValue))
                    newValue -= 0.0001;

                sortedValue.Add(newValue, itemValue.Key);
            }

            List<Models.UserStatisticsItemValueItem> valueItems = new List<Models.UserStatisticsItemValueItem>();
            int i = 0;
            foreach (KeyValuePair<double, string> sortedItem in sortedValue.Reverse())
            {
                valueItems.Add(new Models.UserStatisticsItemValueItem(
                    sortedItem.Value,
                    Convert.ToInt32(sortedItem.Key)));

                if (i++ >= count - 1)
                    break;
            }

            return valueItems;
        }
    }
}
