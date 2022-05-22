using DevExpress.Mvvm;
using GalaSoft.MvvmLight.Threading;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace CTournament.ViewModels
{
    public class UserStatistics : BindableBase, ISingleton
    {
        public static event EventHandler<List<Models.ItemUserStatisticsItemUpdater>> CalculationStatistics;

        private readonly object _lock = new object();

        private readonly Statistics.Statistic _statistic = new Statistics.Statistic();
        private List<Models.TournamentReplay> _currentListReplays;
        private bool isCurrentUser = false;

        public UserStatistics()
        {
            TournamentReplays.UpdateListReplaysEvents += (object sender, List<Models.TournamentReplay> e) =>
            {
                _currentListReplays = e;
                Task.Run(async () => { await UpdateInfoAsync(true); await UpdateInfoAsync(); });
            };
            InitFrame();
            UpdateInfo();
        }

        public bool IsCurrentUser
        {
            get => isCurrentUser;
            set
            {
                isCurrentUser = value;
                Task.Run(() => UpdateInfoAsync());
            }
        }

        public string UserStatistics1Header { get; set; }
        public string UserStatistics2Header { get; set; }
        public string UserStatistics3Header { get; set; }
        public string UserStatistics4Header { get; set; }
        public string UserStatistics5Header { get; set; }
        public string UserStatistics6Header { get; set; }
        public string UserStatistics7Header { get; set; }
        public string UserStatistics8Header { get; set; }

        public List<Frame> UserStatistics1 { get; set; } = new List<Frame>();
        public List<Frame> UserStatistics2 { get; set; } = new List<Frame>();
        public List<Frame> UserStatistics3 { get; set; } = new List<Frame>();
        public List<Frame> UserStatistics4 { get; set; } = new List<Frame>();
        public List<Frame> UserStatistics5 { get; set; } = new List<Frame>();
        public List<Frame> UserStatistics6 { get; set; } = new List<Frame>();
        public List<Frame> UserStatistics7 { get; set; } = new List<Frame>();
        public List<Frame> UserStatistics8 { get; set; } = new List<Frame>();

        public ICommand UpdateCommand => new DelegateCommand(async () =>
        {
            await UpdateInfoAsync();
        });

        private async Task UpdateInfoAsync(bool init = false)
        {
            await Task.Run(() => UpdateInfo(init));
        }

        private void UpdateInfo(bool init = false)
        {
            lock (_lock)
            {
                if (init)
                    _statistic.Init();
                else
                    FillStatistics();

                UserStatistics1Header = "Ликвидировано оперативников";
                UserStatistics2Header = "Ликвидировано ботов";
                UserStatistics3Header = "Погиб";
                UserStatistics4Header = "Содействия";
                UserStatistics5Header = "Урон";
                UserStatistics6Header = "Лечение";

                List<Models.ItemUserStatisticsItemUpdater> userStatistics = _statistic.GetAllStatistics();

                for (int i = 0; i < userStatistics.Count; i++)
                {
                    userStatistics[i].Id = GetRandomItemStatistic(i);
                }

                CalculationStatistics?.Invoke(null, userStatistics);

                while (userStatistics.Count > 0)
                {
                    int index = new Random().Next(0, userStatistics.Count - 1);
                    Events.UserStatisticsItemUpdater.Update(
                        new Models.UserStatisticsItemValue(
                            userStatistics[index].Id,
                            userStatistics[index].GetValueAction.Invoke()));
                    Thread.Sleep(init ? 0 : 50);

                    userStatistics.RemoveAt(index);
                }
            }
        }

        private void FillStatistics()
        {
            _statistic.Init();

            if (_currentListReplays != null)
                _statistic.FillDataReplays(_currentListReplays, IsCurrentUser);
        }

        private void InitFrame()
        {
            DispatcherHelper.CheckBeginInvokeOnUI(() =>
            {
                UserStatistics1.Add(new Frame() { Content = new Views.UserStatisticsItem() });
                UserStatistics1.Add(new Frame() { Content = new Views.UserStatisticsItem() });
                UserStatistics1.Add(new Frame() { Content = new Views.UserStatisticsItem() });
                UserStatistics1.Add(new Frame() { Content = new Views.UserStatisticsItem() });

                UserStatistics2.Add(new Frame() { Content = new Views.UserStatisticsItem() });
                UserStatistics2.Add(new Frame() { Content = new Views.UserStatisticsItem() });
                UserStatistics2.Add(new Frame() { Content = new Views.UserStatisticsItem() });
                UserStatistics2.Add(new Frame() { Content = new Views.UserStatisticsItem() });

                UserStatistics3.Add(new Frame() { Content = new Views.UserStatisticsItem() });
                UserStatistics3.Add(new Frame() { Content = new Views.UserStatisticsItem() });
                UserStatistics3.Add(new Frame() { Content = new Views.UserStatisticsItem() });
                UserStatistics3.Add(new Frame() { Content = new Views.UserStatisticsItem() });

                UserStatistics4.Add(new Frame() { Content = new Views.UserStatisticsItem() });
                UserStatistics4.Add(new Frame() { Content = new Views.UserStatisticsItem() });
                UserStatistics4.Add(new Frame() { Content = new Views.UserStatisticsItem() });
                UserStatistics4.Add(new Frame() { Content = new Views.UserStatisticsItem() });

                UserStatistics5.Add(new Frame() { Content = new Views.UserStatisticsItem() });
                UserStatistics5.Add(new Frame() { Content = new Views.UserStatisticsItem() });
                UserStatistics5.Add(new Frame() { Content = new Views.UserStatisticsItem() });
                UserStatistics5.Add(new Frame() { Content = new Views.UserStatisticsItem() });

                UserStatistics6.Add(new Frame() { Content = new Views.UserStatisticsItem() });
                UserStatistics6.Add(new Frame() { Content = new Views.UserStatisticsItem() });
                UserStatistics6.Add(new Frame() { Content = new Views.UserStatisticsItem() });
                UserStatistics6.Add(new Frame() { Content = new Views.UserStatisticsItem() });
            });
        }

        private int GetRandomItemStatistic(int id)
        {
            return UserStatisticsItem.ListUserStatisticsItemID[id];
        }

    }
}
