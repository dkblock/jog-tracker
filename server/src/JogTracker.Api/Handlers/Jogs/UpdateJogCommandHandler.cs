using AutoMapper;
using JogTracker.Api.Validators;
using JogTracker.Common.Constants;
using JogTracker.Common.Exceptions;
using JogTracker.Entities;
using JogTracker.Models.DTO.Jogs;
using JogTracker.Models.Requests.Jogs;
using JogTracker.Repository;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace JogTracker.Api.Handlers.Jogs
{
    public class UpdateJogCommandHandler : IRequestHandler<UpdateJogCommand, Jog>
    {
        private readonly IJogsValidator _jogsValidator;
        private readonly IJogsRepository _jogsRepository;
        private readonly IUsersRepository _usersRepository;
        private readonly IMapper _mapper;

        public UpdateJogCommandHandler(
            IJogsValidator jogsValidator,
            IJogsRepository jogsRepository, 
            IUsersRepository usersRepository, 
            IMapper mapper)
        {
            _jogsValidator = jogsValidator;
            _jogsRepository = jogsRepository;
            _usersRepository = usersRepository;
            _mapper = mapper;
        }

        public async Task<Jog> Handle(UpdateJogCommand payload, CancellationToken cancellationToken)
        {
            if (!await _jogsRepository.Exists(payload.Id))
                throw new NotFoundException();

            var currentUser = await _usersRepository.GetById(payload.UserId);
            var entity = await _jogsRepository.GetWithChildren(payload.Id);

            if (!HasAccessToJog(entity, currentUser))
                throw new ForbiddenException();

            var validationResult = _jogsValidator.Validate(payload);

            if (!validationResult.IsValid)
                throw new BadRequestException(validationResult.ValidationErrors);

            var updatedEntity = _mapper.Map<JogEntity>(payload);
            updatedEntity.UserId = entity.UserId;

            await _jogsRepository.Update(updatedEntity);
            updatedEntity = await _jogsRepository.GetWithChildren(entity.Id);

            var result = _mapper.Map<Jog>(updatedEntity);
            result.HasAccess = true;

            return result;
        }

        private bool HasAccessToJog(JogEntity entity, UserEntity currentUser)
        {
            return entity.UserId == currentUser.Id || currentUser.Role == Roles.Administrator;
        }
    }
}
