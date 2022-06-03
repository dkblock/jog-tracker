using JogTracker.Models.DTO.Users;
using System;

namespace JogTracker.Models.DTO.Jogs
{
    public class Jog
    {
        public string Id { get; set; }
        public DateTime Date { get; set; }
        public double DistanceInMeters { get; set; }
        public double DistanceInKilometers { get; set; }
        public double AverageSpeedInMetersPerSecond { get; set; }
        public double AverageSpeedInKilometersPerHour { get; set; }
        public JogTime ElapsedTime { get; set; }
        public User User { get; set; }
    }
}
