using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CTournament.SaveToExcel
{
    internal class PersonalStatistics
    {
        private AreaPersonalStatistics[] _areas;

        public PersonalStatistics()
        {
            InitAreas();
        }

        internal List<Models.TournamentReplay> TournamentReplays { get; set; }

        internal void SavePersonalStatistics(XLWorkbook workbook)
        {
            Statistics.Statistic statistic = new Statistics.Statistic();
            statistic.Init();
            statistic.FillDataReplays(TournamentReplays);


            List<Models.ItemUserStatisticsItemUpdater> itemUserStatistics = statistic.GetAllStatistics();

            IXLWorksheet worksheet = workbook.Worksheets.Add("Личная статистика");

            foreach (AreaPersonalStatistics itemArea in _areas.Where(f => f.Name == null))
            {
                worksheet.Cell(itemArea.RowId, itemArea.ColumnId).Value = itemArea.Description;
                MainMethods.SetStyleHeader(worksheet.Cell(itemArea.RowId, itemArea.ColumnId));
            }

            foreach (Models.ItemUserStatisticsItemUpdater itemStatistic in itemUserStatistics)
            {
                AreaPersonalStatistics areaStatistics = _areas.FirstOrDefault(f => f.Name == itemStatistic.Key);

                if (areaStatistics == null)
                    continue;

                int rowID = areaStatistics.RowId;

                IXLRange header = worksheet.Range(rowID, areaStatistics.ColumnId + 1, rowID, areaStatistics.ColumnId).Merge();

                MainMethods.SetStyleHeader(header);

                worksheet.Cell(rowID, areaStatistics.ColumnId).Value = areaStatistics.Description;

                rowID++;

                List<Models.UserStatisticsItemValueItem> userStatistics = itemStatistic.GetValueAction();
                foreach (Models.UserStatisticsItemValueItem itemUser in userStatistics)
                {
                    worksheet.Cell(rowID, areaStatistics.ColumnId).Value = itemUser.Header;
                    worksheet.Cell(rowID++, areaStatistics.ColumnId + 1).Value = itemUser.Value;
                }

                worksheet.Columns(_areas[0].ColumnId, _areas.Last().ColumnId + 1).AdjustToContents();
            }
        }

        private void InitAreas()
        {
            _areas = new AreaPersonalStatistics[30];

            _areas[0] = new AreaPersonalStatistics(2, 2, null, "Ликвидировано оперативников");
            _areas[1] = new AreaPersonalStatistics(4, 2, "TopPlayerKillsUsers", "По игрокам");
            _areas[2] = new AreaPersonalStatistics(9, 2, "TopPlayerKillsOperators", "По оперативникам");
            _areas[3] = new AreaPersonalStatistics(14, 2, "SumPlayerKillsUsers", "Суммарно по игрокам");
            _areas[4] = new AreaPersonalStatistics(19, 2, "SumPlayerKillsOperators", "Суммарно по оперативникам");

            _areas[5] = new AreaPersonalStatistics(2, 5, null, "Ликвидировано ботов");
            _areas[6] = new AreaPersonalStatistics(4, 5, "TopBotKillsUsers", "По игрокам");
            _areas[7] = new AreaPersonalStatistics(9, 5, "TopBotKillsOperators", "По оперативникам");
            _areas[8] = new AreaPersonalStatistics(14, 5, "SumBotKillsUsers", "Суммарно по игрокам");
            _areas[9] = new AreaPersonalStatistics(19, 5, "SumBotKillsOperators", "Суммарно по оперативникам");

            _areas[10] = new AreaPersonalStatistics(2, 8, null, "Погиб");
            _areas[11] = new AreaPersonalStatistics(4, 8, "TopDeathUsers", "По игрокам");
            _areas[12] = new AreaPersonalStatistics(9, 8, "TopDeathOperators", "По оперативникам");
            _areas[13] = new AreaPersonalStatistics(14, 8, "SumDeathUsers", "Суммарно по игрокам");
            _areas[14] = new AreaPersonalStatistics(19, 8, "SumDeathOperators", "Суммарно по оперативникам");

            _areas[15] = new AreaPersonalStatistics(2, 11, null, "Содействия");
            _areas[16] = new AreaPersonalStatistics(4, 11, "TopAssistUsers", "По игрокам");
            _areas[17] = new AreaPersonalStatistics(9, 11, "TopAssistOperators", "По оперативникам");
            _areas[18] = new AreaPersonalStatistics(14, 11, "SumAssistUsers", "Суммарно по игрокам");
            _areas[19] = new AreaPersonalStatistics(19, 11, "SumAssistOperators", "Суммарно по оперативникам");

            _areas[20] = new AreaPersonalStatistics(2, 14, null, "Урон");
            _areas[21] = new AreaPersonalStatistics(4, 14, "TopDamageDealtUsers", "По игрокам");
            _areas[22] = new AreaPersonalStatistics(9, 14, "TopDamageDealtOperators", "По оперативникам");
            _areas[23] = new AreaPersonalStatistics(14, 14, "SumDamageDealtUsers", "Суммарно по игрокам");
            _areas[24] = new AreaPersonalStatistics(19, 14, "SumDamageDealtOperators", "Суммарно по оперативникам");

            _areas[25] = new AreaPersonalStatistics(2, 17, null, "Лечение");
            _areas[26] = new AreaPersonalStatistics(4, 17, "TopHealedUsers", "По игрокам");
            _areas[27] = new AreaPersonalStatistics(9, 17, "TopHealedOperators", "По оперативникам");
            _areas[28] = new AreaPersonalStatistics(14, 17, "SumHealedUsers", "Суммарно по игрокам");
            _areas[29] = new AreaPersonalStatistics(19, 17, "SumHealedOperators", "Суммарно по оперативникам");
        }

    }
}
