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
        public int Damage { get; set; } // F
        public int Heal { get; set; } // H
        public int Time { get; set; } // N
        public int Death { get; set; } // G
        public int Minutes { get; set; } // L
        public int Seconds { get; set; } // M
        public int RateMinSec { get; set; }
        public int CommonRate { get; set; }

        public void Save(string fileName)
        {
            string serializedSettings = JsonConvert.SerializeObject(this);

            using StreamWriter writer = new StreamWriter(fileName, false);
            writer.Write(serializedSettings);
            writer.Flush();
            writer.Close();
        }

        public static SavedRateSettings Load(string fileName)
        {
            FileInfo fileInfo = new FileInfo(fileName);

            using StreamReader reader = new StreamReader(fileInfo.OpenRead());

            string serializedSettings = reader.ReadToEnd();

            reader.Close();

            return JsonConvert.DeserializeObject<SavedRateSettings>(serializedSettings);
        }
    }
}
