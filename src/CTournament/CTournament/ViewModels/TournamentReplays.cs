using DevExpress.Mvvm;
using DevExpress.Mvvm.Native;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace CTournament.ViewModels
{
    public class TournamentReplays : BindableBase
    {
        private readonly object _lock = new object();

        public static event EventHandler<List<Models.TournamentReplay>> UpdateListReplaysEvents;

        private Models.TournamentsDirectory _currentTournametDirectory;

        public TournamentReplays()
        {
            TournamentDirectory.ChangeSelectedItemTournamentsDirectoryEvents += (object sender, Models.TournamentsDirectory e) =>
            {
                _currentTournametDirectory = e;
                Task.Run(async () => await UpdateListReplaysAsync());
            };
        }

        public ICollectionView Replays { get; set; }

        public bool ReplaysUpdating { get; private set; } = false;

        public Visibility VisibilityReplaysUpdating { get => ReplaysUpdating ? Visibility.Visible : Visibility.Collapsed; }

        public int CountRepays { get; private set; } = 0;

        public string CountRepaysText
        {
            get
            {
                string textProgress = $"          {CountRepays}";

                return textProgress.Substring(textProgress.Length - CountAllReplays.ToString().Length - 1);
            }
        }

        public int CountAllReplays { get; private set; } = 0;

        public int ProgressLoadingReplays { get { if (CountAllReplays == 0) return 0; return CountRepays * 100 / CountAllReplays; } }

        public string TextUpdatingMessage
        {
            get
            {
                if (CountRepays == 0 && CountAllReplays == 0)
                    return "Подготовка к загрузке";
                else
                    return $"Загрузка данных:";
            }
        }

        public bool VisibleDetailedInfoReplay { get; set; } = false;

        public DataGridRowDetailsVisibilityMode VisibilityDetailedInfoReplay { get => VisibleDetailedInfoReplay ? DataGridRowDetailsVisibilityMode.VisibleWhenSelected : DataGridRowDetailsVisibilityMode.Collapsed; }

        public ICommand SaveReplaysToExcelCommand => new DelegateCommand(() =>
        {
            if (Replays == null)
                return;

            List<Models.TournamentReplay> tournamentReplays = Replays.Cast<Models.TournamentReplay>().ToList();

            SaveToExcel.Saving saveToExcel = new SaveToExcel.Saving()
            {
                FileName = _currentTournametDirectory.Name
            };
            saveToExcel.SaveList(tournamentReplays);
        });

         public ICommand SaveReplaysToExcelIsCurrentUserCommand => new DelegateCommand(() =>
        {
            if (Replays == null)
                return;

            List<Models.TournamentReplay> tournamentReplays = Replays.Cast<Models.TournamentReplay>().ToList();

            SaveToExcel.Saving saveToExcel = new SaveToExcel.Saving()
            {
                FileName = _currentTournametDirectory.Name,
                IsCurrentUser = true
            };
            saveToExcel.SaveList(tournamentReplays);
        });

       public ICommand RefreshListReplaysCommand => new DelegateCommand(async () => { await UpdateListReplaysAsync(); });

        private async Task UpdateListReplaysAsync()
        {
            try
            {
                await Task.Run(() => UpdateListReplays(_currentTournametDirectory));
            }
            catch (Exception ex)
            {
                Events.Messages.SendCrashMessage(
                    null,
                    "Ошибка при чтении каталога реплеев.",
                    ex.ToString());
            }
        }

        private void UpdateListReplays(Models.TournamentsDirectory directory)
        {
            ReplaysUpdating = true;

            List<Models.TournamentReplay> replays = new List<Models.TournamentReplay>();

            if (directory != null)
            {
                ReadDirectoryReplays(directory, replays);

                replays.Sort((a, b) => b.Result.CompareTo(a.Result));
            }

            Replays = CollectionViewSource.GetDefaultView(replays);

            UpdateListReplaysEvents?.Invoke(null, replays);

            ReplaysUpdating = false;
        }

        private void ReadDirectoryReplays(Models.TournamentsDirectory directory, List<Models.TournamentReplay> replays)
        {
            Models.CalculationFormula formula = new Models.CalculationFormula();

            Readers.Replay replayReader = new Readers.Replay();

            FileInfo[] files = directory.GetFiles();

            CountRepays = 0;
            CountAllReplays = files.Count();

            foreach (FileInfo itemFileReplay in files)
            {
                CountRepays += 1;

                if (replayReader.Read(itemFileReplay.FullName))
                {
                    if (replayReader.ResultInfo != null)
                    {
                        List<Models.CReplay.Players.PlayersInfo> playersDatas = replayReader.GameModeInfo.Players.ToList();
                        playersDatas.Sort((a, b) => a.PlayersDataInfo.SortedRole.CompareTo(b.PlayersDataInfo.SortedRole));

                        Models.CalculationParameters parameters = new Models.CalculationParameters(replayReader.ResultInfo);

                        formula.Calculate(parameters);

                        Models.TournamentReplay tournamentReplay = new Models.TournamentReplay()
                        {
                            Name = itemFileReplay.Name,
                            FullName = itemFileReplay.FullName,
                            Data = parameters,
                            PlayersData = playersDatas.ToArray(),
                            MatchTime = replayReader.ResultInfo.MatchTimeView,
                            MatchTimeAsTime = replayReader.ResultInfo.MatchTimeViewTime,
                            Capitan = replayReader.ResultInfo.PlayersData.FirstOrDefault(item => item.IsCurrentUser).Nickname,
                            IsRecruit = PlayersIsRecruit(replayReader.GameModeInfo.Players),
                            MinLevelOperators = replayReader.GameModeInfo.Players.Min(item => item.Level),
                            Result = formula.Result,
                            Mission = replayReader.GameModeInfo.MissionView,
                            GameMode = replayReader.GameModeInfo.GameModeView
                        };

                        replays.Add(tournamentReplay);
                    }
                }
            }
        }

        private bool PlayersIsRecruit(List<Models.CReplay.Players.PlayersInfo> players)
        {
            foreach (Models.CReplay.Players.PlayersInfo player in players)
            {
                if (player.Visual?.Contains("PL_RUS_Recruit_", StringComparison.OrdinalIgnoreCase) ?? false)
                    return true;
            }

            return false;
        }
    }
}
