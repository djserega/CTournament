using System;
using System.Collections.Generic;
using System.Text;

namespace CTournament.Models
{
    internal class CrashApp
    {
        public CrashApp(string header, string message)
        {
            Header = header;
            Message = message;
        }

        public string Header { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
    }
}
