using AutoMapper;
using JogTracker.Common.Helpers;
using JogTracker.Entities;
using JogTracker.Models.Commands.Jogs;
using JogTracker.Models.DTO.Jogs;
using System;

namespace JogTracker.Mappers
{
    public class JogsProfile : Profile
    {
        private const int MetersInOneKilometerRatio = 1000;
        private const double MetersPerSecondToKilometersPerHourRatio = 3.6;

        public JogsProfile()
            : base()
        {
            CreateMap<JogEntity, Jog>()
                .ForMember(dest => dest.DistanceInKilometers, src => src.MapFrom(src => ToKilometers(src)))
                .ForMember(dest => dest.AverageSpeedInKilometersPerHour, src => src.MapFrom(src => ToKilometersPerHour(src)))
                .ForMember(dest => dest.ElapsedTime, src => src.MapFrom(src => ToTime(src)));

            CreateMap<CreateJogCommand, JogEntity>()
                .ForMember(dest => dest.DistanceInMeters, src => src.MapFrom(src => ToMeters(src)))
                .ForMember(dest => dest.ElapsedTimeInSeconds, src => src.MapFrom(src => ToTimeInSeconds(src)))
                .ForMember(dest => dest.AverageSpeedInMetersPerSecond, src => src.MapFrom(src => ToMetersPerSecond(src)));

            CreateMap<UpdateJogCommand, JogEntity>()
                .ForMember(dest => dest.DistanceInMeters, src => src.MapFrom(src => ToMeters(src)))
                .ForMember(dest => dest.ElapsedTimeInSeconds, src => src.MapFrom(src => ToTimeInSeconds(src)))
                .ForMember(dest => dest.AverageSpeedInMetersPerSecond, src => src.MapFrom(src => ToMetersPerSecond(src)));
        }

        private double ToMeters(JogPayloadCommand source)
        {
            return source.DistanceInMeters.HasValue
                ? (source.DistanceInMeters.Value)
                : (source.DistanceInKilometers.Value * MetersInOneKilometerRatio);
        }

        private double ToKilometers(JogEntity source)
        {
            return source.DistanceInMeters / MetersInOneKilometerRatio;
        }

        private double ToMetersPerSecond(JogPayloadCommand source)
        {
            return Round(ToMeters(source) / ToTimeInSeconds(source));
        }

        private double ToKilometersPerHour(JogEntity source)
        {
            var value = source.AverageSpeedInMetersPerSecond * MetersPerSecondToKilometersPerHourRatio;
            return Round(value);
        }

        private JogTime ToTime(JogEntity source)
        {
            var time = TimeSpan.FromSeconds(source.ElapsedTimeInSeconds);
            return new JogTime(time.Hours, time.Minutes, time.Seconds);
        }

        private int ToTimeInSeconds(JogPayloadCommand source)
        {
            return (int)new TimeSpan(source.ElapsedTime.Hours, source.ElapsedTime.Minutes, source.ElapsedTime.Seconds).TotalSeconds;
        }

        private double Round(double value) => QueryHelper.IsInteger(value) ? value : Math.Round(value, 1);
    }
}
