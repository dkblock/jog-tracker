using JogTracker.Common.Constants;
using JogTracker.Common.Exceptions;
using JogTracker.Entities;
using JogTracker.Models.Commands.Jogs;
using JogTracker.Repository;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace JogTracker.Api.Handlers.Jogs
{
    public class DeleteJogCommandHandler : IRequestHandler<DeleteJogCommand>
    {
        private readonly IJogsRepository _jogsRepository;
        private readonly IUsersRepository _usersRepository;

        public DeleteJogCommandHandler(IJogsRepository jogsRepository, IUsersRepository usersRepository)
        {
            _jogsRepository = jogsRepository;
            _usersRepository = usersRepository;
        }

        public async Task<Unit> Handle(DeleteJogCommand payload, CancellationToken cancellationToken)
        {
            if (!await _jogsRepository.Exists(payload.Id))
                throw new NotFoundException();

            var entity = await _jogsRepository.Get(payload.Id);
            var userRole = await _usersRepository.GetRole(payload.UserId);

            if (!HasAccessToJog(entity, payload.UserId, userRole))
                throw new ForbiddenException();

            await _jogsRepository.Delete(entity);
            return Unit.Value;
        }

        private bool HasAccessToJog(JogEntity entity, string userId, string userRole)
        {
            return entity.UserId == userId || userRole == Roles.Administrator;
        }
    }
}
