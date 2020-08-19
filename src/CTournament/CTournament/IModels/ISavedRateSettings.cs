namespace CTournament.Models
{
    public interface ISavedRateSettings
    {
        // ((F-H)/N-G * 2-(L * 100+M)/100) * 100
        int Damage { get; set; }      // F
        int Heal { get; set; }        // H
        int Time { get; set; }        // N
        int Death { get; set; }       // G
        int Minutes { get; set; }     // L
        int Seconds { get; set; }     // M
        int RateMinSec { get; set; }  
        int CommonRate { get; set; }
    }
}