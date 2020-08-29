using System;
using System.Collections.Generic;
using System.Text;

namespace CTournament.Models
{
    public class ItemUserStatisticsItemUpdater
    {
        public ItemUserStatisticsItemUpdater(string key, Func<List<UserStatisticsItemValueItem>> getValueAction)
        {
            Key = key;
            GetValueAction = getValueAction;
        }

        public int Id { get; set; }
        public string Key { get; set; }
        public Func<List<UserStatisticsItemValueItem>> GetValueAction { get; set; }
    }
}
