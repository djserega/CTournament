namespace CTournament.Models
{
    public interface ISavedRateSettings
    {
        // ((F-H)/N-G * 2-(L * 100+M)/100) * 100
        double PlayedRoundsCount { get; set; }
        double Damage { get; set; }      // F
        double Heal { get; set; }        // H
        double Time { get; set; }        // N
        double Death { get; set; }       // G
        double Minutes { get; set; }     // L
        double Seconds { get; set; }     // M
        double RateMinSec { get; set; }
        double CommonRate { get; set; }
    }
}