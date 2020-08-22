using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows;

namespace CTournament.SaveToExcel
{
    internal class Saving
    {
        private readonly Columns[] _columnsExcel;

        internal Saving()
        {
            _columnsExcel = new Columns[10];
            _columnsExcel[0] = new Columns() { Id = 1, Name = "GameMode", Description = "Режим" };
            _columnsExcel[1] = new Columns() { Id = 2, Name = "Capitan", Description = "Капитан" };
            _columnsExcel[2] = new Columns() { Id = 3, Name = "IsRecruit", Description = "Рекруты" };
            _columnsExcel[3] = new Columns() { Id = 4, Name = "MinLevel", Description = "Минимальный уровень" };
            _columnsExcel[4] = new Columns() { Id = 5, Name = "Mission", Description = "Миссия" };
            _columnsExcel[5] = new Columns() { Id = 6, Name = "Data.Damage", Description = "Урон" };
            _columnsExcel[6] = new Columns() { Id = 7, Name = "Data.Heal", Description = "Лечение" };
            _columnsExcel[7] = new Columns() { Id = 8, Name = "Data.Death", Description = "Погиб" };
            _columnsExcel[8] = new Columns() { Id = 9, Name = "MatchTimeAsTime", Description = "Время боя" };
            _columnsExcel[9] = new Columns() { Id = 10, Name = "Result", Description = "Результат" };
        }

        internal string FileName { get; set; } = "CTournament";
        internal string FileNameWithExtension { get => $"{FileName}.xlsx"; }

        internal void SaveList(List<Models.TournamentReplay> tournamentReplays)
        {
            using var workbook = new XLWorkbook();

            IXLWorksheet worksheet = workbook.Worksheets.Add("TDSheet");


            worksheet.Cell(1, 1).Value = "№";
            foreach (Columns itemColumns in _columnsExcel)
            {
                worksheet.Cell(1, itemColumns.Id + 1).Value = itemColumns.Description;
            }


            for (int i = 0; i < tournamentReplays.Count; i++)
            {
                worksheet.Cell(i + 2, 1).Value = i.ToString();

                foreach (Columns itemColumns in _columnsExcel)
                {
                    worksheet.Cell(i + 2, itemColumns.Id + 1).Value = GetValueReplay(tournamentReplays[i], itemColumns);
                }
            }

            try
            {
                workbook.SaveAs(FileNameWithExtension);
            }
            catch (IOException ex)
            {
                MessageBox.Show("Не удалось сохранить данные в файл. Возможно файл открыт.");
                return;
            }
            RunFile();
        }

        private string GetValueReplay(Models.TournamentReplay replay, Columns itemColumns)
        {
            string value = string.Empty;

            if (itemColumns.Name == "Capitan")
                value = replay.Capitan;
            else if (itemColumns.Name == "GameMode")
                value = replay.GameMode;
            else if (itemColumns.Name == "IsRecruit")
                value = BoolToYesNo(replay.IsRecruit);
            else if (itemColumns.Name == "MinLevel")
                value = replay.MinLevelOperators.ToString();
            else if (itemColumns.Name == "Mission")
                value = replay.Mission;
            else if (itemColumns.Name == "Data.Damage")
                value = replay.Data.Damage.ToString();
            else if (itemColumns.Name == "Data.Heal")
                value = replay.Data.Heal.ToString();
            else if (itemColumns.Name == "Data.Death")
                value = replay.Data.Death.ToString();
            else if (itemColumns.Name == "MatchTimeAsTime")
                value = replay.MatchTimeAsTime.ToString();
            else if (itemColumns.Name == "Result")
                value = replay.Result.ToString();

            return value;
        }

        internal static string BoolToYesNo(bool value) => value ? "Да" : "Нет";

        private void RunFile()
        {
            Process process = new Process
            {
                StartInfo = new ProcessStartInfo()
                {
                    UseShellExecute = true,
                    FileName = FileNameWithExtension
                }
            };
            process.Start();
        }
    }
}
