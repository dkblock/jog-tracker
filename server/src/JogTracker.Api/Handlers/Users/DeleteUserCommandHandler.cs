using JogTracker.Common.Exceptions;
using JogTracker.Models.Requests.Users;
using JogTracker.Repository;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace JogTracker.Api.Handlers.Users
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand>
    {
        private readonly IUsersRepository _usersRepository;

        public DeleteUserCommandHandler(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        public async Task<Unit> Handle(DeleteUserCommand payload, CancellationToken cancellationToken)
        {
            if (!await _usersRepository.IsExistById(payload.Id))
                throw new NotFoundException();

            if (payload.Id == payload.UserId)
                throw new ForbiddenException();

            var user = await _usersRepository.GetById(payload.Id);
            await _usersRepository.Delete(user);

            return Unit.Value;
        }
    }
}
