using JogTracker.Models.DTO.Users;

namespace JogTracker.Models.DTO.Jogs
{
    public class JogReport
    {
        public int TotalJogs { get;set; }
        public int TotalOwnJogs { get; set; }

        public double MaxTotalDistanceInMeters { get; set; }
        public double MaxTotalDistanceInKilometers { get; set; }
        public User MaxTotalDistanceUser { get; set; }

        public JogTime MaxTotalElapsedTime { get; set; }
        public User MaxTotalElapsedTimeUser { get; set; }

        public double MaxAverageSpeedInMetersPerSecond { get; set; }
        public double MaxAverageSpeedInKilometersPerHour { get; set; }
        public User MaxAverageSpeedUser { get; set; }

        public double OwnTotalDistanceInMeters { get; set; }
        public double OwnTotalDistanceInKilometers { get; set; }
        public JogTime OwnTotalElapsedTime { get; set; }
        public double OwnAverageSpeedInMetersPerSecond { get; set; }
        public double OwnAverageSpeedInKilometersPerHour { get; set; }
        public User User { get; set; }
    }
}
