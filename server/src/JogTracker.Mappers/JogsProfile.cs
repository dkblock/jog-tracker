using AutoMapper;
using JogTracker.Common.Helpers;
using JogTracker.Entities;
using JogTracker.Models.DTO.Jogs;
using JogTracker.Models.Requests.Jogs;
using System;

namespace JogTracker.Mappers
{
    public class JogsProfile : Profile
    {
        public JogsProfile()
            : base()
        {
            CreateMap<JogEntity, Jog>()
                .ForMember(dest => dest.DistanceInKilometers, src => src.MapFrom(src => JogValuesMapper.ToKilometers(src)))
                .ForMember(dest => dest.AverageSpeedInKilometersPerHour, src => src.MapFrom(src => JogValuesMapper.ToKilometersPerHour(src)))
                .ForMember(dest => dest.ElapsedTime, src => src.MapFrom(src => JogValuesMapper.ToTime(src)));

            CreateMap<CreateJogCommand, JogEntity>()
                .ForMember(dest => dest.DistanceInMeters, src => src.MapFrom(src => JogValuesMapper.ToMeters(src)))
                .ForMember(dest => dest.ElapsedTimeInSeconds, src => src.MapFrom(src => JogValuesMapper.ToTimeInSeconds(src)))
                .ForMember(dest => dest.AverageSpeedInMetersPerSecond, src => src.MapFrom(src => JogValuesMapper.ToMetersPerSecond(src)));

            CreateMap<UpdateJogCommand, JogEntity>()
                .ForMember(dest => dest.DistanceInMeters, src => src.MapFrom(src => JogValuesMapper.ToMeters(src)))
                .ForMember(dest => dest.ElapsedTimeInSeconds, src => src.MapFrom(src => JogValuesMapper.ToTimeInSeconds(src)))
                .ForMember(dest => dest.AverageSpeedInMetersPerSecond, src => src.MapFrom(src => JogValuesMapper.ToMetersPerSecond(src)));
        }
    }

    public static class JogValuesMapper
    {
        private const int MetersInOneKilometerRatio = 1000;
        private const double MetersPerSecondToKilometersPerHourRatio = 3.6;

        public static double ToMeters(JogPayloadCommand source)
        {
            return source.DistanceInMeters.HasValue
                ? source.DistanceInMeters.Value
                : (source.DistanceInKilometers.Value * MetersInOneKilometerRatio);
        }

        public static double ToKilometers(JogEntity source)
        {
            return source.DistanceInMeters / MetersInOneKilometerRatio;
        }

        public static double ToKilometers(double meters)
        {
            return meters / MetersInOneKilometerRatio;
        }

        public static double ToMetersPerSecond(JogPayloadCommand source)
        {
            return Round(ToMeters(source) / ToTimeInSeconds(source));
        }

        public static double ToMetersPerSecond(double metersPerSecond)
        {
            return Round(metersPerSecond);
        }

        public static double ToKilometersPerHour(JogEntity source)
        {
            var value = source.AverageSpeedInMetersPerSecond * MetersPerSecondToKilometersPerHourRatio;
            return Round(value);
        }

        public static double ToKilometersPerHour(double metersPerSecond)
        {
            return Round(metersPerSecond * MetersPerSecondToKilometersPerHourRatio);
        }

        public static JogTime ToTime(JogEntity source)
        {
            return ToTime(source.ElapsedTimeInSeconds);
        }

        public static JogTime ToTime(double timeInSeconds)
        {
            var time = TimeSpan.FromSeconds(timeInSeconds);
            return new JogTime(time.Hours, time.Minutes, time.Seconds);
        }

        public static int ToTimeInSeconds(JogPayloadCommand source)
        {
            return (int)new TimeSpan(source.ElapsedTime.Hours, source.ElapsedTime.Minutes, source.ElapsedTime.Seconds).TotalSeconds;
        }

        private static double Round(double value) => QueryHelper.IsInteger(value) ? value : Math.Round(value, 1);
    }
}
