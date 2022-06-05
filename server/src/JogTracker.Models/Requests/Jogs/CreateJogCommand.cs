using JogTracker.Models.DTO.Jogs;
using MediatR;

namespace JogTracker.Models.Requests.Jogs
{
    public class CreateJogCommand : JogPayloadCommand, IRequest<Jog>
    {
    }
}
