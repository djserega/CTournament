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
        internal string FileName { get; set; } = "CTournament";
        internal string FileNameWithExtension
        {
            get
            {
                if (IsCurrentUser)
                    return $"{FileName} (персональный зачет авторов).xlsx";
                else
                    return $"{FileName}.xlsx";
            }
        }
        internal bool IsCurrentUser { get; set; }

        internal void SaveList(List<Models.TournamentReplay> tournamentReplays)
        {
            using var workbook = new XLWorkbook();

            MainStatistics mainStatistics = new MainStatistics() { TournamentReplays = tournamentReplays };
            mainStatistics.SaveMainStatistics(workbook);

            PersonalStatistics personalStatistics = new PersonalStatistics() { TournamentReplays = tournamentReplays };
            personalStatistics.SavePersonalStatistics(workbook, IsCurrentUser);

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
