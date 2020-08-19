using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;

namespace CTournament.ViewModels
{
    public class RateSettings : BindableBase, Models.ISavedRateSettings
    {
        private static RateSettings _rateSettings;

        public RateSettings()
        {
            _rateSettings = this;
            SetDefault();
        }

        public static RateSettings GetObject()
        {
            if (_rateSettings == null)
                _rateSettings = new RateSettings();

            return _rateSettings;
        }

        public Models.CalculationParameters Parameters { get; } = new Models.CalculationParameters();
        public Models.CalculationFormula Formula { get; set; } = new Models.CalculationFormula();

        public int Damage { get; set; }
        public int Heal { get; set; }
        public int Time { get; set; }
        public int Death { get; set; }
        public int Minutes { get; set; }
        public int Seconds { get; set; }
        public int RateMinSec { get; set; }
        public int CommonRate { get; set; }

        public double Result { get; private set; }
        public double ResultFull { get; private set; }

        public ICommand CommandSetDefault => new DelegateCommand(() =>
        {
            SetDefault();
        });

        public ICommand CommandCalculateDefault => new DelegateCommand(() =>
        {
            Formula.Calculate(Parameters);
            Result = Formula.Result;
            ResultFull = Formula.ResultFull;
        });

        public AdditionModels.VisibilityPanel VisibilityPanel { get; } = new AdditionModels.VisibilityPanel();

        public ICommand CommandSaveSettings => new DelegateCommand(() =>
        {
            var dialog = new Ookii.Dialogs.Wpf.VistaSaveFileDialog()
            {
                AddExtension = true,
                CheckFileExists = true,
                CheckPathExists = true,
                DefaultExt = "json",
                FileName = "settings",
                Filter = "*.json|*.json",
                Title = "Сохранение коэффицентов расчета в файл"
            };
            if (dialog.ShowDialog() ?? false)
            {
                Models.SavedRateSettings savedRateSettings = new Models.SavedRateSettings()
                {
                    CommonRate = CommonRate,
                    Damage = Damage,
                    Death = Death,
                    Heal = Heal,
                    Minutes = Minutes,
                    RateMinSec = RateMinSec,
                    Seconds = Seconds,
                    Time = Time
                };

                savedRateSettings.Save(dialog.FileName);

                Events.Messages.SendMessage(null, "Настройки сохранены");
            }
        });

        public ICommand CommandLoadSettings => new DelegateCommand(() =>
        {
            var dialog = new Ookii.Dialogs.Wpf.VistaOpenFileDialog()
            {
                AddExtension = false,
                CheckFileExists = true,
                CheckPathExists = true,
                DefaultExt = "json",
                FileName = "settings",
                Filter = "*.json|*.json",
                Title = "Сохранение коэффицентов расчета в файл"
            };
            if (dialog.ShowDialog() ?? false)
            {
                Models.SavedRateSettings savedRateSettings = Models.SavedRateSettings.Load(dialog.FileName);

                SetDefaultISavedRateSettings(savedRateSettings);

                Events.Messages.SendMessage(null, "Настройки сохранены");
            }
        });

        public ICommand CommandChangeValueFormula => new DelegateCommand(() =>
        {
            CalculateFormula();
        });

        private void CalculateFormula()
        {
            Formula.Calculate(Parameters);
            Result = Formula.Result;
            ResultFull = Formula.ResultFull;
            Formula.FillFormulaTemplate(this);
        }

        public void SetDefault()
        {
            Parameters.SetDefault();

            SetDefaultISavedRateSettings();

            CalculateFormula();
        }

        private void SetDefaultISavedRateSettings(Models.SavedRateSettings savedRateSettings = null)
        {
            if (savedRateSettings == null)
            {
                Damage = 1;
                Heal = 1;
                Time = 1;
                Death = 2;
                Minutes = 100;
                Seconds = 1;
                RateMinSec = 100;
                CommonRate = 100;
            }
            else
            {
                Damage = savedRateSettings.Damage;
                Heal = savedRateSettings.Heal;
                Time = savedRateSettings.Time;
                Death = savedRateSettings.Death;
                Minutes = savedRateSettings.Minutes;
                Seconds = savedRateSettings.Seconds;
                RateMinSec = savedRateSettings.RateMinSec;
                CommonRate = savedRateSettings.CommonRate;
            }
        }
    }
}
