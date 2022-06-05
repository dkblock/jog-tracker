using AutoMapper;
using JogTracker.Models.DTO.Users;
using JogTracker.Models.Requests.Authentication;
using JogTracker.Repository;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace JogTracker.Api.Handlers.Authentication
{
    public class AuthenticateCommandHandler : IRequestHandler<AuthenticateCommand, User>
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IMapper _mapper;

        public AuthenticateCommandHandler(IUsersRepository usersRepository, IMapper mapper)
        {
            _usersRepository = usersRepository;
            _mapper = mapper;
        }

        public async Task<User> Handle(AuthenticateCommand payload, CancellationToken cancellationToken)
        {
            var userEntity = await _usersRepository.GetById(payload.UserId);
            var user = _mapper.Map<User>(userEntity);

            return user;
        }
    }
}
