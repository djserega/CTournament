using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;

namespace CTournament.ViewModels
{
    public class MainWindow : BindableBase
    {
        public Views.TournamentDirectory TournamentDirectory { get; set; } = new Views.TournamentDirectory();
        public Views.RateSettings RateSettings { get; set; } = new Views.RateSettings();
        public Views.TournamentReplays TournamentReplays { get; set; } = new Views.TournamentReplays();
        public Views.UserStatistics UserStatistics { get; set; } = new Views.UserStatistics();

        public AdditionModels.VisibilityPanel VisibilityPanel { get; } = new AdditionModels.VisibilityPanel();
    }
}
