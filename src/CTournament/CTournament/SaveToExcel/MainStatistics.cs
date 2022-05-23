using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CTournament.SaveToExcel
{
    internal class MainStatistics
    {
        private ColumnMainStatistics[] _columns;

        public MainStatistics()
        {
            InitColumns();
        }

        internal List<Models.TournamentReplay> TournamentReplays { get; set; }

        internal void SaveMainStatistics(XLWorkbook workbook)
        {
            IXLWorksheet worksheet = workbook.Worksheets.Add("Общая статистика");

            worksheet.Cell(1, 1).Value = "№";
            MainMethods.SetStyleHeader(worksheet.Cell(1, 1));

            foreach (ColumnMainStatistics itemColumns in _columns)
            {
                worksheet.Cell(1, itemColumns.Id + 1).Value = itemColumns.Description;
                MainMethods.SetStyleHeader(worksheet.Cell(1, itemColumns.Id + 1));
            }


            for (int i = 0; i < TournamentReplays.Count; i++)
            {
                worksheet.Cell(i + 2, 1).Value = (i + 1).ToString();

                foreach (ColumnMainStatistics itemColumns in _columns)
                {
                    worksheet.Cell(i + 2, itemColumns.Id + 1).Value = GetValueReplay(TournamentReplays[i], itemColumns);
                }
            }

            worksheet.Columns(_columns[0].Id + 1, _columns.Last().Id + 1).AdjustToContents();
        }

        private string GetValueReplay(Models.TournamentReplay replay, ColumnMainStatistics itemColumns)
        {
            string value = itemColumns.Name switch
            {
                "Capitan" => replay.Capitan,
                "Operator" => replay.Operator,
                "PlayedRoundsCount" => replay.PlayedRoundsCount.ToString(),
                "GameMode" => replay.GameMode,
                "IsRecruit" => BoolToYesNo(replay.IsRecruit),
                "MinLevel" => replay.MinLevelOperators.ToString(),
                "Mission" => replay.Mission,
                "Data.Damage" => replay.Data.Damage.ToString(),
                "Data.Heal" => replay.Data.Heal.ToString(),
                "Data.Death" => replay.Data.Death.ToString(),
                "MatchTimeAsTime" => replay.MatchTimeAsTime.ToString(),
                "Result" => replay.Result.ToString(),
                _ => "<empty>",
            };
            return value;
        }

        private static string BoolToYesNo(bool value) => value ? "Да" : "Нет";

        private void InitColumns()
        {
            _columns = new ColumnMainStatistics[10];
            _columns[0] = new ColumnMainStatistics(1, "Result", "Результат");
            _columns[1] = new ColumnMainStatistics(2, "GameMode", "Режим");
            _columns[2] = new ColumnMainStatistics(3, "Capitan", "Капитан");
            _columns[3] = new ColumnMainStatistics(4, "Operator", "Оперативник");
            _columns[4] = new ColumnMainStatistics(5, "Mission", "Миссия");
            _columns[5] = new ColumnMainStatistics(6, "PlayedRoundsCount", "Раундов");
            _columns[6] = new ColumnMainStatistics(7, "Data.Damage", "Урон");
            _columns[7] = new ColumnMainStatistics(8, "Data.Heal", "Лечение");
            _columns[8] = new ColumnMainStatistics(9, "Data.Death", "Погиб");
            _columns[9] = new ColumnMainStatistics(10, "MatchTimeAsTime", "Время боя");
        }
    }
}
