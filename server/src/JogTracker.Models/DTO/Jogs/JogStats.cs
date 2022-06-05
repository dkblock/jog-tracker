namespace JogTracker.Models.DTO.Jogs
{
    public class JogStats
    {
        public double TotalDistanceInMeters { get; set; }
        public double TotalElapsedTimeInSeconds { get; set; }
        public double AverageSpeedInMetersPerSecond { get; set; }
        public string UserId { get; set; }
    }
}
