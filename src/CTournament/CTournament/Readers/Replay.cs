using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace CTournament.Readers
{
    internal sealed class Replay
    {
        private static readonly object _lock = new object();

        private const string _keyStartGameModeInfo = "{\"GameMode\":";
        private const string _textEndGameMode = "\"ReplayPath\":null}";
        private const string _keyStartResultInfo = "{\"PlayersData\":";

        internal bool InvokeUpdaterData { get; set; } = true;

        internal Models.CReplay.GameModeInfo GameModeInfo { get; private set; }
        internal Models.CReplay.ResultInfo ResultInfo { get; private set; }

        internal bool Read(string fileName, bool showMessage = true)
        {
            lock (_lock)
            {
                GameModeInfo = default;
                ResultInfo = default;

                Exception innerException = null;

                bool result = false;

                if (string.IsNullOrWhiteSpace(fileName))
                {
                    if (showMessage)
                        Events.Messages.SendMessage(this, "Не выбран файл реплея.");
                }
                else
                {
                    try
                    {
                        FileInfo fileInfo = new FileInfo(fileName);

                        if (fileInfo.Exists)
                        {
                            using FileStream stream = fileInfo.OpenRead();

                            using StreamReader reader = new StreamReader(stream, true);
                            try
                            {
                                int countJump = 0;
                                while (reader.Peek() >= 0)
                                {
                                    string lineLog = reader.ReadLine();

                                    if (ContainsStartParameter(lineLog, _keyStartGameModeInfo))
                                    {
                                        int endPartGameMode = ParseGameMode(lineLog);

                                        ParseModelResult(lineLog, endPartGameMode);

                                        result = true;
                                    }
                                    else if (ContainsStartParameter(lineLog, _keyStartResultInfo))
                                    {
                                        ParseModelResult(lineLog);
                                    }
                                    if (++countJump >= 5)
                                        break;
                                }
                            }
                            catch (Exception ex)
                            {
                                Events.Messages.SendMessage(null, ex.Message, Errors.ErrorType.Critical);
                                innerException = new Exception(ex.Message, ex);
                            }
                            finally
                            {
                                reader.Dispose();
                            }
                        }
                        else
                        {
                            Events.Messages.SendMessage(null, "Файл реплея не существует.");
                        }

                    }
                    catch (IOException ex)
                    {
#if DEBUG
                        Events.Messages.SendMessage(this, $"Ошибка чтения реплея. Нет доступа.\n\n{ex}");
#endif
                    }
                    catch (Exception ex)
                    {
                        Events.Messages.SendMessage(this, $"Ошибка чтения реплея.\n\n{ex}");
                    }
                }

                if (InvokeUpdaterData)
                {
                    Events.GameModeInfo.Updating(GameModeInfo);
                    Events.ResultInfo.Updating(ResultInfo);
                }

                if (innerException != null)
                    throw innerException;

                return result;
            }
        }

        private int ParseGameMode(string lineLog)
        {
            int startPartGameMode = lineLog.IndexOf(_keyStartGameModeInfo);

            int endGameMode = lineLog.IndexOf(_textEndGameMode);
            int endPartGameMode = endGameMode + _textEndGameMode.Length - startPartGameMode;

            string textGameMode = lineLog.Substring(startPartGameMode, endPartGameMode);

            GameModeInfo = JsonConvert.DeserializeObject<Models.CReplay.GameModeInfo>(textGameMode);

            Models.CReplay.GameModeInfo.FillObject(GameModeInfo);

            return endPartGameMode;
        }

        private void ParseModelResult(string lineLog, int endPartGameMode = 0)
        {
            int startPartPlayersData = lineLog.IndexOf(_keyStartResultInfo, endPartGameMode);

            if (startPartPlayersData > 0)
            {
                string partPlayersData = lineLog.Substring(startPartPlayersData);
                int positionStartMatchId = partPlayersData.IndexOf("MatchId");
                int positionEndMatchId = partPlayersData.IndexOf("}", positionStartMatchId);

                partPlayersData = partPlayersData.Substring(0, positionEndMatchId + 1);

                ResultInfo = JsonConvert.DeserializeObject<Models.CReplay.ResultInfo>(partPlayersData);

                Models.CReplay.ResultInfo.FillObject(ResultInfo);
                
                SetPlayersGameModeFromResultInfo();
            }
        }

        private bool ContainsStartParameter(string source, string text)
        {
            return source.Length > 40
                && source.Substring(0, 40).Contains(text);
        }

        private void SetPlayersGameModeFromResultInfo()
        {
            foreach (Models.CReplay.Players.PlayersDataInfo itemDataInfo in ResultInfo.PlayersData)
            {
                GameModeInfo.Players.Find(f => f.NickName == itemDataInfo.Nickname).PlayersDataInfo = itemDataInfo;
            }
        }
    }
}
