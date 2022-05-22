using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace CTournament.ViewModels
{
    public class MainWindow : BindableBase, ISingleton
    {
        public MainWindow()
        {
            Events.Messages.EnterceptedCriticalErrors += (object sender, Models.CrashApp e) => { VisibilityCrashAppView = Visibility.Visible; };
        }

        public Views.TournamentDirectory TournamentDirectory { get; set; } = new Views.TournamentDirectory();
        public Views.RateSettings RateSettings { get; set; } = new Views.RateSettings();
        public Views.TournamentReplays TournamentReplays { get; set; } = new Views.TournamentReplays();
        public Views.UserStatistics UserStatistics { get; set; } = new Views.UserStatistics();

        public AdditionModels.VisibilityPanel VisibilityPanel { get; } = new AdditionModels.VisibilityPanel();

        public Views.CrashApp CrashAppView { get; set; } = new Views.CrashApp();
        public Visibility VisibilityCrashAppView { get; set; } = Visibility.Collapsed;
        public ICommand ChangeVisibilityCrashAppViewCommand => new DelegateCommand(() => { VisibilityCrashAppView = VisibilityCrashAppView == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible; });
    }
}
