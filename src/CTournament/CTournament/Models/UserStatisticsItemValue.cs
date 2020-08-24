using System;
using System.Collections.Generic;
using System.Text;

namespace CTournament.Models
{
    public class UserStatisticsItemValue
    {
        public UserStatisticsItemValue(int id,
                                       List<UserStatisticsItemValueItem> valueItems)
        {
            Id = id;
            //Header = header;

            int i = 0;
            foreach (UserStatisticsItemValueItem itemValue in valueItems)
            {
                i++;

                if (i == 1)
                {
                    //Value1 = itemValue;
                    Header = itemValue;
                }
                else if (i == 2)
                    Value1 = itemValue;
                else if (i == 3)
                    Value2 = itemValue;
            }
        }

        public int Id { get; set; }

        public UserStatisticsItemValueItem Header { get; set; }

        public UserStatisticsItemValueItem Value1 { get; set; }
        public UserStatisticsItemValueItem Value2 { get; set; }
        public UserStatisticsItemValueItem Value3 { get; set; }
    }
}
