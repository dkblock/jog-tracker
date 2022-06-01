using JogTracker.Entities;
using JogTracker.Models;
using JogTracker.Models.Jogs;
using System;

namespace JogTracker.Mappers
{
    public interface IJogMapper
    {
        JogEntity ToEntity(JogPayload payload);
        Jog ToModel(JogEntity entity);
    }

    public class JogMapper : IJogMapper
    {
        private const int MetersInOneKilometer = 1000;

        public JogEntity ToEntity(JogPayload payload)
        {
            var distance = payload.DistanceInMeters ?? payload.DistanceInKilometers.Value * MetersInOneKilometer;
            var elapsedTime = new TimeSpan(payload.ElapsedTime.Hours, payload.ElapsedTime.Minutes, payload.ElapsedTime.Seconds);

            return new JogEntity
            {
                Date = payload.Date,
                DistanceInMeters = distance,
                ElapsedTimeInSeconds = (int)elapsedTime.TotalSeconds,
            };
        }

        public Jog ToModel(JogEntity entity)
        {
            if (entity == null)
                return null;

            var time = TimeSpan.FromSeconds(entity.ElapsedTimeInSeconds);

            return new Jog
            {
                Id = entity.Id,
                Date = entity.Date,
                DistanceInMeters = entity.DistanceInMeters,
                DistanceInKilometers = Math.Round(entity.DistanceInMeters / 1000, 2),
                ElapsedTime = new JogTime(time.Hours, time.Minutes, time.Seconds),
            };
        }
    }
}
