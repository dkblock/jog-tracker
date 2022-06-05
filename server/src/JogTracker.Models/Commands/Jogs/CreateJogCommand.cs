using JogTracker.Models.DTO.Jogs;
using MediatR;

namespace JogTracker.Models.Commands.Jogs
{
    public class CreateJogCommand : JogPayloadCommand, IRequest<Jog>
    {
    }
}
