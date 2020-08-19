using DevExpress.Mvvm;
using System.Windows;
using System.Windows.Input;

namespace CTournament.AdditionModels
{
    public class VisibilityPanel : BindableBase
    {
        public Visibility VisibilityBorderLegentUp { get; private set; } = Visibility.Visible;

        public Visibility VisibilityBorderLegentDown { get; private set; } = Visibility.Collapsed;

        public ICommand CommandChangeVisibility => new DelegateCommand(() =>
        {
            VisibilityBorderLegentUp = VisibilityBorderLegentUp == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
            VisibilityBorderLegentDown = VisibilityBorderLegentDown == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
        });

    }
}
