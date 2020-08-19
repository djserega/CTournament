using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media;

namespace CTournament
{
    internal static class DefaultValues
    {
        public static Brush BrushVictory { get; } = Brushes.DeepSkyBlue;
        public static Brush BrushDefeat { get; } = Brushes.Red;
        public static Brush BrushTie { get; } = Brushes.Black;



        static readonly string[] _hours = { "час", "часа", "часов" };
        static readonly string[] _minutes = { "минута", "минуты", "минут" };
        static readonly string[] _seconds = { "секунда", "секунды", "секунд" };

        internal static string GetTimeFormat(TimeSpan time)
        {
            string hh = GetIdenticalLengthString(GetCaseHoursMinutesSeconds(time.Hours, _hours), 6);
            string mm = GetIdenticalLengthString(GetCaseHoursMinutesSeconds(time.Minutes, _minutes), 8);
            string ss = GetIdenticalLengthString(GetCaseHoursMinutesSeconds(time.Seconds, _seconds), 10);

            if (time.Hours > 0)
                return string.Format("{0:hh} {1} {0:mm} {2} {0:ss} {3}", time, hh, mm, ss);
            else if (time.Minutes > 0)
                return string.Format("{0:mm} {2} {0:ss} {3}", time, hh, mm, ss);
            else if (time.Seconds > 0)
                return string.Format("{0:ss} {3}", time, hh, mm, ss);
            else
                return string.Empty;
        }

        private static string GetIdenticalLengthString(string value, int countEpmtyChar)
        {
            string postfix = "";
            for (int i = 0; i < countEpmtyChar; i++)
                postfix += " ";

            return (value + postfix).Substring(0, countEpmtyChar);
        }

        private static string GetCaseHoursMinutesSeconds(int value, string[] options)
        {
            value = Math.Abs(value) % 100;

            if (value > 10 && value < 15)
                return options[2];

            value %= 10;
            if (value == 1)
                return options[0];

            if (value > 1 && value < 5)
                return options[1];

            return options[2];
        }
    }
}
