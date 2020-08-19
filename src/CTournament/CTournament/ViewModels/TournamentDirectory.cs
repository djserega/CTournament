using DevExpress.Mvvm;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Data;
using System.Windows.Input;

namespace CTournament.ViewModels
{
    public class TournamentDirectory : BindableBase
    {
        public static event EventHandler<Models.TournamentsDirectory> ChangeSelectedItemTournamentsDirectoryEvents;

        private const int _maxLengthReplayPathView = 40;
        private Models.TournamentsDirectory selectedItemTournamentsDirectory;

        public TournamentDirectory()
        {
            string _pathApplication = new FileInfo(Assembly.GetExecutingAssembly().Location).Directory.FullName;

            ReplayPath = Path.Combine(
                _pathApplication,
                    "Replays");

            UpdateListTournamentsDirectory();
        }

        public ICommand CommandSelectReplayPath => new DelegateCommand(() =>
        {
            var dialog = new Ookii.Dialogs.Wpf.VistaFolderBrowserDialog()
            {
                Description = "Выберите базовый каталог реплеев турнира:",
                SelectedPath = (ReplayPath + "\\")
            };
            if (dialog.ShowDialog() ?? false)
            {
                ReplayPath = dialog.SelectedPath;

                UpdateListTournamentsDirectory();
            }
        });

        public ICommand CommandUpdateReplayPath => new DelegateCommand(() =>
        {
            UpdateListTournamentsDirectory();
        });

        public string ReplayPath { get; set; } = string.Empty;

        public string ReplayPathView
        {
            get
            {
                if (ReplayPath.Length > _maxLengthReplayPathView)
                    return $"...{ReplayPath.Substring(ReplayPath.Length - _maxLengthReplayPathView)}";
                else
                    return ReplayPath;
            }
        }

        public ICollectionView TournamentsDirectory { get; set; }

        public Models.TournamentsDirectory SelectedItemTournamentsDirectory 
        {
            get => selectedItemTournamentsDirectory;
            set { selectedItemTournamentsDirectory = value; ChangeSelectedItemTournamentsDirectoryEvents?.Invoke(null, value); } 
        }

        private void UpdateListTournamentsDirectory()
        {
            DirectoryInfo directoryInfoReplays = new DirectoryInfo(ReplayPath);
            if (!directoryInfoReplays.Exists)
                return;

            List<Models.TournamentsDirectory> listDirectory = new List<Models.TournamentsDirectory>();
            foreach (DirectoryInfo itemTournamentDir in directoryInfoReplays.GetDirectories())
            {
                try
                {
                    listDirectory.Add(new Models.TournamentsDirectory(itemTournamentDir));
                }
                catch (UnauthorizedAccessException)
                {
                }
            }

            TournamentsDirectory = CollectionViewSource.GetDefaultView(listDirectory);
        }
    }
}
