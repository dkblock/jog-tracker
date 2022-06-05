using AutoMapper;
using JogTracker.Api.Validators;
using JogTracker.Common.Constants;
using JogTracker.Common.Exceptions;
using JogTracker.Entities;
using JogTracker.Models.Commands.Jogs;
using JogTracker.Models.DTO.Jogs;
using JogTracker.Repository;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace JogTracker.Api.Handlers.Jogs
{
    public class UpdateJogCommandHandler : IRequestHandler<UpdateJogCommand, Jog>
    {
        private readonly IJogsRepository _jogsRepository;
        private readonly IUsersRepository _usersRepository;
        private readonly IJogsValidator _jogsValidator;
        private readonly IMapper _mapper;

        public UpdateJogCommandHandler(
            IJogsRepository jogsRepository, 
            IUsersRepository usersRepository, 
            IJogsValidator jogsValidator,
            IMapper mapper)
        {
            _jogsRepository = jogsRepository;
            _usersRepository = usersRepository;
            _jogsValidator = jogsValidator;
            _mapper = mapper;
        }

        public async Task<Jog> Handle(UpdateJogCommand payload, CancellationToken cancellationToken)
        {
            if (!await _jogsRepository.Exists(payload.Id))
                throw new NotFoundException();

            var entity = await _jogsRepository.Get(payload.Id);
            var role = await _usersRepository.GetRole(payload.UserId);

            if (!HasAccessToJog(entity, payload.UserId, role))
                throw new ForbiddenException();

            var validationResult = _jogsValidator.Validate(payload);

            if (!validationResult.IsValid)
                throw new BadRequestException(validationResult.ValidationErrors);

            entity = _mapper.Map<JogEntity>(payload);
            await _jogsRepository.Update(entity);
            entity = await _jogsRepository.GetWithChildren(entity.Id);

            var result = _mapper.Map<Jog>(entity);
            result.HasAccess = true;

            return result;
        }

        private bool HasAccessToJog(JogEntity entity, string userId, string userRole)
        {
            return entity.UserId == userId || userRole == Roles.Administrator;
        }
    }
}
