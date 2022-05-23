using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;

namespace CTournament.Models
{
    public class CalculationFormula : BindableBase
    {
        public CalculationFormula()
        {
            switch (DefaultValues.RateFormulaVariant)
            {
                case RateFormulaVariants.speedRun:
                    Formula = "( (Ур - Л) / ОВ - П - (М + С) / Коэфф. времени ) / Общий коэфф.";
                    FormulaTemplate = "( (Ур - Л) / ОВ - П - (М + С) / Коэфф. времени ) / Общий коэфф.";
                    FormulaDetailed =
                        "Ур - Урон" +
                        "\nЛ - Лечение" +
                        "\nОВ - Общее время" +
                        "\nП - Погиб" +
                        "\nМ - Минуты" +
                        "\nС - Секунды";
                    break;
                case RateFormulaVariants.damagePerRound:
                    Formula = "( Ур / Р) / Общий коэфф.";
                    FormulaTemplate = "(Ур / Р) / Общий коэфф.";
                    FormulaDetailed =
                      "Ур - Урон" +
                      "\nР - Общее количество раундов";
                    break;
            }
        }

        public string Formula { get; } 
        public string FormulaTemplate { get; private set; }
        public string FormulaDetailed { get; } 

        public double Result { get; set; }
        public double ResultFull { get; set; }

        public void Calculate(CalculationParameters parameters)
        {
            ViewModels.RateSettings rateSettings = ViewModels.RateSettings.GetObject();

            double playedRoundsCount = parameters.PlayedRoundsCount * rateSettings.PlayedRoundsCount;
            double damage = parameters.Damage * rateSettings.Damage;
            double heal = parameters.Heal * rateSettings.Heal;
            double time = parameters.Time * rateSettings.Time;
            double death = parameters.Death * rateSettings.Death;
            double minutes = parameters.Minutes * rateSettings.Minutes;
            double seconds = parameters.Seconds * rateSettings.Seconds;

            switch (DefaultValues.RateFormulaVariant)
            {
                case RateFormulaVariants.speedRun:
                    //           ((F      - H   ) / N    - G     - (L       + M      ) / 100                    ) * 100
                    ResultFull = ((damage - heal) / time - death - (minutes + seconds) / rateSettings.RateMinSec) * rateSettings.CommonRate;
                    break;
                case RateFormulaVariants.damagePerRound:
                    //           (F      - PRC              ) * 100
                    ResultFull = (damage / playedRoundsCount) * rateSettings.CommonRate;
                    break;
                default:
                    break;
            }

            Result = Math.Round(ResultFull);
        }

        public void FillFormulaTemplate(ViewModels.RateSettings rateSettings)
        {
            FormulaTemplate = (string)Formula.Clone();

            SetValueToTemplateFormula("Р", rateSettings.PlayedRoundsCount, rateSettings.Parameters.PlayedRoundsCount);

            SetValueToTemplateFormula("Ур", rateSettings.Damage, rateSettings.Parameters.Damage);
            SetValueToTemplateFormula("Л", rateSettings.Heal, rateSettings.Parameters.Heal);
            SetValueToTemplateFormula("ОВ", rateSettings.Time, rateSettings.Parameters.Time);
            SetValueToTemplateFormula("П", rateSettings.Death, rateSettings.Parameters.Death);
            SetValueToTemplateFormula("М", rateSettings.Minutes, rateSettings.Parameters.Minutes);
            SetValueToTemplateFormula("С", rateSettings.Seconds, rateSettings.Parameters.Seconds);

            FormulaTemplate = FormulaTemplate.Replace("Коэфф. времени", rateSettings.RateMinSec.ToString());
            FormulaTemplate = FormulaTemplate.Replace("Общий коэфф.", rateSettings.CommonRate.ToString());
        }

        private void SetValueToTemplateFormula(string key, int coeff, int value)
        {
            FormulaTemplate = FormulaTemplate.Replace(key, $"{coeff} * {value}");
        }
        private void SetValueToTemplateFormula(string key, double coeff, int value)
        {
            FormulaTemplate = FormulaTemplate.Replace(key, $"{coeff} * {value}");
        }
    }
}
