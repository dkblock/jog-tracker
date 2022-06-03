using AutoMapper;
using JogTracker.Api.Validators;
using JogTracker.Common.Exceptions;
using JogTracker.Entities;
using JogTracker.Identity;
using JogTracker.Models.Commands.Account;
using JogTracker.Models.DTO.Account;
using JogTracker.Models.DTO.Users;
using JogTracker.Repository;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace JogTracker.Api.Handlers.Account
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, AuthResult>
    {
        private readonly IAccountValidator _accountValidator;
        private readonly IAuthenticationService _authenticationService;
        private readonly IUsersRepository _usersRepository;
        private readonly IMapper _mapper;

        public RegisterCommandHandler(
            IAccountValidator accountValidator,
            IAuthenticationService authenticationService,
            IUsersRepository usersRepository,
            IMapper mapper)
        {
            _accountValidator = accountValidator;
            _authenticationService = authenticationService;
            _usersRepository = usersRepository;
            _mapper = mapper;
        }

        public async Task<AuthResult> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _accountValidator.ValidateOnRegister(request);

            if (!validationResult.IsValid)
                throw new BadRequestException(validationResult.ValidationErrors);

            var userEntity = _mapper.Map<UserEntity>(request);
            userEntity.Id = Guid.NewGuid().ToString();

            await _usersRepository.Create(userEntity, request.Password);

            var user = _mapper.Map<User>(userEntity);
            user.Role = await _usersRepository.GetRole(userEntity.Id);

            var authResult = _authenticationService.Authenticate(user);
            return authResult;
        }
    }
}
