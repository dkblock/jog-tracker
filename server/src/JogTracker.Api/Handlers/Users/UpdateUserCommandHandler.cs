using AutoMapper;
using JogTracker.Api.Validators;
using JogTracker.Common.Exceptions;
using JogTracker.Models.DTO.Users;
using JogTracker.Models.Requests.Users;
using JogTracker.Repository;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace JogTracker.Api.Handlers.Users
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, User>
    {
        private readonly IAccountValidator _accountValidator;
        private readonly IUsersRepository _usersRepository;
        private readonly IMapper _mapper;

        public UpdateUserCommandHandler(IAccountValidator accountValidator, IUsersRepository usersRepository, IMapper mapper)
        {
            _accountValidator = accountValidator;
            _usersRepository = usersRepository;
            _mapper = mapper;
        }

        public async Task<User> Handle(UpdateUserCommand payload, CancellationToken cancellationToken)
        {
            if (!await _usersRepository.IsExistById(payload.Id))
                throw new NotFoundException();

            var validationResult = _accountValidator.ValidateName(payload);

            if (!validationResult.IsValid)
                throw new BadRequestException(validationResult.ValidationErrors);

            var user = await _usersRepository.GetById(payload.Id);
            user.FirstName = payload.FirstName;
            user.LastName = payload.LastName;

            if (user.Role != payload.Role)
            {
                if (payload.UserId == user.Id)
                    throw new ForbiddenException();

                user.Role = payload.Role;
                await _usersRepository.UpdateRole(user, user.Role);
            }

            await _usersRepository.Update(user);
            return _mapper.Map<User>(user);
        }
    }
}
