using JogTracker.Models.DTO.Jogs;
using System;

namespace JogTracker.Models.Requests.Jogs
{
    public class JogPayloadCommand
    {
        public double? DistanceInMeters { get; set; }
        public double? DistanceInKilometers { get; set; }
        public JogTime ElapsedTime { get; set; }
        public DateTime Date { get; set; }
        public string UserId { get; set; }
    }
}
