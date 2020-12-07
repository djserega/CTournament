using GalaSoft.MvvmLight.Threading;
using System;

namespace CTournament.Events
{
    internal class Messages
    {
        internal static event EventHandler<Errors.Error> ErrorMessage;
        internal static event EventHandler<Models.CrashApp> EnterceptedCriticalErrors;


        internal static void SendMessage(object sender, string message)
        {
            SendMessage(sender, message, Errors.ErrorType.Normal);
        }
        internal static void SendMessage(object sender, string message, Errors.ErrorType type)
        {
            SendMessage(sender, message, type, null);
        }
        internal static void SendMessage(object sender, string message, string path)
        {
            SendMessage(sender, message, Errors.ErrorType.Normal, path);
        }
        internal static void SendMessage(object sender, string message, Errors.ErrorType type, string path)
        {
            if (!string.IsNullOrWhiteSpace(message))
            {
                DispatcherHelper.CheckBeginInvokeOnUI(
                   () =>
                   {
                       Errors.Error error = new Errors.Error(message, type, path);

                       ErrorMessage?.Invoke(sender, error);
                   });
            }
        }
        internal static void SendCrashMessage(object sender, string header, string message)
        {
            if (!string.IsNullOrWhiteSpace(message))
            {
                DispatcherHelper.CheckBeginInvokeOnUI(
                   () =>
                   {
                       EnterceptedCriticalErrors?.Invoke(null, new Models.CrashApp(header, message));
                   });
            }
        }
    }
}
