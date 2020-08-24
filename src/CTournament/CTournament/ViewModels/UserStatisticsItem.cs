using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace CTournament.ViewModels
{
    public class UserStatisticsItem : BindableBase
    {
        private int _id = new Random().Next(0, 10241024);
        public UserStatisticsItem()
        {
            ListUserStatisticsItemID.Add(_id);

            Events.UserStatisticsItemUpdater.Updater += UserStatisticsItemUpdater_Updater;
        }

        public static List<int> ListUserStatisticsItemID { get; set; } = new List<int>();
        public Visibility VisibilityPage { get; set; } = Visibility.Hidden;

        private void UserStatisticsItemUpdater_Updater(object sender, Models.UserStatisticsItemValue e)
        {
            if (_id == e.Id)
            {
                _id = e.Id;

                Header = e.Header;

                Value1 = e.Value1;
                Value2 = e.Value2;
                Value3 = e.Value3;

                VisibilityPage = e.Header == null ? Visibility.Hidden : Visibility.Visible;
            }
        }

        public Models.UserStatisticsItemValueItem Header { get; set; } 
        public Models.UserStatisticsItemValueItem Value1 { get; set; }
        public Models.UserStatisticsItemValueItem Value2 { get; set; }
        public Models.UserStatisticsItemValueItem Value3 { get; set; }
    }
}
