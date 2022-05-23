using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;

namespace CTournament.Models
{
    public class CalculationFormula : BindableBase
    {
        public string Formula { get; } = "( (Ур - Л) / ОВ - П - (М + С) / Коэфф. времени ) / Общий коэфф.";
        public string FormulaTemplate { get; private set; } = "( (Ур - Л) / ОВ - П - (М + С) / Коэфф. времени ) / Общий коэфф.";
        public string FormulaDetailed { get; } = 
            "Ур - Урон" +
            "\nЛ - Лечение" +
            "\nОВ - Общее время" +
            "\nП - Погиб" +
            "\nМ - Минуты" +
            "\nС - Секунды";

        public double Result { get; set; }
        public double ResultFull { get; set; }

        public void Calculate(CalculationParameters parameters)
        {
            ViewModels.RateSettings rateSettings = ViewModels.RateSettings.GetObject();

            double damage = parameters.Damage * rateSettings.Damage;
            double heal = parameters.Heal * rateSettings.Heal;
            double time = parameters.Time * rateSettings.Time;
            double death = parameters.Death * rateSettings.Death;
            double minutes = parameters.Minutes * rateSettings.Minutes;
            double seconds = parameters.Seconds * rateSettings.Seconds;


            //           ((F      - H   ) / N    - G     - (L       + M      ) / 100                    ) * 100
            ResultFull = ((damage - heal) / time - death - (minutes + seconds) / rateSettings.RateMinSec) * rateSettings.CommonRate;

            Result = Math.Round(ResultFull);
        }

        public void FillFormulaTemplate(ViewModels.RateSettings rateSettings)
        {
            FormulaTemplate = (string)Formula.Clone();

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
    }
}
