using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;

namespace CTournament.Errors
{
    public class Error
    {
        public Error()
        {
            CreatedDate = DateTime.Now;
        }

        public Error(string message) : this()
        {
            Message = message;
        }

        public Error(string message, ErrorType type) : this(message)
        {
            Type = type;
        }
       
        public Error(string message, string path) : this(message)
        {
            Path = path;
        }

        public Error(string message, ErrorType type, string path) : this(message, type)
        {
            Path = path;
        }

        public string Message { get; set; }
        public ErrorType Type { get; set; } = ErrorType.Normal;
        public PackIcon ImageType
        {
            get
            {
                PackIconKind iconKind = Type switch
                {
                    ErrorType.Normal => PackIconKind.InformationVariant,
                    ErrorType.Critical => PackIconKind.AlertOutline,
                    _ => PackIconKind.InfoCircleOutline,
                };

                return new PackIcon() { Kind = iconKind };
            }
        }
        public DateTime CreatedDate { get; set; }
        public string Path { get; set; }

    }
}
