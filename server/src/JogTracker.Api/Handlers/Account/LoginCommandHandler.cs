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
    public class LoginCommandHandler : IRequestHandler<LoginCommand, AuthResult>
    {
        private readonly IAccountValidator _accountValidator;
        private readonly IAuthenticationService _authenticationService;
        private readonly IUsersRepository _usersRepository;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public LoginCommandHandler(
            IAccountValidator accountValidator,
            IAuthenticationService authenticationService,
            IUsersRepository usersRepository,
            IMediator mediator,
            IMapper mapper)
        {
            _accountValidator = accountValidator;
            _authenticationService = authenticationService;
            _usersRepository = usersRepository;
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task<AuthResult> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _accountValidator.ValidateOnLogin(request);

            if (!validationResult.IsValid)
                throw new BadRequestException(validationResult.ValidationErrors);

            var userEntity = await _usersRepository.GetByUserName(request.UserName);
            var user = _mapper.Map<User>(userEntity);
            user.Role = await _usersRepository.GetRole(userEntity.Id);

            var authResult = _authenticationService.Authenticate(user);
            await _mediator.Send(new CreateRefreshTokenCommand(authResult.Jwt.RefreshToken, userEntity.Id));

            return authResult;
        }
    }
}
