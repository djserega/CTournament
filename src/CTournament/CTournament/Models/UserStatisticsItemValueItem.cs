using System;
using System.Collections.Generic;
using System.Text;

namespace CTournament.Models
{
    public class UserStatisticsItemValueItem
    {
        public UserStatisticsItemValueItem(string header, int value)
        {
            Header = header;
            Value = value;
        }

        public string Header { get; set; }
        public int Value { get; set; }
    }
}
