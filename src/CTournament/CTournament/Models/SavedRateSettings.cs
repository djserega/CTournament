using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CTournament.Models
{
    public class SavedRateSettings : ISavedRateSettings
    {
        // ((F-H)/N-G * 2-(L * 100+M)/100) * 100
        public double PlayedRoundsCount { get; set; }
        public double Damage { get; set; } // F
        public double Heal { get; set; } // H
        public double Time { get; set; } // N
        public double Death { get; set; } // G
        public double Minutes { get; set; } // L
        public double Seconds { get; set; } // M
        public double RateMinSec { get; set; }
        public double CommonRate { get; set; }

        public void Save(string fileName)
        {
            string serializedSettings = JsonConvert.SerializeObject(this);

            using StreamWriter writer = new(fileName, false);
            writer.Write(serializedSettings);
            writer.Flush();
            writer.Close();
        }

        public static SavedRateSettings Load(string fileName)
        {
            FileInfo fileInfo = new(fileName);

            using StreamReader reader = new(fileInfo.OpenRead());

            string serializedSettings = reader.ReadToEnd();

            reader.Close();

            return JsonConvert.DeserializeObject<SavedRateSettings>(serializedSettings);
        }
    }
}
