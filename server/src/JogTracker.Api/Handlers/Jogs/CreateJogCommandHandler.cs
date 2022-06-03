using AutoMapper;
using JogTracker.Api.Validators;
using JogTracker.Common.Exceptions;
using JogTracker.Entities;
using JogTracker.Models.Commands.Jogs;
using JogTracker.Models.DTO.Jogs;
using JogTracker.Repository;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace JogTracker.Api.Handlers.Jogs
{
    public class CreateJogCommandHandler : IRequestHandler<CreateJogCommand, Jog>
    {
        private readonly IJogsValidator _jogsValidator;
        private readonly IJogsRepository _jogsRepository;
        private readonly IMapper _mapper;

        public CreateJogCommandHandler(IJogsValidator jogsValidator, IJogsRepository jogsRepository, IMapper mapper)
        {
            _jogsValidator = jogsValidator;
            _jogsRepository = jogsRepository;
            _mapper = mapper;
        }

        public async Task<Jog> Handle(CreateJogCommand payload, CancellationToken cancellationToken)
        {
            var validationResult = _jogsValidator.Validate(payload);

            if (!validationResult.IsValid)
                throw new BadRequestException(validationResult.ValidationErrors);

            var entity = _mapper.Map<JogEntity>(payload);
            entity.Id = Guid.NewGuid().ToString();
            await _jogsRepository.Create(entity);

            return _mapper.Map<Jog>(entity);
        }
    }
}
