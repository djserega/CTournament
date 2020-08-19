using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CTournament.Models
{
    public class TournamentsDirectory : BindableBase
    {
        private const string _keyFilesReplay = "*.bytes";

        public TournamentsDirectory(DirectoryInfo directoryInfo)
        {
            FullName = directoryInfo.FullName;
            Name = directoryInfo.Name;
            CountReplays = directoryInfo.GetFiles(_keyFilesReplay).Count();
        }

        public string FullName { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public int CountReplays { get; set; } = 0;

        public FileInfo[] GetFiles()
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(FullName);

            return directoryInfo.GetFiles(_keyFilesReplay);
        }
    }
}
