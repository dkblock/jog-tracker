using JogTracker.Models.Users;
using System;

namespace JogTracker.Models.Jogs
{
    public class Jog
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public double DistanceInMeters { get; set; }
        public double DistanceInKilometers { get; set; }
        public JogTime ElapsedTime { get; set; }
        public User User { get; set; }
    }
}
