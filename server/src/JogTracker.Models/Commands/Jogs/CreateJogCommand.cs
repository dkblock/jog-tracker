using JogTracker.Models.DTO.Jogs;
using MediatR;
using System;

namespace JogTracker.Models.Commands.Jogs
{
    public class CreateJogCommand : IRequest<Jog>
    {
        public double? DistanceInMeters { get; set; }
        public double? DistanceInKilometers { get; set; }
        public JogTime ElapsedTime { get; set; }
        public DateTime Date { get; set; }
        public string UserId { get; set; }
    }
}
