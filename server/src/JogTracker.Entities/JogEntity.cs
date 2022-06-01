﻿using System;

namespace JogTracker.Entities
{
    public class JogEntity
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public double DistanceInMeters { get; set; }
        public long ElapsedTimeInSeconds { get; set; }
        public string UserId { get; set; }

        public UserEntity User { get; set; }
    }
}
