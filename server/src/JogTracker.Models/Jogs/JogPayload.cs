using System;

namespace JogTracker.Models.Jogs
{
    public class JogPayload
    {
        public double? DistanceInMeters { get; set; }
        public double? DistanceInKilometers { get; set; }
        public JogTime ElapsedTime { get; set; }
        public DateTime Date { get; set; }
        public string UserId { get; set; }
    }
}
