using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace CTournament.ViewModels
{
    public class CrashApp : BindableBase, ISingleton
    {
        public CrashApp()
        {
            Events.Messages.EnterceptedCriticalErrors += (object sender, Models.CrashApp e) =>
            {
                Header = e.Header;
                Message = e.Message;
            };
        }

        public string Header { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;

        public ICommand CopyToClipboardCommand => new DelegateCommand(() => { Clipboard.SetText(Message); });
    }
}
