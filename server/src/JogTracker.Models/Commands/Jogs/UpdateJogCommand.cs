using JogTracker.Models.DTO.Jogs;
using MediatR;

namespace JogTracker.Models.Commands.Jogs
{
    public class UpdateJogCommand : JogPayloadCommand, IRequest<Jog>
    {
        public string Id { get; set; }
    }
}
